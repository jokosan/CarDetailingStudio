using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    public class DetailingsController : Controller
    {
        private IDetailingsServises _detailings;
        private IGroupWashServices _groupWashServices;

        public DetailingsController(IDetailingsServises detailings, IGroupWashServices groupWashServices)
        {
            _detailings = detailings;
            _groupWashServices = groupWashServices;
        }

        // GET: Detailings
        public ActionResult PriceList(int page = 1)
        {
            return View(Mapper.Map<IEnumerable<DetailingsView>>(_detailings.GetAll()));
        }


        public ActionResult AddServises()
        {
            ViewBag.GroupServises = new SelectList(_groupWashServices.GetAllTable(), "Id", "group");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddServises([Bind(Include = "Id,services_list,validity,note,S,M,L,XL,group,status,currency,mark,IdGroupWashServices,IdTypeService")] DetailingsView price)
        {
            if (ModelState.IsValid)
            {
                DetailingsBll detailings = Mapper.Map<DetailingsView, DetailingsBll>(price);
                _detailings.AddNewServices(detailings);
            }

            ViewBag.GroupServises = new SelectList(_groupWashServices.GetAllTable(), "Id", "group");

            return RedirectToAction("PriceList");
        }

        public ActionResult EditServises(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailingsView detailings = Mapper.Map<DetailingsView>(_detailings.GetId(id));

            ViewBag.GroupServises = new SelectList(_groupWashServices.GetAllTable(), "Id", "group");

            if (detailings == null)
            {
                return HttpNotFound();
            }

            return View(detailings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditServises([Bind(Include = "Id,services_list,validity,note,S,M,L,XL,group,status,currency,mark,IdGroupWashServices,IdTypeService")] DetailingsView price)
        {
            if (ModelState.IsValid)
            {
                DetailingsBll detailings = Mapper.Map<DetailingsView, DetailingsBll>(price);
                _detailings.UpdateServices(detailings);
            }

            ViewBag.GroupServises = new SelectList(_groupWashServices.GetAllTable(), "Id", "group");

            return RedirectToAction("PriceList");
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
