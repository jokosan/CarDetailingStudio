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
using CarDetailingStudio.Filters;

namespace CarDetailingStudio.Controllers
{
    // [WorkShiftFilter]
    public class OrderController : Controller
    {
        private OrderServicesCarWashServices _order;
        private ServisesCarWashOrderServices _servisesCarWash;

        public OrderController(OrderServicesCarWashServices orderServices, ServisesCarWashOrderServices servises)
        {
            _order = orderServices;
            _servisesCarWash = servises;

        }

        // GET: Order
        public ActionResult Index()
        {
            var RedirectModel = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(_order.GetAll());
            return View(RedirectModel);
        }

        // GET: Order/Details/5
        public ActionResult OrderInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var OrderOnfo = Mapper.Map<OrderServicesCarWashView>(_order.GetId(id));
            var info = Mapper.Map<IEnumerable<ServisesCarWashOrderView>>(_servisesCarWash.GetAll().Where(x => x.IdOrderServicesCarWash == id));

            ViewBag.ServisesInfo = info;

            if (OrderOnfo == null)
            {
                return HttpNotFound();
            }
            return View(OrderOnfo);
        }

        public ActionResult CloseOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var OrderOnfo = Mapper.Map<OrderServicesCarWashView>(_order.GetId(id));

            

            if (OrderOnfo == null)
            {
                return HttpNotFound();
            }
            return View(OrderOnfo);
        }

        #region
        //// GET: Order/Details/5
        //public ActionResult Details(int? id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //OrderServicesCarWashView orderServicesCarWashView = db.OrderServicesCarWash.Find(id);
        //    //if (orderServicesCarWashView == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //return View(orderServicesCarWashView);
        //    throw new NotImplementedException();
        //}

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdClientsOfCarWash,IdServisesCarWashOrder,idCarWashWorkers,discount,StatusOrder,PaymentState,OrderDate,ExecutionTime,ClosingData,ClosingTime,TotalCostOfAllServices")] OrderServicesCarWashView orderServicesCarWashView)
        {
            //    if (ModelState.IsValid)
            //    {
            //        db.OrderServicesCarWash.Add(orderServicesCarWashView);
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }

            //    return View(orderServicesCarWashView);
            throw new NotImplementedException();
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OrderServicesCarWashView orderServicesCarWashView = db.OrderServicesCarWash.Find(id);
            //if (orderServicesCarWashView == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(orderServicesCarWashView);
            throw new NotImplementedException();
        }

        // POST: Order/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdClientsOfCarWash,IdServisesCarWashOrder,idCarWashWorkers,discount,StatusOrder,PaymentState,OrderDate,ExecutionTime,ClosingData,ClosingTime,TotalCostOfAllServices")] OrderServicesCarWashView orderServicesCarWashView)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(orderServicesCarWashView).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(orderServicesCarWashView);
            throw new NotImplementedException();
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OrderServicesCarWashView orderServicesCarWashView = db.OrderServicesCarWash.Find(id);
            //if (orderServicesCarWashView == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(orderServicesCarWashView);
            throw new NotImplementedException();
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //OrderServicesCarWashView orderServicesCarWashView = db.OrderServicesCarWash.Find(id);
            //db.OrderServicesCarWash.Remove(orderServicesCarWashView);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            throw new NotImplementedException();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        #endregion
    }
}
