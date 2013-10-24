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

namespace Motorlam.Controllers
{
    public class SupplierController : MotorlamController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Proveedores";

            var supliers = new List<Supplier>();
            
            return View(supliers);
        }


        public ActionResult Nuevo()
        {
            ViewBag.Message = "Proveedores";

            var provinces = this.CreateQuery<Province>(Proyection.Basic).ToList();

            ViewBag.Provinces = provinces;
            ViewBag.Cities = new List<City>();

            return View(new Supplier());
            
        }

        public ActionResult Editar(int SupplierId)
        {
            ViewBag.Message = "Proveedores";
            var supplier = this.CreateQuery<Supplier>(Proyection.Detailed)
                .Where(SupplierFields.SupplierId, SupplierId).ToList().FirstOrDefault();
                        
            var provinces = this.CreateQuery<Province>(Proyection.Basic).ToList();
            ViewBag.Provinces = provinces;
            var cities = this.CreateQuery<City>(Proyection.Basic).ToList();
            ViewBag.Cities = cities;

            return View("Nuevo", supplier);
        }


        public ActionResult SalvarProveedor(Supplier supplier)
        {
            ViewBag.Message = "Proveedores";

            if (supplier.SupplierId == 0)
                this.Repository.Insert(supplier);
            else
                this.Repository.Update(supplier);
                       
            var suppliers = new List<Supplier>();
            return View("Index", suppliers);           
        }


        public ActionResult Buscar(string SupplierName, string SupplierNIF)
        {
            ViewBag.Message = "Proveedores";
           
            var suppliers = this.CreateQuery<Supplier>(Proyection.Basic);

            if (!string.IsNullOrEmpty(SupplierName))
                suppliers.Where(SupplierFields.SupplierName, OperatorLite.Contains, SupplierName);
            if (!string.IsNullOrEmpty(SupplierNIF))
                suppliers.And(SupplierFields.SupplierNIF, OperatorLite.Contains, SupplierNIF);

           
            return PartialView("List", suppliers.ToList());
        }

        [HttpPost]
        public ActionResult DeleteSupplier(int Id)
        {
            var products = this.CreateQuery<Product>(Proyection.Basic)
                .Where(ProductFields.SupplierId,Id).ToList();

            if (products.Count > 0)
            {
                ModelState.AddModelError("ErrorSql", "Este proveedor no puede ser eliminado, porque tiene productos asociados");
                return this.Json(new { result = "error", validationErrors = ModelState.GetErrors() });
            }
            else
            {
                var supplier = this.CreateQuery<Supplier>(Proyection.Basic).Get(Id);

                if (supplier != null) this.Repository.Delete(supplier);

                var suppliers = this.CreateQuery<Supplier>(Proyection.Detailed).ToList();

                return this.Json(new { result = "success" });
                
            }
        }

        public ActionResult LoadSuppliers(string SupplierName, string SupplierNIF)
        {
            var suppliers = this.CreateQuery<Supplier>(Proyection.Basic);

            if (!string.IsNullOrEmpty(SupplierName))
                suppliers.Where(SupplierFields.SupplierName, OperatorLite.Contains, SupplierName);
            if (!string.IsNullOrEmpty(SupplierNIF))
                suppliers.And(SupplierFields.SupplierNIF, OperatorLite.Contains, SupplierNIF);

            return PartialView("List",suppliers.ToList());
        }

       
        [HttpGet]
        public ActionResult LoadCityByProvince(int fatherId)
        {
            var ChildItems = LoadCities(fatherId);

            return this.Json(new { result = "success", ChildItems }, JsonRequestBehavior.AllowGet);
        }

        private IList<ChildItem> LoadCities(int fatherId)
        {
            var cities = this.CreateQuery<City>(Proyection.Basic)
                .Where(CityFields.ProvinceId, fatherId).ToList();
            var ChildItems = ChildItem.GetChildItems(new SelectList(cities, CityFields.CityId, CityFields.CityName));
            return ChildItems;
            
        }     
    }
}
