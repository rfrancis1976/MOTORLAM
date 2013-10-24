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

            ViewBag.Suppliers = this.CreateQuery<Supplier>(Proyection.Basic).ToList();
            
            var deliverys = this.CreateQuery<Delivery>(Proyection.Detailed)
                .Where(DeliveryFields.DeliveryIsPaid,false)
                .OrderByDesc(DeliveryFields.DeliveryDate)
                .ToList();

            return View(deliverys);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Message = "Albaranes";

            ViewBag.Suppliers = this.CreateQuery<Supplier>(Proyection.Basic).ToList();
            ViewBag.BrandProducts = this.CreateQuery<BrandProduct>(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.CreateQuery<TypeProduct>(Proyection.Basic).ToList();
            
             
            return View(new Delivery());
        }

        public ActionResult Editar(int DeliveryId)
        {
            ViewBag.Message = "Albaranes";

            var delivery = this.CreateQuery<Delivery>(Proyection.Detailed).Where(DeliveryFields.DeliveryId, DeliveryId).ToList().FirstOrDefault();

            ViewBag.Suppliers = this.CreateQuery<Supplier>(Proyection.Basic).ToList();
            ViewBag.BrandProducts = this.CreateQuery<BrandProduct>(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.CreateQuery<TypeProduct>(Proyection.Basic).ToList();
          
            ViewBag.DeliveryLines = this.CreateQuery<DeliveryLine>(Proyection.Basic).Where(DeliveryLineFields.DeliveryId, DeliveryId).ToList();
            ViewBag.DeliveryLine = new DeliveryLine();

            return View("Nuevo", delivery);

        }

        [HttpPost]
        public ActionResult DeleteDelivery(int Id)
        {
            var deliveryLines = this.CreateQuery<DeliveryLine>(Proyection.Basic)
                .Where(DeliveryLineFields.DeliveryId, Id).ToList();

            if (deliveryLines != null)
            {
                foreach (var line in deliveryLines)
                {
                    this.Repository.Delete(line);
                }
            }

            var delivery = this.CreateQuery<Delivery>(Proyection.Basic).Get(Id);

            this.Repository.Delete(delivery);

            return this.Json(new { result = "success" });
        }


        public ActionResult DeleteDeliveryLine(int ID)
        {
            var line = this.CreateQuery<DeliveryLine>(Proyection.Basic).Get(ID);

            if (line != null) this.Repository.Delete(line);

            TotalsDelivery(line.DeliveryId);

            var deliveryLines = this.CreateQuery<DeliveryLine>(Proyection.Basic).Where(DeliveryLineFields.DeliveryId, line.DeliveryId).ToList();

            return PartialView("OrderList", deliveryLines);
        }

     
        [HttpPost]
        public ActionResult SaveDelivery(Delivery delivery)
        {
            var deli = this.CreateQuery<Delivery>(Proyection.Basic)
                .Where(DeliveryFields.DeliveryId, delivery.DeliveryId).ToList().FirstOrDefault();

            if (deli != null)
                this.Repository.Update(delivery);
            else
            {
                this.Repository.Insert(delivery);
            }         
                        
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
                deliveryLine = this.CreateQuery<DeliveryLine>(Proyection.Basic)
                    .Where(DeliveryLineFields.DeliveryLineId, DeliveryLineId).ToList().FirstOrDefault();
            }

            var product = new Product();
            if (ProductId.HasValue) product = this.CreateQuery<Product>(Proyection.Basic).Where(ProductFields.ProductId, ProductId).ToList().FirstOrDefault();
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
            if (DeliveryLineId > 0)
            {
                this.Repository.Update(deliveryLine);
            }
            else
            {
                this.Repository.Insert(deliveryLine);
            }
            TotalsDelivery(DeliveryID);

            this.Repository.Commit();

            return this.Json(new { result = "success" });
        }
     

        private void TotalsDelivery(int? DeliveryID)
        {
            var delivery = this.CreateQuery<Delivery>(Proyection.Basic).Where(DeliveryFields.DeliveryId, DeliveryID).ToList().FirstOrDefault();

            var deliveryLines = this.CreateQuery<DeliveryLine>(Proyection.Basic).Where(DeliveryLineFields.DeliveryId, DeliveryID).ToList();

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

            this.Repository.Update(delivery);
        }

        public ActionResult LoadInvoice(int InvoiceId)
        {
            var invoice = this.CreateQuery<Invoice>(Proyection.Basic)
                .Where(InvoiceFields.InvoiceId, InvoiceId).ToList().FirstOrDefault();
            ViewBag.InvoiceLines = this.CreateQuery<InvoiceLine>(Proyection.Basic)
                .Where(InvoiceLineFields.InvoiceId, InvoiceId).ToList();
            var provinces = this.CreateQuery<Province>(Proyection.Basic).ToList();
            ViewBag.Provinces = provinces;
            ViewBag.Cities = new List<City>();
            ViewBag.Customers = new List<Customer>();

            return View("Nuevo",invoice);
        }


        public ActionResult EditarLinea(int DeliveryLineId)
        {
            var deliveryLine = this.CreateQuery<DeliveryLine>(Proyection.Basic)
                .Where(DeliveryLineFields.DeliveryLineId, DeliveryLineId).ToList().FirstOrDefault();

            ViewBag.BrandProducts = this.CreateQuery<BrandProduct>(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.CreateQuery<TypeProduct>(Proyection.Basic).ToList(); 

            return PartialView("DatosLineaDelivery", deliveryLine);
        }

        
        public ActionResult Buscar(string RefDelivery2, DateTime? DeliveryDate, int? SupplierId, int DeliveryIsPaid)
        {
            var deliverys = this.CreateQuery<Delivery>(Proyection.Detailed);

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
            var deliverys = this.CreateQuery<Delivery>(Proyection.Detailed);
            
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
            var product = this.CreateQuery<Product>(Proyection.Detailed).Where(ProductFields.ProductReference, ProductReference).ToList().FirstOrDefault();

            if (product != null) return this.Json(new { result = "success", Product = product });
            else return this.Json(new { result = "error"});
        }
    }
}
