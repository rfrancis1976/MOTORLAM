using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using FluentValidation.Mvc;
using FluentValidation.Attributes;
using System.Globalization;
using System.Threading;
using Motorlam.Filters;

namespace Motorlam
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MotorlamFilterAttribute());
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var culture = CultureInfo.GetCultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ValidationExtensions.ResourceClassKey = "BindingErrors";
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(
                    new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            IDisposable ds = (IDisposable)this.Context.Items["DataService"];
            if (ds != null)
            {
                ds.Dispose();
            }
        }
    }
}