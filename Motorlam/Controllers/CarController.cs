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
    public class CarController : MotorlamController
    {
        public ActionResult Index()
        {
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).OrderBy(BrandFields.BrandName).ToList();
            var models = new List<Model>();
            ViewBag.Models = new List<Model>();
            return View(models);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).OrderBy(BrandFields.BrandName).ToList();
            ViewBag.Models = new List<Model>();
            return View(new ModelMotor());            
        }

        public ActionResult EditarModel(int ModelMotorId)
        {
            var model = this.DataService.ModelMotorRepository.CreateQuery(Proyection.Basic).Where(ModelFields.ModelMotorId, ModelMotorId).ToList().FirstOrDefault();
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).OrderBy(BrandFields.BrandName).ToList();
            ViewBag.Models = this.DataService.ModelRepository.CreateQuery(Proyection.Basic).Where(ModelFields.BrandId, model.BrandId).ToList();            
            return View("Nuevo", model);
        }


        public ActionResult SalvarModelo(ModelMotor model)
        {
            this.DataService.BeginTransaction();

            if (model.ModelMotorId < 1)
            {
                Motor newMotor = new Motor();
                newMotor.MotorName = model.MotorName;
                if (model.MotorType != "") newMotor.MotorType = model.MotorType;
                SaveEntity(newMotor);
                model.MotorId = newMotor.MotorId;
            }
            else
            {
                Motor motor = new Motor();
                motor.MotorId = (int)model.MotorId;
                motor.MotorName = model.MotorName;
                motor.MotorType = model.MotorType;
                SaveEntity(motor);
            }

            SaveEntity(model);

            this.DataService.Commit();
            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).OrderBy(BrandFields.BrandName).ToList();
            ViewBag.Models = new List<Model>();
            return View("Nuevo",model);           
        }

        [HttpPost]
        public ActionResult DeleteModelMotor(int Id)
        {
            var cars = this.DataService.CarRepository.CreateQuery(Proyection.Basic).Where(CarFields.ModelMotorId, Id).ToList();

            if (cars.Count > 0)
            {
                this.ModelState.AddModelError("ErrorSQL", "No se ha podido eliminar este coche, porque esta asociado a un cliente");
                return this.Json(new { result = "error", validationErrors = ModelState.GetErrors() });
            }
            else
            {
                this.Repository.BeginTransaction();

                var modelmotor = this.DataService.ModelMotorRepository.CreateQuery(Proyection.Basic).Get(Id);

                if (modelmotor.MotorId != null)
                {
                    var motor = this.DataService.MotorRepository.CreateQuery(Proyection.Basic).Get(modelmotor.MotorId);
                    this.DataService.Delete(motor);
                }

                this.DataService.Delete(modelmotor);

                this.DataService.Commit();

                return this.Json(new { result = "success" });
            }
        }

        public ActionResult Buscar(int? BrandId, int? ModelId)
        {
            var models = this.DataService.ModelRepository.CreateQuery(Proyection.Detailed);

            if (BrandId.HasValue) models.Where(ModelFields.BrandId, BrandId);

            if (ModelId.HasValue) models.Where(ModelFields.ModelId, ModelId);

            return PartialView("List", models.OrderBy(ModelFields.BrandName, ModelFields.ModelName).ToList());
        }

        public ActionResult LoadCars(int? BrandId, int? ModelId)
        {
            var models = this.DataService.ModelRepository.CreateQuery(Proyection.Detailed);
            
            if (BrandId.HasValue) models.Where(ModelFields.BrandId, BrandId);

            if (ModelId.HasValue) models.Where(ModelFields.ModelId, ModelId);

            return PartialView("List", models.OrderBy(ModelFields.BrandName, ModelFields.ModelName).ToList());
        }

        [HttpPost]
        public ActionResult SaveModel(int BrandId, string NewModelName)
        {
            Model newModel = new Model();
            this.DataService.BeginTransaction();
            if (!string.IsNullOrEmpty(NewModelName))
            {
                var oldmodel = this.DataService.ModelRepository.CreateQuery(Proyection.Basic).Where(ModelFields.ModelName, NewModelName).ToList().FirstOrDefault();

                if (oldmodel == null)
                {
                    newModel.BrandId = BrandId;
                    newModel.ModelName = NewModelName;
                    
                    SaveEntity(newModel);
                }
            }
            this.DataService.Commit();

            ViewBag.Brands = this.DataService.BrandRepository.CreateQuery(Proyection.Basic).OrderBy(BrandFields.BrandName).ToList();
            ViewBag.Models = this.DataService.ModelRepository.CreateQuery(Proyection.Basic).Where(ModelFields.BrandId, BrandId).OrderBy(ModelFields.ModelName).ToList();

            return this.Json(new{ result = "success" });
        }

        [HttpGet]
        public ActionResult LoadModelByBrand(int fatherId)
        {
            var ChildItems = LoadModels(fatherId);

            return this.Json(new { result = "success", ChildItems }, JsonRequestBehavior.AllowGet);
        }

        private IList<ChildItem> LoadModels(int fatherId)
        {
            var models = this.DataService.ModelRepository.CreateQuery(Proyection.Basic).Where(ModelFields.BrandId, fatherId).OrderBy(ModelFields.ModelName).ToList();
            var ChildItems = ChildItem.GetChildItems(new SelectList(models, ModelFields.ModelId, ModelFields.ModelName));
            return ChildItems;
        }
    }
}
