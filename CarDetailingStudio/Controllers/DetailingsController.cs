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
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers
{
    public class DetailingsController : Controller
    {
        private DetailingsServises _detailings;
        private ServisesCarWashOrderServices _services;

        public DetailingsController(DetailingsServises detailings)
        {
            _detailings = detailings;
        }

        private int PageSize = 10;

        // GET: Detailings
        public ActionResult PriceList(int page = 1)
        {
            //return View(Mapper.Map<IEnumerable<DetailingsView>>(_services.GetAll()
            //    .Skip((page - 1) * PageSize)
            //    .Take(PageSize)));

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
