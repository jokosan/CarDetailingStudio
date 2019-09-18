using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.DataBase.db;

namespace CarDetailingStudio.Controllers
{
    public class ServisesCarWashOrdersController : Controller
    {
        private carWashEntities db = new carWashEntities();

        // GET: ServisesCarWashOrders
        public ActionResult Index()
        {
            var servisesCarWashOrder = db.ServisesCarWashOrder.Include(s => s.Detailings).Include(s => s.WashServices);
            return View(servisesCarWashOrder.ToList());
        }

        // GET: ServisesCarWashOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServisesCarWashOrder servisesCarWashOrder = db.ServisesCarWashOrder.Find(id);
            if (servisesCarWashOrder == null)
            {
                return HttpNotFound();
            }
            return View(servisesCarWashOrder);
        }

        // GET: ServisesCarWashOrders/Create
        public ActionResult Create()
        {
            ViewBag.IdWashServices = new SelectList(db.Detailings, "Id", "services_list");
            ViewBag.IdWashServices = new SelectList(db.WashServices, "id", "Services_list");
            return View();
        }

        // POST: ServisesCarWashOrders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdClientsOfCarWash,IdOrderServicesCarWash,idCarWashWorkers,IdWashServices,Price")] ServisesCarWashOrder servisesCarWashOrder)
        {
            if (ModelState.IsValid)
            {
                db.ServisesCarWashOrder.Add(servisesCarWashOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdWashServices = new SelectList(db.Detailings, "Id", "services_list", servisesCarWashOrder.IdWashServices);
            ViewBag.IdWashServices = new SelectList(db.WashServices, "id", "Services_list", servisesCarWashOrder.IdWashServices);
            return View(servisesCarWashOrder);
        }

        // GET: ServisesCarWashOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServisesCarWashOrder servisesCarWashOrder = db.ServisesCarWashOrder.Find(id);
            if (servisesCarWashOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdWashServices = new SelectList(db.Detailings, "Id", "services_list", servisesCarWashOrder.IdWashServices);
            ViewBag.IdWashServices = new SelectList(db.WashServices, "id", "Services_list", servisesCarWashOrder.IdWashServices);
            return View(servisesCarWashOrder);
        }

        // POST: ServisesCarWashOrders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdClientsOfCarWash,IdOrderServicesCarWash,idCarWashWorkers,IdWashServices,Price")] ServisesCarWashOrder servisesCarWashOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servisesCarWashOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdWashServices = new SelectList(db.Detailings, "Id", "services_list", servisesCarWashOrder.IdWashServices);
            ViewBag.IdWashServices = new SelectList(db.WashServices, "id", "Services_list", servisesCarWashOrder.IdWashServices);
            return View(servisesCarWashOrder);
        }

        // GET: ServisesCarWashOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServisesCarWashOrder servisesCarWashOrder = db.ServisesCarWashOrder.Find(id);
            if (servisesCarWashOrder == null)
            {
                return HttpNotFound();
            }
            return View(servisesCarWashOrder);
        }

        // POST: ServisesCarWashOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServisesCarWashOrder servisesCarWashOrder = db.ServisesCarWashOrder.Find(id);
            db.ServisesCarWashOrder.Remove(servisesCarWashOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
