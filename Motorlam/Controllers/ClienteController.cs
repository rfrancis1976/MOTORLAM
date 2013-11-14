using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Motorlam.Extenders;
using inercya.ORMLite;
using Motorlam.Controllers;
using Motorlam.Entities;
using inercya.Gataca.Web.Models;

namespace Motorlam.Controllers
{
    public class ClienteController : MotorlamController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Clientes";
            var customers = new List<Customer>();            
            return View(customers);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Message = "Clientes";
            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Cities = new List<City>();
            return View(new Customer());            
        }

        public ActionResult Editar(int CustomerID)
        {
            ViewBag.Message = "Clientes";
            var customer = this.DataService.CustomerRepository.CreateQuery(Proyection.Detailed).Where(CustomerFields.CustomerId, CustomerID).ToList().FirstOrDefault();
            var cars = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Where(CarFields.CustomerId, CustomerID).ToList();

            if (cars.Count > 0) ViewBag.Cars = cars;
            else ViewBag.Cars = new List<Car>();

            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Cities = this.DataService.CityRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Models = new List<Model>();
            ViewBag.Motors = new List<ModelMotor>();
            ViewBag.TypeMotors = new List<Motor>();
            return View("Nuevo",customer);
        }               

        public ActionResult SalvarCliente(Customer customer)
        {
            ViewBag.Message = "Clientes";

            if (customer.CustomerId == 0)
                this.DataService.Insert(customer);
            else
                this.DataService.Update(customer);

            var cars = this.DataService.CarRepository.CreateQuery(Proyection.Detailed)
                .Where(CarFields.CustomerId, customer.CustomerId).ToList();

            if (cars.Count > 0) ViewBag.Cars = cars;
            else ViewBag.Cars = new List<Car>();

            ViewBag.Provinces = this.DataService.ProvinceRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Cities = this.DataService.CityRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Models = new List<Model>();
            ViewBag.Motors = new List<ModelMotor>();
            ViewBag.TypeMotors = new List<Motor>();

            return View("Nuevo", customer);           
        }        

        public ActionResult Buscar(string CustomerName, string CustomerSurName, string NIF, string carRack)
        {
            ViewBag.Message = "Clientes";
            var customers = this.DataService.CustomerRepository.CreateQuery(Proyection.Basic);
            if (!string.IsNullOrEmpty(CustomerName))
                customers.Where(CustomerFields.CustomerName, OperatorLite.Contains, CustomerName);
            if (!string.IsNullOrEmpty(CustomerSurName))
                customers.And(CustomerFields.CustomerSurName, OperatorLite.Contains, CustomerSurName);
            if (!string.IsNullOrEmpty(NIF))
                customers.And(CustomerFields.CustomerNIF, OperatorLite.Contains, NIF);
            if (!string.IsNullOrEmpty(carRack))
            {
                var ids = this.DataService.CarRepository.CreateQuery(Proyection.Basic).Where(CarFields.CarRack, OperatorLite.Contains, carRack).ToList();
                customers.And(CustomerFields.CustomerId, OperatorLite.In, ids.Select(p => p.CustomerId));
            }

            return PartialView("List", customers.ToList());
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int Id)
        {
            var customer = this.DataService.CustomerRepository.CreateQuery(Proyection.Basic).Where(CustomerFields.CustomerId, Id).ToList().FirstOrDefault();
            var invoices = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).Where(InvoiceFields.CustomerId, Id).ToList();
            if (invoices.Count > 0)
            {
                ModelState.AddModelError("ErrorSql", "Este cliente no puede ser eliminado, porque tiene facturas asociadas");
                return this.Json(new { result = "error", validationErrors = ModelState.GetErrors() });
            }
            if (customer != null) this.DataService.Delete(customer);
            return this.Json(new { result = "success" });            
        }

        [HttpPost]
        public ActionResult DeleteCar(int Id)
        {
            var invoices = this.DataService.InvoiceRepository.CreateQuery(Proyection.Basic).Where(InvoiceFields.CarId, Id).ToList();
            if (invoices.Count > 0)
            {
                this.ModelState.AddModelError("ErrorSQL", "No se ha podido eliminar este coche, porque esta asociado a una factura");
                return this.Json(new { result = "error", validationErrors = ModelState.GetErrors() });
            }
            else
            {
                var car = this.DataService.CarRepository.CreateQuery(Proyection.Basic).Where(CarFields.CarId, Id).ToList().FirstOrDefault();
                if (car != null) this.DataService.Delete(car);               
                ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();
                ViewBag.Models = new List<Model>();
                ViewBag.Motors = new List<ModelMotor>();
                ViewBag.TypeMotors = new List<Motor>();
                return this.Json(new { result = "success" });
            }
        }
   
        [HttpPost]
        public ActionResult EditarCoche(int CarId)
        {
            var car = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Where(CarFields.CarId, CarId).ToList().FirstOrDefault();
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Models = this.DataService.ModelRepository.CreateQuery(Proyection.Basic).Where(ModelFields.BrandId, car.BrandId).ToList();
            ViewBag.Motors = this.DataService.ModelMotorRepository.CreateQuery(Proyection.Basic).Where(ModelMotorFields.ModelId, car.ModelId).ToList();
            ViewBag.TypeMotors = this.DataService.MotorRepository.CreateQuery(Proyection.Detailed).Where(MotorFields.ModelId, car.ModelId).ToList();
            return PartialView("DatosCoche", car);            
        }

        [HttpPost]
        public ActionResult SalvarCoche(Car car, int CustomerId)
        {
            car.CustomerId = CustomerId;
            SaveEntity(car);                      
            return this.Json(new { result = "success" });
        }                      

        public ActionResult LoadCustomers(string CustomerName, string CustomerSurName, string NIF)
        {
            var customers = this.DataService.CustomerRepository.CreateQuery(Proyection.Basic);

            if (!string.IsNullOrEmpty(CustomerName))
                customers.Where(CustomerFields.CustomerName, OperatorLite.Contains, CustomerName);
            if (!string.IsNullOrEmpty(CustomerSurName))
                customers.And(CustomerFields.CustomerSurName, OperatorLite.Contains, CustomerSurName);
            if (!string.IsNullOrEmpty(NIF))
                customers.And(CustomerFields.CustomerNIF, OperatorLite.Contains, NIF);

            return PartialView("List", customers.ToList());
        }
        
        public ActionResult LoadCars(int CustomerId)
        {
            var cars = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Where(CarFields.CustomerId, CustomerId).ToList();
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).ToList();
            ViewBag.Models = new List<Model>();
            ViewBag.Motors = new List<ModelMotor>();
            ViewBag.TypeMotors = new List<Motor>();
            return PartialView("ListCars", cars);
        }

        [HttpGet]
        public ActionResult LoadCityByProvince(int fatherId)
        {
            var ChildItems = LoadCities(fatherId);
            return this.Json(new { result = "success", ChildItems }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoadModelByBrand(int fatherId)
        {
            var ChildItems = LoadModels(fatherId);
            return this.Json(new { result = "success", ChildItems }, JsonRequestBehavior.AllowGet);
        }

        private IList<ChildItem> LoadModels(int fatherId)
        {
            var models = this.DataService.ModelRepository.CreateQuery(Proyection.Basic).Where(ModelFields.BrandId, fatherId).ToList();
            var ChildItems = ChildItem.GetChildItems(new SelectList(models, ModelFields.ModelId, ModelFields.ModelName));
            return ChildItems;            
        }

        [HttpGet]
        public ActionResult LoadMotorByModel(int fatherId)
        {
            var ChildItems = LoadMotors(fatherId);
            return this.Json(new { result = "success", ChildItems }, JsonRequestBehavior.AllowGet);
        }

        private IList<ChildItem> LoadMotors(int fatherId)
        {
            var motors = this.DataService.ModelMotorRepository.CreateQuery(Proyection.Basic)
                .Where(ModelMotorFields.ModelId, fatherId).ToList();
            return ChildItem.GetChildItems(new SelectList(motors, ModelMotorFields.ModelMotorId, ModelFields.MotorName));
        }

        [HttpGet]
        public ActionResult LoadMotorTypeByMotor(int fatherId)
        {
            var ChildItems = LoadTypeMotors(fatherId);
            return this.Json(new { result = "success", ChildItems }, JsonRequestBehavior.AllowGet);
        }

        private IList<ChildItem> LoadTypeMotors(int fatherId)
        {
            var modelmotor = this.DataService.ModelMotorRepository.CreateQuery(Proyection.Basic).Where(ModelMotorFields.ModelMotorId, fatherId).ToList().FirstOrDefault();
            var motors = this.DataService.MotorRepository.CreateQuery(Proyection.Detailed).Where(MotorFields.ModelId, modelmotor.ModelId).ToList();
            var ChildItems = ChildItem.GetChildItems(new SelectList(motors, MotorFields.MotorId, ModelFields.MotorType));
            return ChildItems;
        }

        private IList<ChildItem> LoadCities(int fatherId)
        {
            var cities = this.DataService.CityRepository.CreateQuery(Proyection.Basic).Where(CityFields.ProvinceId, fatherId).ToList();
            return ChildItem.GetChildItems(new SelectList(cities, CityFields.CityId, CityFields.CityName));
        }


        #region UPLOADFILE

        [HttpPost]
        public ActionResult Upload(int Id)
        {
            // Comprobamos si habia un archivo ya asociado y lo elminamos
            var file = this.DataService.FileRepository.CreateQuery(Proyection.Basic).Where(FileFields.EntityId, Id).ToList().FirstOrDefault();
            if (file != null) this.Repository.Delete(file);

            HttpPostedFileBase fileBase = Request.Files[0];

            this.DataService.BeginTransaction();

            byte[] fileData = GetFileByteArray(fileBase.InputStream);
            Entities.File doc = new Entities.File();
            doc.FileContent = fileData;
            doc.FileName = fileBase.FileName;
            doc.EntityId = Id;
            doc.CreatedDate = DateTime.Now;
            SaveEntity(doc);

            var car = this.DataService.CarRepository.CreateQuery(Proyection.Detailed).Get(Id);
            car.CarITVName = doc.FileName;
            car.CarITVId = doc.FileId;
            SaveEntity(car);

            this.DataService.Commit();

            return View();
        }

        public ActionResult DeleteFile(int Id)
        {
            var file = this.DataService.FileRepository.CreateQuery(Proyection.Basic).Where(FileFields.EntityId, Id).ToList().FirstOrDefault();
            this.DataService.BeginTransaction();
            this.DataService.Delete(file);
            var car = this.DataService.CarRepository.CreateQuery(Proyection.Basic).Get(Id);
            car.CarITVName = "";
            car.CarITVId = null;
            this.DataService.Update(car);
            this.DataService.Commit();
            return this.Json(new { result = "success", EntityId = Id });
        }

        public ActionResult LoadFile(int Id)
        {
            ViewBag.File  = this.DataService.FileRepository.CreateQuery(Proyection.Basic).Where(FileFields.EntityId, Id).ToList().FirstOrDefault();
            return this.Json(new { result = "success" });
        }

        public ActionResult OpenFile(int fileId)
        {
            var file = GetFile(fileId);
            return this.File(file.FileContent, "application/octet-stream", file.FileName);
        }

        private byte[] GetFileByteArray(System.IO.Stream stream)
        {
            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }

        private File GetFile(int Id)
        {
            var file = this.DataService.FileRepository.CreateQuery(Proyection.Basic).Where(FileFields.FileId, OperatorLite.Equals, Id).ToList().FirstOrDefault();
            return file;
        }

        #endregion
    }
}
