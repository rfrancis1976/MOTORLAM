using inercya.ORMLite;
using Microsoft.CSharp.RuntimeBinder;
using Motorlam.Entities;
using Motorlam.Extenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace Motorlam.Controllers
{
    public class BrandProductController : MotorlamController
    {
        [HttpPost]
        public ActionResult DeleteBrandProduct(int Id)
        {
            var brandProduct = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).Where(BrandProductFields.BrandProductId, Id).ToList().FirstOrDefault();
            if (brandProduct != null)
            {
                try
                {
                    this.Repository.Delete(brandProduct);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("ErrorSQL", "No se ha podido eliminar la Marca de Producto, porque esta asociado a una factura o albaran");
                    return this.Json(new { result = "error", validationErrors = base.ModelState.GetErrors() });                   
                }
            }
            return this.Json(new { result = "success" });            
        }

        public ActionResult Editar(int BrandProductId)
        {
            ViewBag.Message = "Marcas de Coche";
            var brandProduct = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).Where(BrandProductFields.BrandProductId, BrandProductId).ToList().FirstOrDefault();
            return View("Nuevo", brandProduct);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Marcas de Coche";
            var list = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).OrderBy(BrandProductFields.BrandProductName).ToList();
            return View(list);
        }

        public ActionResult LoadBrandProducts()
        {
            var list = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).OrderBy(BrandProductFields.BrandProductName).ToList();
            return PartialView("List", list);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Message = "Marcas de Coche";
            return View(new BrandProduct());
        }

        public ActionResult SalvarBrandProduct(BrandProduct brand)
        {
            ViewBag.Message = "Marcas de Coche";
            SaveEntity(brand);
            return base.View("Nuevo", brand);
        }
    }
}
