using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Motorlam.Entities;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;

namespace Motorlam.Data
{
    public class DataServiceUserContext
    {
        public DataServiceUserContext()
        {
            this.DataService = this.GetDataService();
            this.CurrentUser = this.GetUser();
            this.DataService.User = this.CurrentUser;
            SetCulture("es-ES");          
        }

        public DataService DataService { get; private set; }

        public string LanguageCode { get; private set; }

        private DataService GetDataService()
        {
            DataService _dataService = (DataService)HttpContext.Current.Items["DataService"];
            if (_dataService == null)
            {
                _dataService = new DataService();
                HttpContext.Current.Items["DataService"] = _dataService;
            }
            return _dataService;
        }


        public User CurrentUser { get; private set; }


        protected User GetUser()
        {
            return this.DataService.UserRepository.GetCurrentUser();
        }

        void SetCulture(string cultureName)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name != cultureName || Thread.CurrentThread.CurrentCulture.Name != cultureName)
            {
                CultureInfo culture = CultureInfo.GetCultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}
