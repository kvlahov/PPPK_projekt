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
        public ActionResult GetTravelOrderForm(long? id)
        {
            var viewModel = _service.GetTravelOrderViewModel(id);
            return PartialView("_InsertTravelOrder", viewModel);
        }

        [HttpPost]
        public ActionResult InsertTravelOrderForm(TravelOrderViewModel model)
        {
            return RedirectToAction("Index");
        }
        [HttpPatch]
        public ActionResult UpdateTravelOrderForm(TravelOrder model)
        {
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult DeleteTravelOrderForm(long id)
        {
            return RedirectToAction("Index");
        }
    }
}