using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;

namespace CarDetailingStudio.Controllers
{
    public class DetailingsController : Controller
    {
        private DetailingsServises _services;

        public DetailingsController(DetailingsServises detailings)
        {
            _services = detailings;
        }

        // GET: Detailings
        public ActionResult PriceList()
        {          
            return View(Mapper.Map<IEnumerable<DetailingsView>>(_services.GetAll()));
        }

     
    }
}
