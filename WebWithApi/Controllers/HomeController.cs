using ClearstreamWeb.Models.TimeKeeping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeltyAutomation.M2M.QueryData;

namespace ClearstreamWeb.Controllers {
    public class HomeController : Controller {

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