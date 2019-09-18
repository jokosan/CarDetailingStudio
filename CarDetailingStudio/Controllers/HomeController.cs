using CarDetailingStudio.BLL.Services.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    public class HomeController : Controller
    {
        private EntryCondition _entryCondition;

        public HomeController(EntryCondition entry)
        {
            _entryCondition = entry;
        }

        public ActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }

            bool redirect = _entryCondition.HomeEntryCondition();

            if (!redirect)
            {
                return Redirect("/CarWashWorkers/Index");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}