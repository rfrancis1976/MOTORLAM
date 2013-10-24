using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motorlam.Controllers;


namespace Motorlam.Controllers
{
    public class AdministrationController : MotorlamController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "MotorLam";

            return View();
        }

    }
}
