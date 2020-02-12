using PPPK.Models;
using PPPK.Services;
using PPPK.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPPK.Controllers
{
    public class EntityController : Controller
    {
        private EntityService service;
        public EntityController()
        {
            service = new EntityService();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetVehicles()
        {
            List<Vehicle> vehicles = service.GetAllVehicles();
            return Json(new { data = vehicles }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServicesForVehicle(int id)
        {
            List<ServiceInfo> services = service.GetServicesForVehicle(id);
            ViewBag.VehicleID = id;
            return PartialView("_ServiceInfoes", services);
            //return Json(new { data = services }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceForm(int? id)
        {
            ServiceInfo model = service.GetService(id);
            return PartialView("_ServiceForm", model);
        }

        [HttpPost]
        public ActionResult InsertService(ServiceInfo model)
        {
            
            return Json(new { success = service.InsertService(model) });
        }

        [HttpPatch]
        public ActionResult UpdateService(ServiceInfo model)
        {
            return Json(new { success = service.UpdateService(model) });
        }

        [HttpDelete]
        public ActionResult DeleteService(int id)
        {
            return Json(new { success = service.DeleteService(id) });
        }

        public ActionResult GenerateReportForVehicle(int id)
        {
            VehicleViewModel vm = service.GetVehicleViewModel(id);
            return PartialView("_VehicleReport", vm);
        }
    }
}