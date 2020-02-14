using PPPK.Models;
using PPPK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPPK.Controllers
{
    public class DataSetController : Controller
    {
        private readonly DataSetService service;
        private readonly XmlService xmlService;
        public DataSetController()
        {
            service = new DataSetService();
            xmlService = new XmlService();
        }

        // GET: DataSet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateBackup()
        {
            var success = xmlService.CreateBackup();
            var message = success ? "Backup sucessfuly created" : "Backup failed to create";
            return Json(new { success, message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClearDatabase()
        {
            var success = xmlService.ClearDatabase();
            var message = success ? "Database sucessfuly cleared" : "Database failed to clear";
            return Json(new { success, message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RestoreBackup()
        {
            var success = xmlService.ImportBackup();
            var message = success ? "Database sucessfuly restored" : "Database failed to restore";
            return Json(new { success, message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTravelOrders()
        {
            var travelOrders = service.GetTravelOrders();
            return Json(new { data = travelOrders }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoutesForTravelOrder(int id)
        {
            var routes = service.GetRouteInfoesForTravelOrder(id);
            ViewBag.TravelOrderID = id;
            return PartialView("_RouteInfoes", routes);
        }

        public ActionResult GetRouteInfoForm(int? id)
        {
            var model = service.GetRouteInfo(id);
            return PartialView("_RouteInfoForm", model);
        }

        [HttpPost]
        public ActionResult InsertRouteInfo(RouteInfo model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = service.InsertRouteInfo(model) });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
        }

        [HttpPatch]
        public ActionResult UpdateRouteInfo(RouteInfo model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = service.UpdateRouteInfo(model) });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
        }

        [HttpDelete]
        public ActionResult DeleteRouteInfo(int id)
        {
            return Json(new { success = service.DeleteRouteInfo(id) });
        }

    }
}