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
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers.TireStorage
{
    public class TireStorageServicesController : Controller
    {
        private ITireStorageServices _tireStorageServices;

        public TireStorageServicesController(
            ITireStorageServices tireStorageServices)
        {
            _tireStorageServices = tireStorageServices;
        }

        // GET: TireStorageServices
        public async Task<ActionResult> Price()
        {
            return View(Mapper.Map<IEnumerable<TireStorageServicesView>>(await _tireStorageServices.GetTableAll()));
        }

        // GET: TireStorageServices/Create
        public ActionResult PriceCreate()
        {
            return View();
        }

        // POST: TireStorageServices/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PriceCreate([Bind(Include = "Id,ServicesName,radius,amount,storageTime,Price")] TireStorageServicesView tireStorageServicesView)
        {
            if (ModelState.IsValid)
            {
                await _tireStorageServices.Insert(TransformEntity(tireStorageServicesView));
                return RedirectToAction("Index");
            }

            return View(tireStorageServicesView);
        }

        // GET: TireStorageServices/Edit/5
        public async Task<ActionResult> PriceEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TireStorageServicesView tireStorageServicesView = Mapper.Map<TireStorageServicesView>(await _tireStorageServices.SelectId(id));
            if (tireStorageServicesView == null)
            {
                return HttpNotFound();
            }
            return View(tireStorageServicesView);
        }

        // POST: TireStorageServices/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PriceEdit([Bind(Include = "Id,ServicesName,radius,amount,storageTime,Price")] TireStorageServicesView tireStorageServicesView)
        {
            if (ModelState.IsValid)
            {
                await _tireStorageServices.Update(TransformEntity(tireStorageServicesView));
                return RedirectToAction("Index");
            }
            return View(tireStorageServicesView);
        }

        private TireStorageServicesBll TransformEntity(TireStorageServicesView entity) => Mapper.Map<TireStorageServicesView, TireStorageServicesBll>(entity);


    }
}
