using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers
{
    public class TireServiceController : Controller
    {
        private ITireService _tireService;
       

        public TireServiceController(
            ITireService tireService)
           
        {
            _tireService = tireService;
           
        }

        // GET: TireService
        public async Task<ActionResult> Index()
        {
            var tireServiceViews = Mapper.Map<IEnumerable<TireServiceView>>(await _tireService.GetTableAll());
            return View(tireServiceViews);
        }

        // GET: TireService/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TireServiceView tireServiceView = Mapper.Map<TireServiceView>(await _tireService.SelectId(id));
            if (tireServiceView == null)
            {
                return HttpNotFound();
            }
            return View(tireServiceView);
        }

        // GET: TireService/Create
        public ActionResult Create()
        {
            //ViewBag.orderServicesCarWashId = new SelectList(db.OrderServicesCarWashViews, "Id", "Id");
            return View();
        }

        // POST: TireService/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idTireService,clientsOfCarWashId,orderServicesCarWashId,priceListTireFittingAdditionalServicesId,numberOfTires,tireRadius,priceTireFitting")] TireServiceView tireServiceView)
        {
            if (ModelState.IsValid)
            {
                TireServiceBll tireService = Mapper.Map<TireServiceView, TireServiceBll>(tireServiceView);
                await _tireService.Insert(tireService);
                return RedirectToAction("Index");
            }

           // ViewBag.orderServicesCarWashId = new SelectList(db.OrderServicesCarWashViews, "Id", "Id", tireServiceView.orderServicesCarWashId);
            return View(tireServiceView);
        }

        // GET: TireService/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TireServiceView tireServiceView = Mapper.Map<TireServiceView>(await _tireService.SelectId(id));
            if (tireServiceView == null)
            {
                return HttpNotFound();
            }
            //ViewBag.orderServicesCarWashId = new SelectList(db.OrderServicesCarWashViews, "Id", "Id", tireServiceView.orderServicesCarWashId);
            return View(tireServiceView);
        }

        // POST: TireService/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idTireService,clientsOfCarWashId,orderServicesCarWashId,priceListTireFittingAdditionalServicesId,numberOfTires,tireRadius,priceTireFitting")] TireServiceView tireServiceView)
        {
            if (ModelState.IsValid)
            {
                TireServiceBll tireService = Mapper.Map<TireServiceView, TireServiceBll>(tireServiceView);
                await _tireService.Update(tireService);
                return RedirectToAction("Index");
            }
            //ViewBag.orderServicesCarWashId = new SelectList(db.OrderServicesCarWashViews, "Id", "Id", tireServiceView.orderServicesCarWashId);
            return View(tireServiceView);
        }   

    }
}
