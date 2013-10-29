using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using inercya.ORMLite;
using System.Web;

namespace Motorlam.Entities
{
    public partial class UserRepository
    {
        public IList<User> Get()
        {
            return this.CreateQuery(Proyection.Basic).ToList();
        }

        public User Get(int userId)
        {
            return this.CreateQuery(Proyection.Detailed).Get(userId);
        }

       
        public User Get(string loginName)
        {
            return this.CreateQuery(Proyection.Detailed)
                .Get(UserFields.UserLoginName, loginName);
        }

        public User Get(string loginName, string password)
        {

            var sha1 = new SHA256Managed();
            var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));

            return this.CreateQuery(Proyection.Detailed)
                        .Where(UserFields.UserPasswordHash, hashBytes)
                        .Get(UserFields.UserLoginName, loginName);

        }

        public User GetCurrentUser()
        {
            var _currentUser = (User)HttpContext.Current.Session["User"];
            if (_currentUser == null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    _currentUser = this.CreateQuery(Proyection.Detailed).Get(UserFields.UserLoginName, HttpContext.Current.User.Identity.Name);
                    if (_currentUser != null)
                    {
                        HttpContext.Current.Session["User"] = _currentUser;
                    }
                }
            }
            return _currentUser;
        }

    }
}
