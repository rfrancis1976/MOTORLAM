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
    public class TypeProductController : MotorlamController
    {
        [HttpPost]
        public ActionResult DeleteTypeProduct(int Id)
        {
            TypeProduct typeProduct = this.CreateQuery<TypeProduct>(Proyection.Basic).Where(TypeProductFields.TypeProductId, Id).ToList().FirstOrDefault();
            if (typeProduct != null)
            {
                try
                {
                    base.Repository.Delete(typeProduct);
                }
                catch (Exception)
                {
                    base.ModelState.AddModelError("ErrorSQL", "No se ha podido eliminar el Tipo de Producto, porque esta asociado a una factura o albaran");
                    return this.Json(new { result = "error", validationErrors = base.ModelState.GetErrors() });                     
                }
            }
            
            return this.Json(new { result = "success" });            
        }

        public ActionResult Editar(int TypeProductId)
        {
            ViewBag.Message = "Tipos de Productos";
            TypeProduct typeProduct = this.CreateQuery<TypeProduct>(Proyection.Basic).Where(TypeProductFields.TypeProductId, TypeProductId).ToList().FirstOrDefault();
            return View("Nuevo", typeProduct);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Tipos de Productos";
            var list = this.CreateQuery<TypeProduct>(Proyection.Basic).OrderBy(TypeProductFields.TypeProductName).ToList();
            return View(list);
        }

        public ActionResult LoadTypeProducts()
        {
            var list = this.CreateQuery<TypeProduct>(Proyection.Basic).OrderBy(TypeProductFields.TypeProductName).ToList();
            return PartialView("List", list);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Message = "Tipos de Productos";
            return View(new TypeProduct());
        }

        public ActionResult SalvarTypeProduct(TypeProduct type)
        {
            ViewBag.Message = "Tipos de Productos";
            if (type.TypeProductId != 0)
            {
                base.Repository.Update(type);
            }
            else
            {
                base.Repository.Insert(type);
            }
            return View("Nuevo", type);
        }
    }
}
