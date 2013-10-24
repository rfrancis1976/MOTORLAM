using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using FluentValidation.Mvc;
using FluentValidation.Attributes;
using Motorlam.Data;

namespace Motorlam
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            this.BeginRequest += new EventHandler(MvcApplication_BeginRequest);
            this.EndRequest += new EventHandler(MvcApplication_EndRequest);
        }

        void MvcApplication_EndRequest(object sender, EventArgs e)
        {
            MotorlamRepository repository = (MotorlamRepository)this.Context.Items["_MotorlamRepository_"];
            if (repository != null) repository.Dispose();
        }

        void MvcApplication_BeginRequest(object sender, EventArgs e)
        {
            this.Context.Items.Add("_MotorlamRepository_", new MotorlamRepository());
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

            RegisterRoutes(RouteTable.Routes);
            ValidationExtensions.ResourceClassKey = "BindingErrors";
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(
                    new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));

        }
    }
}