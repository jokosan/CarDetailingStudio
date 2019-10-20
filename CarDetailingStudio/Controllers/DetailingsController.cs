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
using CarDetailingStudio.BLL.Services.Contract;

namespace CarDetailingStudio.Controllers
{
    public class DetailingsController : Controller
    {
        private IDetailingsServises _detailings;

        public DetailingsController(IDetailingsServises detailings)
        {
            _detailings = detailings;
        }
            
        // GET: Detailings
        public ActionResult PriceList(int page = 1)
        {
            return View(Mapper.Map<IEnumerable<DetailingsView>>(_detailings.GetAll()));
        }


        public ActionResult EditOrder()//int OrderId
        {
            return View(Mapper.Map<IEnumerable<DetailingsView>>(_detailings.Converter()));
        }

        [HttpPost]
        public void EditOrder(List<string> idServices 
            //int idOrder, 
            //int idClient
            )
        {
            var x = idServices.Count();

            
          //  List<ServisesCarWashOrderBll> servises = Mapper.Map<List<ServisesCarWashOrderView>, List<ServisesCarWashOrderBll>>(idServices);

           // _services.ServisesInsert(servises, idOrder, idClient);
        }

    }
}
