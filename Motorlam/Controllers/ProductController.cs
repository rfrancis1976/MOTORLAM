using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using inercya.ORMLite;
using Motorlam.Controllers;
using Motorlam.Entities;
using Motorlam.Extenders;
using inercya.Gataca.Web.Models;
using System.Globalization;



namespace Motorlam.Controllers
{
    public class ProductController : MotorlamController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Productos";
            ConfigurePage();
            var products = new List<Product>();
            return View(products);
        }


        public ActionResult Nuevo()
        {
            ViewBag.Message = "Productos";
            ConfigurePage();            
            return View(new Product());            
        }

        public ActionResult Editar(int ProductId)
        {
            ViewBag.Message = "Productos";
            ConfigurePage();
            var product = this.DataService.ProductRepository.CreateQuery(Proyection.Detailed).Where(ProductFields.ProductId, ProductId).ToList().FirstOrDefault();
            return View("Nuevo", product);
        }        

        [HttpPost]
        public ActionResult SalvarProducto(Product product, string TypeName, string BrandName)
        {
            ViewBag.Message = "Productos";

            if (!string.IsNullOrEmpty(TypeName))
            {
                var oldType = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic)
                    .Where(TypeProductFields.TypeProductName, TypeName).ToList().FirstOrDefault();
                if (oldType == null)
                {
                    TypeProduct type = new TypeProduct();
                    type.TypeProductName = TypeName;
                    SaveEntity(type);
                    product.TypeProductId = type.TypeProductId;
                }
                else
                {
                    product.TypeProductId = oldType.TypeProductId;
                }
            }

            if (!string.IsNullOrEmpty(BrandName))
            {
                var oldBp = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic)
                    .Where(BrandProductFields.BrandProductName, BrandName).ToList().FirstOrDefault();
                if (oldBp == null)
                {
                    BrandProduct bp = new BrandProduct();
                    bp.BrandProductName = BrandName;
                    SaveEntity(bp);
                    product.BrandProductId = bp.BrandProductId;
                }
                else
                {
                    product.BrandProductId = oldBp.BrandProductId;
                }
            }

            SaveEntity(product);
            return this.Json(new { result = "success" });          
        }


        public ActionResult Buscar(string ProductReference, string ProductName, int? SupplierId, int? BrandProductId, int? TypeProductId)
        {
            ViewBag.Message = "Productos";
            
            var products = this.DataService.ProductRepository.CreateQuery(Proyection.Detailed);

            if (!string.IsNullOrEmpty(ProductReference))
                products.Where(ProductFields.ProductReference, OperatorLite.Contains, ProductReference);
            if (!string.IsNullOrEmpty(ProductName))
                products.Where(ProductFields.ProductName, OperatorLite.Contains, ProductName);
            if (SupplierId.HasValue)
                products.And(ProductFields.SupplierId, SupplierId);
            if (BrandProductId.HasValue)
                products.And(ProductFields.BrandProductId, BrandProductId);
            if (TypeProductId.HasValue)
                products.And(ProductFields.TypeProductId, TypeProductId);


            return PartialView("List", products.OrderBy(ProductFields.ProductName).ToList());
        }

        [HttpPost]
        public ActionResult DeleteProduct(int Id)
        {
            var product = this.DataService.ProductRepository.CreateQuery(Proyection.Basic).Where(ProductFields.ProductId, Id).ToList().FirstOrDefault();

            try
            {
                if (product != null) this.DataService.Delete(product);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("ErrorSQL", "No se ha podido eliminar el Producto, porque esta asociado a una factura");
                return this.Json(new { result = "error", validationErrors = ModelState.GetErrors() });
            }
            
            return this.Json(new { result = "success" });
        }


        public ActionResult LoadProducts(string ProductReference, string ProductName, int? SupplierId, int? BrandProductId, int? TypeProductId)
        {           
            var products = this.DataService.ProductRepository.CreateQuery(Proyection.Detailed);

            if (!string.IsNullOrEmpty(ProductReference))
                products.Where(ProductFields.ProductReference, OperatorLite.Contains, ProductReference);
            if (!string.IsNullOrEmpty(ProductName))
                products.Where(ProductFields.ProductName, OperatorLite.Contains, ProductName);
            if (SupplierId.HasValue)
                products.And(ProductFields.SupplierId, SupplierId);
            if (BrandProductId.HasValue)
                products.And(ProductFields.BrandProductId, BrandProductId);
            if (TypeProductId.HasValue)
                products.And(ProductFields.TypeProductId, TypeProductId);

            return PartialView("List", products.OrderBy(ProductFields.ProductName).ToList());
        }

             
        [HttpGet]
        public ActionResult LoadCityByProvince(int fatherId)
        {
            var ChildItems = LoadCities(fatherId);

            return this.Json(new { result = "success", ChildItems }, JsonRequestBehavior.AllowGet);
        }

        private IList<ChildItem> LoadCities(int fatherId)
        {
            var cities = this.DataService.CityRepository.CreateQuery(Proyection.Basic).Where(CityFields.ProvinceId, fatherId).ToList();
            var ChildItems = ChildItem.GetChildItems(new SelectList(cities, CityFields.CityId, CityFields.CityName));
            return ChildItems;            
        }

        private void ConfigurePage()
        {
            CultureInfo culturaPersonal = new CultureInfo("es-ES");
            culturaPersonal.NumberFormat.CurrencyDecimalDigits = 2;
            culturaPersonal.NumberFormat.PercentDecimalDigits = 2;
            culturaPersonal.NumberFormat.NumberDecimalDigits = 2;

            ViewBag.Suppliers = this.DataService.SupplierRepository.CreateQuery(Proyection.Basic).OrderBy(SupplierFields.SupplierName).ToList();
            ViewBag.BrandsProduct = this.DataService.BrandProductRepository.CreateQuery(Proyection.Basic).OrderBy(BrandProductFields.BrandProductName).ToList();
            ViewBag.TypeProducts = this.DataService.TypeProductRepository.CreateQuery(Proyection.Basic).OrderBy(TypeProductFields.TypeProductName).ToList();
        }
    }
}
