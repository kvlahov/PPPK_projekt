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
    public class SqlConnectionController : Controller
    {
        private readonly SqlConnectionService _service;
        public SqlConnectionController()
        {
            _service = new SqlConnectionService();
            var a = new XmlService();
            //a.CreateBackup();
            a.ImportBackup();
        }
        public ActionResult Index()
        {
            var viewModel = _service.GetSqlConnectionViewModel();
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult GetAllTravelOrders()
        {
            return Json(new { data = _service.GetSqlConnectionViewModel().TravelOrderListViewModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTravelOrderForm(long? id)
        {
            var viewModel = _service.GetTravelOrderViewModel(id);
            return PartialView("_TravelOrderForm", viewModel);
        }

        [HttpPost]
        public ActionResult InsertTravelOrder(TravelOrderViewModel model)
        {
            var success = _service.InsertTravelOrder(model.TravelOrder);
            return Json(new { success });
        }

        [HttpPatch]
        public ActionResult UpdateTravelOrder(TravelOrderViewModel model)
        {
            var success = _service.UpdateTravelOrder(model.TravelOrder);
            return Json(new { success });
        }

        [HttpDelete]
        public ActionResult DeleteTravelOrder(long id)
        {
            var success = _service.RemoveTravelOrder(id);
            return Json(new { success });
        }

        //drivers
        [HttpGet]
        public ActionResult GetAllDrivers()
        {
            return Json(new { data = _service.GetSqlConnectionViewModel().DriverListViewModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDriverForm(long? id)
        {
            var vm = id.HasValue ? _service.GetDriver(id.Value) : new Driver();
            return PartialView("_DriverForm", vm);
        }

        [HttpPost]
        public ActionResult InsertDriver(Driver model)
        {
            var success = _service.InsertDriver(model);
            return Json(new { success });
        }

        [HttpPatch]
        public ActionResult UpdateDriver(Driver model)
        {
            var success = _service.UpdateDriver(model); ;
            return Json(new { success });
        }

        [HttpDelete]
        public ActionResult DeleteDriver(long id)
        {
            var success = _service.DeleteDriver(id); ;
            return Json(new { success });
        }

        //vehicles

        [HttpGet]
        public ActionResult GetAllVehicles()
        {
            return Json(new { data = _service.GetSqlConnectionViewModel().VehicleListViewModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetVehicleForm(long? id)
        {
            var vm = id.HasValue ? _service.GetVehicle(id.Value) : new Vehicle();
            return PartialView("_VehicleForm", vm);

        }

        [HttpPost]
        public ActionResult InsertVehicle(Vehicle model)
        {
            var success = _service.InsertVehicle(model);
            return Json(new { success });
        }

        [HttpPatch]
        public ActionResult UpdateVehicle(Vehicle model)
        {
            var success = _service.UpdateVehicle(model);
            return Json(new { success });
        }

        [HttpDelete]
        public ActionResult DeleteVehicle(long id)
        {
            var success = _service.DeleteVehicle(id);
            return Json(new { success });
        }
    }
}