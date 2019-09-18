using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.DataBase.db;

namespace CarDetailingStudio.Controllers
{
    public class OrderServicesCarWashesController : Controller
    {
        private OrderServicesCarWashServices _servicesOrders;

        public OrderServicesCarWashesController(OrderServicesCarWashServices order)
        {
            _servicesOrders = order;
        }

        // GET: OrderServicesCarWashes
        public ActionResult Index()
        {
            //var orderServicesCarWash = db.OrderServicesCarWash.Include(o => o.brigadeForToday).Include(o => o.ClientsOfCarWash).Include(o => o.ServisesCarWashOrder);
            IEnumerable<OrderServicesCarWash> orderServicesTable = _servicesOrders.GetAll();
            return View(orderServicesTable);
        }

        // GET: OrderServicesCarWashes/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OrderServicesCarWash orderServicesCarWash = db.OrderServicesCarWash.Find(id);
            //if (orderServicesCarWash == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(orderServicesCarWash);
            throw new NotImplementedException();
        }

        // GET: OrderServicesCarWashes/Create
        public ActionResult Create()
        {
            ViewBag.idCarWashWorkers = new SelectList(_servicesOrders.GetAll(), "id", "EarlyTermination");
            ViewBag.IdClientsOfCarWash = new SelectList(_servicesOrders.GetAll(), "ib", "Surname");
            ViewBag.IdServisesCarWashOrder = new SelectList(_servicesOrders.GetAll(), "Id", "Id");
            return View();        
        }

        // POST: OrderServicesCarWashes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,idClientDEL,IdClientsOfCarWash,IdServisesCarWashOrder,idCarWashWorkers,discount,StatusOrder,PaymentState,OrderDate,ExecutionTime,ClosingData,ClosingTime,TotalCostOfAllServices")] OrderServicesCarWash orderServicesCarWash)
        {
            //if (ModelState.IsValid)
            //{
            //    db.OrderServicesCarWash.Add(orderServicesCarWash);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            IEnumerable<OrderServicesCarWash> orderServicesTable = _servicesOrders.GetAll();
            ViewBag.idCarWashWorkers = new SelectList(orderServicesTable, "id", "EarlyTermination", orderServicesCarWash.idCarWashWorkers);
            ViewBag.IdClientsOfCarWash = new SelectList(orderServicesTable, "ib", "Surname", orderServicesCarWash.IdClientsOfCarWash);
            ViewBag.IdServisesCarWashOrder = new SelectList(orderServicesTable, "Id", "Id", orderServicesCarWash.IdServisesCarWashOrder);
            return View(orderServicesCarWash);

        }

        // GET: OrderServicesCarWashes/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OrderServicesCarWash orderServicesCarWash = db.OrderServicesCarWash.Find(id);
            //if (orderServicesCarWash == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.idCarWashWorkers = new SelectList(db.brigadeForToday, "id", "EarlyTermination", orderServicesCarWash.idCarWashWorkers);
            //ViewBag.IdClientsOfCarWash = new SelectList(db.ClientsOfCarWash, "ib", "Surname", orderServicesCarWash.IdClientsOfCarWash);
            //ViewBag.IdServisesCarWashOrder = new SelectList(db.ServisesCarWashOrder, "Id", "Id", orderServicesCarWash.IdServisesCarWashOrder);
            //return View(orderServicesCarWash);
            throw new NotImplementedException();
        }

        // POST: OrderServicesCarWashes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,idClientDEL,IdClientsOfCarWash,IdServisesCarWashOrder,idCarWashWorkers,discount,StatusOrder,PaymentState,OrderDate,ExecutionTime,ClosingData,ClosingTime,TotalCostOfAllServices")] OrderServicesCarWash orderServicesCarWash)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(orderServicesCarWash).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.idCarWashWorkers = new SelectList(db.brigadeForToday, "id", "EarlyTermination", orderServicesCarWash.idCarWashWorkers);
            //ViewBag.IdClientsOfCarWash = new SelectList(db.ClientsOfCarWash, "ib", "Surname", orderServicesCarWash.IdClientsOfCarWash);
            //ViewBag.IdServisesCarWashOrder = new SelectList(db.ServisesCarWashOrder, "Id", "Id", orderServicesCarWash.IdServisesCarWashOrder);
            //return View(orderServicesCarWash);
            throw new NotImplementedException();
        }

        // GET: OrderServicesCarWashes/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OrderServicesCarWash orderServicesCarWash = db.OrderServicesCarWash.Find(id);
            //if (orderServicesCarWash == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(orderServicesCarWash);
            throw new NotImplementedException();
        }

        // POST: OrderServicesCarWashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //OrderServicesCarWash orderServicesCarWash = db.OrderServicesCarWash.Find(id);
            //db.OrderServicesCarWash.Remove(orderServicesCarWash);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _servicesOrders.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
