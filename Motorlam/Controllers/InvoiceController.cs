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
using System.IO;
using SpreadsheetGear;
using msExcel = Microsoft.Office.Interop.Excel;


namespace Motorlam.Controllers
{
    public class InvoiceController : MotorlamController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "MotorLam";
            CultureInfo culturaPersonal = new CultureInfo("es-ES");
            culturaPersonal.NumberFormat.CurrencyDecimalDigits = 2;
            culturaPersonal.NumberFormat.PercentDecimalDigits = 2;
            culturaPersonal.NumberFormat.NumberDecimalDigits = 2;
            
            // Saca en un principio las facturas que estan sin Pagar
            var invoices = this.DataService.InvoiceRepository.CreateQuery(Proyection.Detailed)
                .Where(InvoiceFields.IsInvoicePaid, false)
                .OrderBy(InvoiceFields.InvoiceNumber)
                .ToList();

            return View(invoices);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Message = "Facturas";

            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Suppliers = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.BrandProducts = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.LastInvoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).ToList().LastOrDefault();
            ViewBag.TypeProducts = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList();

            ViewBag.Suppliers1 = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.BrandProducts1 = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts1 = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList();
     
            ViewBag.Models = new List<Model>();
            ViewBag.Cities = new List<City>();
            ViewBag.Customers = new List<Customer>();
            ViewBag.InvoiceLines = new List<InvoiceLine>();
            ViewBag.InvoiceLine = new InvoiceLine();
            ViewBag.Cars = new List<Car>();
            ViewBag.Tab = 1;
            //ViewBag.Iva = this.DataService.IvaRepository.CreateQuery(Proyection.Basic).ToList();
            return View(new Invoice());

        }

        [HttpPost]
        public ActionResult OpenFileInvoice(int Id)
        {
            string str = PrintExcell(Id);
            return this.Json(new { result = "success", file = str });            
        }

        public ActionResult Editar(int InvoiceId, int tab)
        {
            ViewBag.Message = "Facturas";

            var invoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Detailed).Where(InvoiceFields.InvoiceId, InvoiceId).ToList().FirstOrDefault();

            string carName = "";
            string carRack = "";

            if (invoice.CarId != null)
            {
                var car = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Where(CarFields.CarId, invoice.CarId).ToList().FirstOrDefault();
                if (car != null)
                {
                    if (car.BrandName != null) carName = car.BrandName;
                    if (car.ModelName != null) carName = carName + " " + car.ModelName;
                    if (car.MotorName != null) carName = carName + " " + car.MotorName;
                    if (car.CarRack != null) carRack = car.CarRack;
                }
            }
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();
            
            ViewBag.Models = new List<Model>();

            ViewBag.CarName = carName;
            ViewBag.CarRack = carRack;
            ViewBag.Tab = tab;

            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Suppliers = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Cars = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Where(CarFields.CustomerId, invoice.CustomerId).ToList();
            ViewBag.BrandProducts = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList();

            ViewBag.Suppliers1 = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.BrandsProduct1 = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts1 = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList();

            ViewBag.Cities = this.DataService.CityRepository.CreateQuery(Proyection.Basic).Where(CityFields.ProvinceId,invoice.ProvinceId).ToList();
            ViewBag.Customers = new List<Customer>();
            ViewBag.InvoiceLines = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic).Where(InvoiceLineFields.InvoiceId, InvoiceId).ToList();
            ViewBag.InvoiceLine = new InvoiceLine();
            ViewBag.LastInvoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).ToList().LastOrDefault();
            //ViewBag.Iva = this.DataService.IvaRepository.CreateQuery(Proyection.Basic).ToList();
            if (invoice.InvoiceIVAId.HasValue)
            {
                ViewBag.InvoiceIVA = invoice.InvoiceIVA;
            }

            return View("Nuevo",invoice);
        }

        [HttpPost]
        public ActionResult LoadInvoiceData(int InvoiceId)
        {
            ViewBag.Message = "Facturas";

            var invoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Detailed).Where(InvoiceFields.InvoiceId, InvoiceId).ToList().FirstOrDefault();

            string carName = "";
            string carRack = "";

            if (invoice.CarId != null)
            {
                var car = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Where(CarFields.CarId, invoice.CarId).ToList().FirstOrDefault();
                if (car != null)
                {
                    if (car.BrandName != null) carName = car.BrandName;
                    if (car.ModelName != null) carName = carName + " " + car.ModelName;
                    if (car.MotorName != null) carName = carName + " " + car.MotorName;
                    if (car.CarRack != null) carRack = car.CarRack;
                }
            }
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();

            ViewBag.Models = new List<Model>();

            ViewBag.CarName = carName;
            ViewBag.CarRack = carRack;
            ViewBag.Tab = 1;

            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Suppliers = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Cars = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Where(CarFields.CustomerId, invoice.CustomerId).ToList();
            ViewBag.BrandProducts = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList();

            ViewBag.Suppliers1 = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.BrandsProduct1 = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts1 = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList();

            ViewBag.Cities = this.DataService.CityRepository.CreateQuery(Proyection.Basic).Where(CityFields.ProvinceId, invoice.ProvinceId).ToList();
            ViewBag.Customers = new List<Customer>();
            ViewBag.InvoiceLines = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic).Where(InvoiceLineFields.InvoiceId, InvoiceId).ToList();
            ViewBag.InvoiceLine = new InvoiceLine();
            ViewBag.LastInvoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).ToList().LastOrDefault();
            //ViewBag.Iva = this.DataService.IvaRepository.CreateQuery(Proyection.Basic).ToList();
            if (invoice.InvoiceIVAId.HasValue)
            {
                ViewBag.InvoiceIVA = invoice.InvoiceIVA;
            }

            return PartialView("~/Views/Invoice/DatosFactura.cshtml", invoice);
        }

        [HttpPost]
        public ActionResult DeleteInvoice(int Id, int? InvoiceId, string CustomerName, DateTime? InvoiceDate, string NIF, int IsInvoicePaid)
        {
            this.DataService.BeginTransaction();
            var invoiceLines = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic)
                .Where(InvoiceLineFields.InvoiceId, Id).ToList();

            if (invoiceLines != null)
            {
                foreach (var line in invoiceLines)
                {
                    this.DataService.Delete(line);
                }
            }

            var invoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).Get(Id);

            this.DataService.Delete(invoice);

            this.DataService.Commit();

            var invoices = this.DataService.InvoiceRepository.CreateQuery(Proyection.Detailed);

            if (!string.IsNullOrEmpty(CustomerName))
                invoices.Where(InvoiceFields.CustomerName, OperatorLite.Contains, CustomerName);
            if (InvoiceId.HasValue)
                invoices.And(InvoiceFields.InvoiceId, InvoiceId);
            if (InvoiceDate != null)
                invoices.And(InvoiceFields.InvoiceDate, InvoiceDate);
            if (!string.IsNullOrEmpty(CustomerName))
                invoices.And(InvoiceFields.InvoiceCustomerNIF, OperatorLite.Contains, NIF);
            if (IsInvoicePaid == 1)
                invoices.And(InvoiceFields.IsInvoicePaid, true);
            else if (IsInvoicePaid == 2)
                invoices.And(InvoiceFields.IsInvoicePaid, false);
            
            return PartialView("List", invoices.ToList());
        }


        public ActionResult DeleteInvoiceLine(int ID)
        {
            var line = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic).Get(ID);

            if (line != null) this.DataService.Delete(line);

            TotalsInvoice(line.InvoiceId);

            var invoiceLines = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic).Where(InvoiceLineFields.InvoiceId, line.InvoiceId).ToList();

            return PartialView("OrderList", invoiceLines);
        }

        [HttpPost]
        public ActionResult LoadCustomer(int CustomerID)
        {
            var customer = this.DataService.CustomerRepository.CreateQuery(Proyection.Detailed).Get(CustomerID);
            var cities = this.DataService.CityRepository.CreateQuery(Proyection.Basic).Where(CityFields.ProvinceId, customer.ProvinceId).ToList();
            return this.Json(new { result = "success", Customer = customer, Cities = cities });
        }

        [HttpPost]
        public ActionResult LoadCar(int CarID)
        {
            var car = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Get(CarID);
           
            return this.Json(new { result = "success", Car = car});
        }

        [HttpPost]
        public ActionResult LoadProduct(int ProductID)
        {
            var product = this.DataService.ProductRepository.CreateQuery(Proyection.Detailed).Get(ProductID);

            return this.Json(new { result = "success", Product = product });
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
            var customers = this.DataService.CustomerRepository.CreateQuery(Proyection.Basic);

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
            this.DataService.BeginTransaction();

            if (invoice.InvoiceId != 0)
                this.DataService.Update(invoice);
            else
                this.DataService.Insert(invoice);

            if (invoice.InvoiceKilometres != null && invoice.InvoiceKilometres != "")
            {
                var car = this.DataService.CarRepository.CreateQuery(Proyection.Basic).Where(CarFields.CarId, invoice.CarId).ToList().FirstOrDefault();
                car.CarKilometres = invoice.InvoiceKilometres;
                this.DataService.Update(car);
            }

            PrintExcell(invoice.InvoiceId);

            this.DataService.Commit();
            
            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Cities = this.DataService.CityRepository.CreateQuery(Proyection.Basic).Where(CityFields.ProvinceId, invoice.ProvinceId).ToList();
            ViewBag.Customers = new List<Customer>();
            ViewBag.InvoiceLine = new InvoiceLine();
            ViewBag.LastInvoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).ToList().LastOrDefault();

            return this.Json(new { result = "success" , Invoice = invoice});
        }

        [HttpPost]
        public ActionResult SaveInvoiceLine(int? InvoiceLineId, int? InvoiceID, int? ProductId, string ProductReference,
                                            string ProductName, double? InvoiceProductValue, double? InvoiceLineQuantity,
                                            double? InvoiceLineDiscount, int? SupplierId, int? BrandProductId, int? TypeProductId)
        {
            
            var invoiceLine = new InvoiceLine();

            if (InvoiceLineId > 0)
            {
                invoiceLine = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic)
                    .Where(InvoiceLineFields.InvoiceLineId, InvoiceLineId).ToList().FirstOrDefault();
            }
            
            var product = new Product();
            if (ProductId.HasValue) product = this.DataService.ProductRepository.CreateQuery(Proyection.Basic).Where(ProductFields.ProductId, ProductId).ToList().FirstOrDefault();
            if (InvoiceID.HasValue) invoiceLine.InvoiceId = InvoiceID;
            if (!string.IsNullOrEmpty(ProductReference)) product.ProductReference = ProductReference;

            product.ProductName = ProductName;
            product.ProductCost = (decimal)InvoiceProductValue;

            if (SupplierId.HasValue) product.SupplierId = SupplierId;
            if (BrandProductId.HasValue) product.BrandProductId = BrandProductId;
            if (TypeProductId.HasValue) product.TypeProductId = TypeProductId;

            if (ProductId.HasValue) this.DataService.Update(product);
            else this.DataService.Insert(product);

            
            invoiceLine.ProductId = product.ProductId;
            invoiceLine.InvoiceLineQuantity = InvoiceLineQuantity;
            invoiceLine.InvoiceProductValue = (decimal)InvoiceProductValue;
            if (InvoiceLineDiscount.HasValue)
                invoiceLine.InvoiceLineDiscount = InvoiceLineDiscount/100;
            var total = (decimal)InvoiceProductValue * (decimal)invoiceLine.InvoiceLineQuantity;
            if (InvoiceLineDiscount.HasValue)
            {
                invoiceLine.InvoiceLineTotal = total - (total * (decimal)invoiceLine.InvoiceLineDiscount);
                invoiceLine.InvoideLineTotalDiscount = total * (decimal)invoiceLine.InvoiceLineDiscount;
            }
            else
            {
                invoiceLine.InvoiceLineDiscount = 0;
                invoiceLine.InvoiceLineTotal = total;
            }

            this.DataService.BeginTransaction();
            if (InvoiceLineId > 0)
            {
                this.DataService.Update(invoiceLine);
            }
            else
            {
                this.DataService.Insert(invoiceLine);
            }
            TotalsInvoice(InvoiceID);

            this.DataService.Commit();
            
            return this.Json(new { result = "success" });
        }

        [HttpPost]
        public ActionResult SalvarCoche(Car car, int CustomerId)
        {
            car.CustomerId = CustomerId;
            SaveEntity(car);
            var newCar = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Get(car.CarId);
            return this.Json(new { result = "success", Car = newCar });
        }

        private void TotalsInvoice(int? InvoiceID)
        {
            var invoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).Where(InvoiceFields.InvoiceId, InvoiceID).ToList().FirstOrDefault();

            var invoiceLines = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic).Where(InvoiceLineFields.InvoiceId, InvoiceID).ToList();

            decimal total = 0;
            double totaldiscount = 0;

            foreach (var line in invoiceLines)
            {
                total += (decimal)line.InvoiceLineTotal;
                if (line.InvoiceLineDiscount > 0)
                {
                    totaldiscount += (double)line.ProductCost * (double)line.InvoiceLineDiscount;
                }

            }
            invoice.InvoiceNetTotal = total;
            invoice.InvoiceTotal = total + (total * invoice.InvoiceIVA / 100);
            invoice.InvoiceTotalDiscount = (decimal)totaldiscount;

            this.DataService.Update(invoice);
        }

        public ActionResult LoadInvoice(int InvoiceId)
        {
            var invoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic)
                .Where(InvoiceFields.InvoiceId, InvoiceId).ToList().FirstOrDefault();
            ViewBag.InvoiceLines = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic)
                .Where(InvoiceLineFields.InvoiceId, InvoiceId).ToList();
            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Cities = new List<City>();
            ViewBag.Customers = new List<Customer>();

            return View("Nuevo",invoice);
        }


        public ActionResult EditarLinea(int InvoiceLineId)
        {
            var invoiceLine = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic)
                .Where(InvoiceLineFields.InvoiceLineId, InvoiceLineId).ToList().FirstOrDefault();

            ViewBag.BrandProducts  = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Suppliers = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.TypeProducts = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).ToList(); 

            return PartialView("DatosLineaFactura",invoiceLine);
        }


        [HttpPost]
        public ActionResult SearchInvoices(string InvoiceNumber, string CustomerName, DateTime? InvoiceDate, string NIF, string CarRack, int IsInvoicePaid)
        {

            var invoices = this.DataService.InvoiceRepository.CreateQuery(Proyection.Detailed);


            if (!string.IsNullOrEmpty(CustomerName))
                invoices.And(InvoiceFields.CustomerName, OperatorLite.Contains, CustomerName);
            if (!string.IsNullOrEmpty(InvoiceNumber))
                invoices.And(InvoiceFields.InvoiceNumber,OperatorLite.Contains,InvoiceNumber);
            if (InvoiceDate != null)
                invoices.And(InvoiceFields.InvoiceDate, InvoiceDate);
            if (!string.IsNullOrEmpty(CustomerName))
                invoices.And(InvoiceFields.InvoiceCustomerNIF, OperatorLite.Contains, NIF);
            if (!string.IsNullOrEmpty(CarRack))
            {
                var car = this.DataService.CarRepository.CreateQuery(Proyection.Basic).Where(CarFields.CarRack, OperatorLite.Equals, CarRack.ToUpper()).ToList().FirstOrDefault();
                if (car!=null) invoices.And(InvoiceFields.CarId, car.CarId);
                else invoices.And(InvoiceFields.CarId, -1);
            }

            if (IsInvoicePaid == 1)
                invoices.And(InvoiceFields.IsInvoicePaid, true);
            else if (IsInvoicePaid == 2)
                invoices.And(InvoiceFields.IsInvoicePaid, false);

            return PartialView("List",invoices.OrderByDesc(InvoiceFields.InvoiceNumber).ToList());
        }


        [HttpPost]
        public ActionResult SearchProduct(int? BrandProductId, string ProductName, int? SupplierId, int? TypeProductId)
        {
            var products = LoadProducts(BrandProductId, ProductName, SupplierId, TypeProductId);

            return PartialView("ListProduct", products);
        }

        private IList<Product> LoadProducts(int? BrandProductId, string ProductName, int? SupplierId, int? TypeProductId)
        {
            var products = this.DataService.ProductRepository.CreateQuery(Proyection.Detailed);

            if (!string.IsNullOrEmpty(ProductName))
                products.Where(ProductFields.ProductName, OperatorLite.Contains, ProductName);
            if (BrandProductId.HasValue)
                products.And(ProductFields.BrandProductId, BrandProductId);
            if (SupplierId.HasValue)
                products.And(ProductFields.SupplierId, SupplierId);
            if (TypeProductId.HasValue)
                products.And(ProductFields.TypeProductId, TypeProductId);

            return products.ToList();
        }

        [HttpPost]
        public ActionResult LoadProductByReference(string ProductReference)
        {

            var product = this.DataService.ProductRepository.CreateQuery(Proyection.Detailed).Where(ProductFields.ProductReference, ProductReference).ToList().FirstOrDefault();

            if (product != null) return this.Json(new { result = "success", Product = product });
            else return this.Json(new { result = "error"});
        }

        [HttpPost]
        public ActionResult LoadCarsByCustomer(int CustomerId)
        {
            var cars = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Where(CarFields.CustomerId, CustomerId).ToList();
            var brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Brands = brands;
            ViewBag.Models = new List<Model>();

            return PartialView("ListCar", cars);
        }

        public string PrintExcell(int id)
        {
            string invoiceNumber;
            double? Iva;
            string str1 = "";
            try
            {
                Invoice invoice = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).Get(id);
                var list = this.DataService.InvoiceLineRepository.CreateQuery(Proyection.Basic)
                    .Where(InvoiceLineFields.InvoiceId, invoice.InvoiceId).OrderBy(InvoiceLineFields.InvoiceLineId).ToList();
                Car car = this.DataService.CarRepository.CreateQuery(Proyection.Basic).Where(CarFields.CarId, invoice.CarId).ToList().FirstOrDefault();

                string FilePath = Server.MapPath("~/Facturas/Factura nº " + invoice.InvoiceNumber + " - " + invoice.InvoiceCustomerName + ".xls");
                string FilePDFPath = Server.MapPath("~/Facturas/Factura nº " + invoice.InvoiceNumber + " - " + invoice.InvoiceCustomerName + ".pdf");
                FileInfo fileInfo = new FileInfo(FilePath);
                if (fileInfo != null)
                {
                    fileInfo.Delete();
                }

                string TemplateFilePath = Server.MapPath("~/Template/plantilla.xls");

                System.IO.File.Copy(TemplateFilePath, FilePath);
                System.IO.File.SetAttributes(FilePath, FileAttributes.Normal);

                IWorkbook workbook = Factory.GetWorkbook(FilePath);
                IWorksheet item = workbook.Worksheets["Factura"];
                item.Cells["C6"].Value = invoice.InvoiceNumber;
                item.Cells["C7"].Value = invoice.InvoiceDate2;
                item.Cells["B35"].Value = invoice.InvoiceTypePaid;
                IRange ranges = item.Cells["E37"];
                int? ivaValor = invoice.IvaValor;
                if (ivaValor.HasValue)
                {
                    Iva = (double)ivaValor.GetValueOrDefault() * 0.01;
                }
                else
                {
                    Iva = null;
                }
                item.Cells["E37"].Value = Iva;
                item.Cells["C12"].Value = string.Concat(invoice.CustomerName, " ", invoice.CustomerSurName);
                item.Cells["C13"].Value = invoice.InvoiceCustomerAddress;
                item.Cells["C14"].Value = string.Concat(invoice.InvoiceCustomerCodPostal, " ", invoice.CityName);
                item.Cells["C15"].Value = invoice.ProvinceName;
                item.Cells["C16"].Value = invoice.InvoiceCustomerNIF;
                item.Cells["E13"].Value = car.CarRack;
                item.Cells["G13"].Value = string.Concat(invoice.InvoiceKilometres, " km.");
                int num = 18;
                string productName = "";
                decimal value = new decimal(0);
                foreach (InvoiceLine invoiceLine in list)
                {
                    if (!invoiceLine.ProductName.Contains("MANO DE OBRA"))
                    {
                        item.Cells[num, 2].Value = invoiceLine.ProductName;
                        item.Cells[num, 4].Value = invoiceLine.InvoiceLineQuantity;
                        item.Cells[num, 6].Value = invoiceLine.InvoiceProductValue;
                        item.Cells[num, 5].Value = invoiceLine.InvoiceLineDiscount;
                        num++;
                    }
                    else
                    {
                        productName = invoiceLine.ProductName;
                        value = invoiceLine.InvoiceProductValue.Value;
                    }
                }
                item.Cells[num, 2].Value = productName;
                item.Cells[num, 7].Value = value;
                workbook.Save();
                Random random = new Random();
                return "../../Facturas/Factura nº " + invoice.InvoiceNumber + " - " + invoice.InvoiceCustomerName + ".xls?nocache=" + random.ToString(); 
            }
            catch (Exception exception)
            {
                str1 = string.Concat("ERROR¬", exception.Message);
            }
            str1 = "";
            return str1;
        }
        
        public class ExcelToPdfConverter
        {

            private static object missing = System.Reflection.Missing.Value;

            public static void ConvertExcelToPdf(string excelFileIn, string pdfFileOut)
            {
                msExcel.Application excel = new msExcel.Application();
                try
                {
                    excel.Visible = false;
                    excel.ScreenUpdating = false;
                    excel.DisplayAlerts = false;

                    FileInfo excelFile = new FileInfo(excelFileIn);

                    string filename = excelFile.FullName;

                    msExcel.Workbook wbk = excel.Workbooks.Open(filename, missing,
                     missing, missing, missing, missing, missing,
                     missing, missing, missing, missing, missing,
                     missing, missing, missing);
                    wbk.Activate();

                    object outputFileName = pdfFileOut;
                    msExcel.XlFixedFormatType fileFormat = msExcel.XlFixedFormatType.xlTypePDF;

                    // Save document into PDF Format
                    wbk.ExportAsFixedFormat(fileFormat, outputFileName,
                    missing, missing, missing,
                    missing, missing, missing,
                    missing);

                    object saveChanges = msExcel.XlSaveAction.xlDoNotSaveChanges;
                    ((msExcel._Workbook)wbk).Close(saveChanges, missing, missing);
                    wbk = null;
                }
                catch(Exception ex)
                {
                   
                }
            }

        }
    }
}
