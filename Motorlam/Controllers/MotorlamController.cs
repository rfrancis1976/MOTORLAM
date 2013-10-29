using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inercya.ORMLite;
using Motorlam.Data;
using Motorlam.Entities;
using Motorlam.Services;


namespace Motorlam.Controllers
{
    public abstract class MotorlamController : Controller
    {
        public DataService DataService { get; set; }
        public AuthorizationManager AuthorizationManager { get; set; }

        public User CurrentUser
        {
            get
            {
                return AuthorizationManager == null ? null : AuthorizationManager.CurrentUser;
            }
        }

        protected DataService Repository
        {
            get { return (DataService)this.HttpContext.Items["DataService"]; }
        }

        protected QueryLite<T> CreateQuery<T>(Proyection proyection) where T:class, new()
        {
            return new QueryLite<T>(proyection, this.Repository);
        }

        protected virtual ActionResult SaveEntity(object entity)
        {
            if (TryUpdateModel(entity))
            {
                if ((int)entity.GetId() == 0)
                {                    
                    this.Repository.Insert(entity);
                }
                else
                {
                    this.Repository.Update(entity);
                }
                return this.Json(new { result = "success", EntityId = entity.GetId() });
            }
            else
            {
                return this.Json(new
                {
                    result = "error"
                    //validationErrors = ModelState.GetErrors()
                });
            }
        }
    }
}
