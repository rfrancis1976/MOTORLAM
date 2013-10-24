using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motorlam.Controllers;
using Motorlam.Data;
using inercya.ORMLite;
using Motorlam.Entities;


namespace Motorlam.Controllers
{
    public class EstimateController : MotorlamController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "MotorLam";

            var invoices = new List<Invoice>();


            return View(invoices);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Message = "Facturas";

            var provinces = this.CreateQuery<Province>(Proyection.Basic).ToList();

            ViewBag.Provinces = provinces;
            ViewBag.Cities = new List<City>();
            ViewBag.Customers = new List<Customer>();
            ViewBag.InvoiceLines = new List<InvoiceLine>();
            ViewBag.InvoiceLine = new InvoiceLine();
            return View(new Invoice());

        }

        public ActionResult Editar(int InvoiceId)
        {
            ViewBag.Message = "Facturas";

            var invoice = this.CreateQuery<Invoice>(Proyection.Detailed).Where(InvoiceFields.InvoiceId, InvoiceId).ToList().FirstOrDefault();

            var provinces = this.CreateQuery<Province>(Proyection.Basic).ToList();

            ViewBag.Provinces = provinces;
            ViewBag.Cities = this.CreateQuery<City>(Proyection.Basic).Where(CityFields.ProvinceId,invoice.ProvinceId).ToList();
            ViewBag.Customers = new List<Customer>();
            ViewBag.InvoiceLines = this.CreateQuery<InvoiceLine>(Proyection.Basic).Where(InvoiceLineFields.InvoiceId, InvoiceId).ToList();
            ViewBag.InvoiceLine = new InvoiceLine();

            return View("Nuevo",invoice);

        }

        [HttpPost]
        public ActionResult DeleteInvoice(int Id, int? InvoiceId, string CustomerName, DateTime? InvoiceDate, string NIF)
        {
            var invoiceLines = this.CreateQuery<InvoiceLine>(Proyection.Basic)
                .Where(InvoiceLineFields.InvoiceId, Id).ToList();

            if (invoiceLines != null)
            {
                foreach (var line in invoiceLines)
                {
                    this.Repository.Delete(line);
                }
            }

            var invoice = this.CreateQuery<Invoice>(Proyection.Basic).Get(Id);

            this.Repository.Delete(invoice);

            var invoices = this.CreateQuery<Invoice>(Proyection.Detailed);

            if (!string.IsNullOrEmpty(CustomerName))
                invoices.Where(InvoiceFields.CustomerName, OperatorLite.Contains, CustomerName);
            if (InvoiceId.HasValue)
                invoices.And(InvoiceFields.InvoiceId, InvoiceId);
            if (InvoiceDate != null)
                invoices.And(InvoiceFields.InvoiceDate, InvoiceDate);
            if (!string.IsNullOrEmpty(CustomerName))
                invoices.And(InvoiceFields.InvoiceCustomerNIF, OperatorLite.Contains, NIF);
            
            return PartialView("List", invoices.ToList());
        }


        public ActionResult DeleteInvoiceLine(int ID)
        {
            var line = this.CreateQuery<InvoiceLine>(Proyection.Basic).Get(ID);

            if (line != null) this.Repository.Delete(line);

            TotalsInvoice(line.InvoiceId);

            var invoiceLines = this.CreateQuery<InvoiceLine>(Proyection.Basic).Where(InvoiceLineFields.InvoiceId, line.InvoiceId).ToList();

            return PartialView("OrderList", invoiceLines);
        }

        [HttpPost]
        public ActionResult LoadCustomer(int CustomerID)
        {
            var customer = this.CreateQuery<Customer>(Proyection.Detailed).Get(CustomerID);
            var cities = this.CreateQuery<City>(Proyection.Basic).Where(CityFields.ProvinceId, customer.ProvinceId).ToList();
            return this.Json(new { result = "success", Customer = customer, Cities = cities });
        }

        [HttpPost]
        public ActionResult SearchCustomer(string CustomerName, string CustomerSurName, string NIF)
        {

            ViewBag.Message = "Clientes";
            var customers = LoadCustomers(CustomerName, CustomerSurName, NIF);

            return PartialView("ListCustomer", customers);
        }

        private IList<Customer> LoadCustomers(string CustomerName, string CustomerSurName, string NIF)
        {
            var customers = this.CreateQuery<Customer>(Proyection.Basic);

            if (!string.IsNullOrEmpty(CustomerName))
                customers.Where(CustomerFields.CustomerName, OperatorLite.Contains, CustomerName);
            if (!string.IsNullOrEmpty(CustomerSurName))
                customers.And(CustomerFields.CustomerSurName, OperatorLite.Contains, CustomerSurName);
            if (!string.IsNullOrEmpty(NIF))
                customers.And(CustomerFields.CustomerNIF, OperatorLite.Contains, NIF);

            return customers.ToList();
        }


        [HttpPost]
        public ActionResult SaveInvoice(Invoice invoice)
        {
            var invoi = this.CreateQuery<Invoice>(Proyection.Basic)
                .Where(InvoiceFields.InvoiceId, invoice.InvoiceId).ToList().FirstOrDefault();

            invoice.InvoiceIVA = 18;
            if (invoi != null)
                this.Repository.Update(invoice);
            else
            {
                
                this.Repository.Insert(invoice);
            }
                        
            ViewBag.Provinces = this.CreateQuery<Province>(Proyection.Basic).ToList();
            ViewBag.Cities = this.CreateQuery<City>(Proyection.Basic).Where(CityFields.ProvinceId, invoice.ProvinceId).ToList();
            ViewBag.Customers = new List<Customer>();
            ViewBag.InvoiceLine = new InvoiceLine();
            
            return this.Json(new { result = "success" , Invoice = invoice});
        }

        [HttpPost]
        public ActionResult SaveInvoiceLine(int? InvoiceID, string ProductReference, string ProductName ,decimal ProductCost, int InvoiceLineQuantity, double? InvoiceLineDiscount)
        {
            var invoiceLine = new InvoiceLine();

            var product = new Product();
            if (InvoiceID.HasValue) invoiceLine.InvoiceId = InvoiceID;
            if (!string.IsNullOrEmpty(ProductReference)) product.ProductReference = ProductReference;
            
            product.ProductName = ProductName;
            product.ProductCost = ProductCost;

            this.Repository.Insert(product);            

            invoiceLine.ProductId = product.ProductId;
            invoiceLine.InvoiceLineQuantity = InvoiceLineQuantity;
            invoiceLine.InvoiceLineDiscount = InvoiceLineDiscount;
            var total = product.ProductCost * (decimal) invoiceLine.InvoiceLineQuantity;
            if (InvoiceLineDiscount.HasValue)
                invoiceLine.InvoiceLineTotal = total - (total * (decimal)invoiceLine.InvoiceLineDiscount / 100);
            else
                invoiceLine.InvoiceLineTotal = total;

            this.Repository.BeginTransaction();

            this.Repository.Insert(invoiceLine);
            
            TotalsInvoice(InvoiceID);

            this.Repository.Commit();

            
            return this.Json(new { result = "success" });
        }


        private void TotalsInvoice(int? InvoiceID)
        {
            var invoice = this.CreateQuery<Invoice>(Proyection.Basic).Where(InvoiceFields.InvoiceId, InvoiceID).ToList().FirstOrDefault();

            var invoiceLines = this.CreateQuery<InvoiceLine>(Proyection.Basic).Where(InvoiceLineFields.InvoiceId, InvoiceID).ToList();

            decimal total = 0;

            foreach (var line in invoiceLines)
            {
                total += (decimal)line.InvoiceLineTotal;
            }
            invoice.InvoiceNetTotal = total;
            invoice.InvoiceTotal = total + (total * invoice.InvoiceIVA / 100);

            this.Repository.Update(invoice);

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


        public ActionResult EditarLinea(int InvoiceLineId)
        {
            var invoiceLine = this.CreateQuery<InvoiceLine>(Proyection.Basic)
                .Where(InvoiceLineFields.InvoiceLineId, InvoiceLineId).ToList().FirstOrDefault();

            return PartialView("DatosLineaFactura",invoiceLine);
        }


        [HttpPost]
        public ActionResult SearchInvoices(int? InvoiceId, string CustomerName, DateTime? InvoiceDate, string NIF)
        {

            var invoices = this.CreateQuery<Invoice>(Proyection.Detailed);

            if (!string.IsNullOrEmpty(CustomerName))
                invoices.Where(InvoiceFields.CustomerName, OperatorLite.Contains, CustomerName);
            if (InvoiceId.HasValue)
                invoices.And(InvoiceFields.InvoiceId, InvoiceId);
            if (InvoiceDate != null)
                invoices.And(InvoiceFields.InvoiceDate, InvoiceDate);
            if (!string.IsNullOrEmpty(CustomerName))
                invoices.And(InvoiceFields.InvoiceCustomerNIF, OperatorLite.Contains, NIF);


            return PartialView("List",invoices.ToList());
        }
        

    }
}
