using ClearstreamWeb.Models.TimeKeeping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeltyAutomation.TimeKeeping.Interfaces;

namespace ClearstreamWeb.Controllers {
    public class HomeController : Controller {
        ITimeKeepingApplication _app;

        public HomeController(ITimeKeepingApplication app)
        {
            _app = app;
        }
        // GET: Time
        public async Task<ActionResult> Index() {
            ViewBag.IsTimeAdmin = true;
            return View();
        }

        public async Task<ActionResult> Admin() {
            return View();
        }
    }
}