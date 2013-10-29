using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motorlam.Controllers;
using Motorlam.Entities;
using inercya.ORMLite;
using inercya.Gataca.Web.Models;
using System.Globalization;


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
            ViewBag.DeliveryLines = this.DataService.DeliveryLineRepository.CreateQuery(Proyection.Basic).Where(DeliveryLineFields.DeliveryId, DeliveryId).ToList();
            ViewBag.DeliveryLine = new DeliveryLine();
            return View("Nuevo", delivery);
        }

        [HttpPost]
        public ActionResult DeleteDelivery(int Id)
        {
            var deliveryLines = this.DataService.DeliveryLineRepository.CreateQuery(Proyection.Basic).Where(DeliveryLineFields.DeliveryId, Id).ToList();
            if (deliveryLines != null)
            {
                foreach (var line in deliveryLines)
                {
                    this.Repository.Delete(line);
                }
            }
            var delivery = this.DataService.DeliveryRepository.CreateQuery(Proyection.Basic).Get(Id);
            this.DataService.Delete(delivery);
            return this.Json(new { result = "success" });
        }

        public ActionResult DeleteDeliveryLine(int ID)
        {
            var line = this.DataService.DeliveryLineRepository.CreateQuery(Proyection.Basic).Get(ID);
            if (line != null) this.Repository.Delete(line);
            TotalsDelivery(line.DeliveryId);
            var deliveryLines = this.DataService.DeliveryLineRepository.CreateQuery(Proyection.Basic).Where(DeliveryLineFields.DeliveryId, line.DeliveryId).ToList();
            return PartialView("OrderList", deliveryLines);
        }
     
        [HttpPost]
        public ActionResult SaveDelivery(Delivery delivery)
        {
            SaveEntity(delivery);
            return this.Json(new { result = "success" , Delivery = delivery});
        }

        [HttpPost]
        public ActionResult SaveDeliveryLine(int? DeliveryLineId, int? DeliveryID, int? ProductId, string ProductReference,
                                            string ProductName, decimal ProductCost, double? DeliveryLineQuantity,
                                            double? discount, int? SupplierId, int? BrandProductId, int? TypeProductId)
        {
            var deliveryLine = new DeliveryLine();

            if (DeliveryLineId > 0)
            {
                deliveryLine = this.DataService.DeliveryLineRepository.CreateQuery(Proyection.Basic)
                    .Where(DeliveryLineFields.DeliveryLineId, DeliveryLineId).ToList().FirstOrDefault();
            }

            var product = new Product();
            if (ProductId.HasValue) product = this.DataService.ProductRepository.CreateQuery(Proyection.Basic).Where(ProductFields.ProductId, ProductId).ToList().FirstOrDefault();
            if (DeliveryID.HasValue) deliveryLine.DeliveryId = DeliveryID;
            if (!string.IsNullOrEmpty(ProductReference)) product.ProductReference = ProductReference;

            product.ProductName = ProductName;
            product.ProductCost = ProductCost;

            if (SupplierId.HasValue) product.SupplierId = SupplierId;
            if (BrandProductId.HasValue) product.BrandProductId = BrandProductId;
            if (TypeProductId.HasValue) product.TypeProductId = TypeProductId;

            if (ProductId.HasValue) this.Repository.Update(product);
            else this.Repository.Insert(product);


            deliveryLine.ProductId = product.ProductId;
            deliveryLine.DeliveryLineQuantity = DeliveryLineQuantity;
            deliveryLine.discount = discount / 100;
            var total = product.ProductCost * (decimal)deliveryLine.DeliveryLineQuantity;
            if (discount.HasValue)
            {
                deliveryLine.LineCostTotal = total - (total * (decimal)deliveryLine.discount);
                //deliveryLine.InvoideLineTotalDiscount = total * (decimal)deliveryLine.InvoiceLineDiscount;
            }
            else
            {
                deliveryLine.discount = 0;
                deliveryLine.LineCostTotal = total;
            }


            this.Repository.BeginTransaction();
            SaveEntity(deliveryLine);
            TotalsDelivery(DeliveryID);

            this.Repository.Commit();

            return this.Json(new { result = "success" });
        }
     

        private void TotalsDelivery(int? DeliveryID)
        {
            var delivery = this.DataService.DeliveryRepository.CreateQuery(Proyection.Basic).Where(DeliveryFields.DeliveryId, DeliveryID).ToList().FirstOrDefault();

            var deliveryLines = this.DataService.DeliveryLineRepository.CreateQuery(Proyection.Basic).Where(DeliveryLineFields.DeliveryId, DeliveryID).ToList();

            decimal total = 0;
            double totaldiscount = 0;

            foreach (var line in deliveryLines)
            {
                total += (decimal)line.LineCostTotal;
                if (line.discount.HasValue)
                {
                    totaldiscount += (double)line.ProductCost * (double)line.discount;
                }

            }
            delivery.TotalDeliverySinIVA = total;
            delivery.TotalDeliveryConIVA = total + (total * 18 / 100);
            //delivery.InvoiceTotalDiscount = (decimal)totaldiscount;

            this.DataService.Update(delivery);
        }

        public ActionResult LoadInvoice(int InvoiceId)
        {
            var invoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).Where(InvoiceFields.InvoiceId, InvoiceId).ToList().FirstOrDefault();
            ViewBag.InvoiceLines = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic).Where(InvoiceLineFields.InvoiceId, InvoiceId).ToList();
            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Cities = new List<City>();
            ViewBag.Customers = new List<Customer>();
            return View("Nuevo",invoice);
        }

        public ActionResult EditarLinea(int DeliveryLineId)
        {
            var deliveryLine = this.DataService.DeliveryLineRepository.CreateQuery(Proyection.Basic)
                .Where(DeliveryLineFields.DeliveryLineId, DeliveryLineId).ToList().FirstOrDefault();
            ViewBag.BrandProducts = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList(); 
            return PartialView("DatosLineaDelivery", deliveryLine);
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
