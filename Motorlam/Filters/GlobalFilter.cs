using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Motorlam.Data;
using Motorlam.Controllers;
using Motorlam.Services;
using System.Web.Routing;

namespace Motorlam.Filters
{
    public class MotorlamFilterAttribute : ActionFilterAttribute
    {
        private static string[] publicPaths = new string[] {
            "/Content/",
            "/Scripts/",
            "/Home/Login",
            "/Home/LogOff",
            "/img/",
            "/PassRecovery",
            "/Security/Unauthorized",
            "/Security/Login"
        };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            if (!filterContext.IsChildAction)
            {
                Debug.WriteLine(string.Format("Controller: {0}, Action: {1}", controllerName, actionName));
            }
            var dataserviceContext = new DataServiceUserContext();
            var authorizationManager = new AuthorizationManager(dataserviceContext.DataService);
            filterContext.Controller.ViewData["CurrentUser"] = dataserviceContext.CurrentUser;

            var controller = filterContext.Controller as MotorlamController;
            if (controller != null)
            {
                controller.DataService = dataserviceContext.DataService;
                controller.AuthorizationManager = authorizationManager;                
            }          
        }
    }
}