using inercya.ORMLite;
using Microsoft.CSharp.RuntimeBinder;
using Motorlam.Extenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Motorlam.Entities;

namespace Motorlam.Controllers
{
    public class BrandController : MotorlamController
    {
        [HttpPost]
        public ActionResult DeleteBrand(int Id)
        {
            Brand brand = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).Where(BrandFields.BrandId, Id).ToList().FirstOrDefault();
            if (brand != null)
            {
                try
                {
                    this.DataService.Delete(brand);
                }
                catch (Exception)
                {
                    this.ModelState.AddModelError("ErrorSQL", "No se ha podido eliminar esta Marca de Coche, porque esta asociado a una factura o albaran");
                    return this.Json(new { result = "error", validationErrors = base.ModelState.GetErrors() });                    
                }
            }
            return this.Json(new { result = "success" });            
        }

        public ActionResult Editar(int BrandId)
        {
            ViewBag.Message = "Marcas de Coche";
            Brand brand = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).Where(BrandFields.BrandId, BrandId).ToList().FirstOrDefault();
            return View("Nuevo", brand);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Marcas de Coche";
            var list = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).OrderBy(BrandFields.BrandName).ToList();
            return View(list);
        }

        public ActionResult LoadBrands()
        {
            var list = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).OrderBy(BrandFields.BrandName).ToList(); ;
            return this.PartialView("List", list);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Message = "Marcas de Coche";
            return View(new Brand());
        }

        public ActionResult SalvarBrand(Brand brand)
        {
            ViewBag.Message = "Marcas de Coche";
            SaveEntity(brand);
         
            return base.View("Nuevo", brand);
        }
    }
}
