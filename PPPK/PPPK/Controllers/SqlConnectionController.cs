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
            return PartialView("_InsertTravelOrder", viewModel);
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
            return null;
            //return PartialView("_InsertTravelOrder", viewModel);
        }

        [HttpPost]
        public ActionResult InsertDriver(Driver model)
        {
            var success = false;
            return Json(new { success });
        }

        [HttpPatch]
        public ActionResult UpdateDriver(Driver model)
        {
            var success = false;
            return Json(new { success });
        }

        [HttpDelete]
        public ActionResult DeleteDriver(long id)
        {
            var success = false;
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
            return null;
            //return PartialView("_InsertTravelOrder", viewModel);
        }

        [HttpPost]
        public ActionResult InsertVehicle(Vehicle model)
        {
            var success = false;
            return Json(new { success });
        }

        [HttpPatch]
        public ActionResult UpdateVehicle(Vehicle model)
        {
            var success = false;
            return Json(new { success });
        }

        [HttpDelete]
        public ActionResult DeleteVehicle(long id)
        {
            var success = false;
            return Json(new { success });
        }
    }
}