using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motorlam.Controllers;
using inercya.ORMLite;
using inercya.Gataca.Web.Models;
using System.Globalization;
using Motorlam.Entities;


namespace Motorlam.Controllers
{
    public class DeliveryController : MotorlamController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Albaranes";
            CultureInfo culturaPersonal = new CultureInfo("es-ES");
            culturaPersonal.NumberFormat.CurrencyDecimalDigits = 2;
            culturaPersonal.NumberFormat.PercentDecimalDigits = 2;
            culturaPersonal.NumberFormat.NumberDecimalDigits = 2;

            ViewBag.Suppliers = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();            
            var deliverys = this.DataService.DeliveryRepository.CreateQuery(Proyection.Detailed)
                .Where(DeliveryFields.DeliveryIsPaid,false).OrderByDesc(DeliveryFields.DeliveryDate).ToList();
            return View(deliverys);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Message = "Albaranes";
            ViewBag.Suppliers = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.BrandProducts = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList();            
            return View(new Delivery());
        }

        public ActionResult Editar(int DeliveryId)
        {
            ViewBag.Message = "Albaranes";
            var delivery = this.DataService.DeliveryRepository.CreateQuery(Proyection.Detailed).Where(DeliveryFields.DeliveryId, DeliveryId).ToList().FirstOrDefault();
            ViewBag.Suppliers = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.BrandProducts = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList();
            return View("Nuevo", delivery);
        }

        [HttpPost]
        public ActionResult DeleteDelivery(int Id)
        {
            var delivery = this.DataService.DeliveryRepository.CreateQuery(Proyection.Basic).Get(Id);
            this.DataService.Delete(delivery);
            return this.Json(new { result = "success" });
        }
            
        [HttpPost]
        public ActionResult SaveDelivery(Delivery delivery)
        {
            if (delivery.DeliveryId != 0)
                this.DataService.Update(delivery);
            else
                this.DataService.Insert(delivery);

            return this.Json(new { result = "success" , Delivery = delivery});
        }

        public ActionResult Buscar(string RefDelivery2, DateTime? DeliveryDate, int? SupplierId, int DeliveryIsPaid)
        {
            var deliverys = this.DataService.DeliveryRepository.CreateQuery(Proyection.Detailed);

            if (!string.IsNullOrEmpty(RefDelivery2))
                deliverys.And(DeliveryFields.RefDelivery, OperatorLite.Contains, RefDelivery2);
            if (SupplierId.HasValue)
                deliverys.And(DeliveryFields.SupplierId, SupplierId);
            if (DeliveryDate != null)
                deliverys.And(DeliveryFields.DeliveryDate, DeliveryDate);

            if (DeliveryIsPaid == 1)
                deliverys.And(DeliveryFields.DeliveryIsPaid, true);
            else if (DeliveryIsPaid == 2)
                deliverys.And(DeliveryFields.DeliveryIsPaid, false);

            return PartialView("List", deliverys.OrderByDesc(DeliveryFields.DeliveryDate).ToList());
        }


        public ActionResult LoadDeliverys(string RefDelivery2, DateTime? DeliveryDate, int? SupplierId, int DeliveryIsPaid)
        {
            var deliverys = this.DataService.DeliveryRepository.CreateQuery(Proyection.Detailed);
            
            if (!string.IsNullOrEmpty(RefDelivery2))
                deliverys.And(DeliveryFields.RefDelivery, OperatorLite.Contains, RefDelivery2);
            if (SupplierId.HasValue)
                deliverys.And(DeliveryFields.SupplierId, SupplierId);
            if (DeliveryDate != null)
                deliverys.And(DeliveryFields.DeliveryDate, DeliveryDate);
            
            if (DeliveryIsPaid == 1)
                deliverys.And(DeliveryFields.DeliveryIsPaid, true);
            else if (DeliveryIsPaid == 2)
                deliverys.And(DeliveryFields.DeliveryIsPaid, false);

            return PartialView("List", deliverys.ToList());
        }
    

        [HttpPost]
        public ActionResult LoadProductByReference(string ProductReference)
        {
            var product = this.DataService.ProductRepository.CreateQuery(Proyection.Detailed).Where(ProductFields.ProductReference, ProductReference).ToList().FirstOrDefault();

            if (product != null) return this.Json(new { result = "success", Product = product });
            else return this.Json(new { result = "error"});
        }
    }
}
