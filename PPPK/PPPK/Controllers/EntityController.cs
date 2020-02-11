using PPPK.Services;
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
        // GET: Entity
        public ActionResult Index()
        {
            service.GetServices(1);
            return View();
        }
    }
}