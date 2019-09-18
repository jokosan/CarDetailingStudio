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
    public class ClientsOfCarWashesController : Controller
    {
        private carWashEntities db = new carWashEntities();

        // GET: ClientsOfCarWashes
        public ActionResult Index()
        {
            var clientsOfCarWash = db.ClientsOfCarWash.Include(c => c.car_mark).Include(c => c.car_model).Include(c => c.CarBody).Include(c => c.ClientsGroups);
            return View(clientsOfCarWash.ToList());
        }

        // GET: ClientsOfCarWashes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientsOfCarWash clientsOfCarWash = db.ClientsOfCarWash.Find(id);
            if (clientsOfCarWash == null)
            {
                return HttpNotFound();
            }
            return View(clientsOfCarWash);
        }

        // GET: ClientsOfCarWashes/Create
        public ActionResult Create()
        {
            ViewBag.Idmark = new SelectList(db.car_mark, "id_car_mark", "name");
            ViewBag.Idmodel = new SelectList(db.car_model, "id_car_model", "name");
            ViewBag.IdBody = new SelectList(db.CarBody, "Id", "Name");
            ViewBag.IdClientsGroups = new SelectList(db.ClientsGroups, "Id", "Name");
            return View();
        }

        // POST: ClientsOfCarWashes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ib,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumderCar,IdClientsGroups,Idmark,Idmodel,IdBody,note,barcode")] ClientsOfCarWash clientsOfCarWash)
        {
            if (ModelState.IsValid)
            {
                db.ClientsOfCarWash.Add(clientsOfCarWash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idmark = new SelectList(db.car_mark, "id_car_mark", "name", clientsOfCarWash.Idmark);
            ViewBag.Idmodel = new SelectList(db.car_model, "id_car_model", "name", clientsOfCarWash.Idmodel);
            ViewBag.IdBody = new SelectList(db.CarBody, "Id", "Name", clientsOfCarWash.IdBody);
            ViewBag.IdClientsGroups = new SelectList(db.ClientsGroups, "Id", "Name", clientsOfCarWash.IdClientsGroups);
            return View(clientsOfCarWash);
        }

        // GET: ClientsOfCarWashes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientsOfCarWash clientsOfCarWash = db.ClientsOfCarWash.Find(id);
            if (clientsOfCarWash == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idmark = new SelectList(db.car_mark, "id_car_mark", "name", clientsOfCarWash.Idmark);
            ViewBag.Idmodel = new SelectList(db.car_model, "id_car_model", "name", clientsOfCarWash.Idmodel);
            ViewBag.IdBody = new SelectList(db.CarBody, "Id", "Name", clientsOfCarWash.IdBody);
            ViewBag.IdClientsGroups = new SelectList(db.ClientsGroups, "Id", "Name", clientsOfCarWash.IdClientsGroups);
            return View(clientsOfCarWash);
        }

        // POST: ClientsOfCarWashes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ib,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumderCar,IdClientsGroups,Idmark,Idmodel,IdBody,note,barcode")] ClientsOfCarWash clientsOfCarWash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientsOfCarWash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idmark = new SelectList(db.car_mark, "id_car_mark", "name", clientsOfCarWash.Idmark);
            ViewBag.Idmodel = new SelectList(db.car_model, "id_car_model", "name", clientsOfCarWash.Idmodel);
            ViewBag.IdBody = new SelectList(db.CarBody, "Id", "Name", clientsOfCarWash.IdBody);
            ViewBag.IdClientsGroups = new SelectList(db.ClientsGroups, "Id", "Name", clientsOfCarWash.IdClientsGroups);
            return View(clientsOfCarWash);
        }

        // GET: ClientsOfCarWashes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientsOfCarWash clientsOfCarWash = db.ClientsOfCarWash.Find(id);
            if (clientsOfCarWash == null)
            {
                return HttpNotFound();
            }
            return View(clientsOfCarWash);
        }

        // POST: ClientsOfCarWashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientsOfCarWash clientsOfCarWash = db.ClientsOfCarWash.Find(id);
            db.ClientsOfCarWash.Remove(clientsOfCarWash);
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
