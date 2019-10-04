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
    public class ClientsOfCarWashViewsController : Controller
    {
        private ClientsOfCarWashServices _services;
        private DetailingsServises _detailings;


        public ClientsOfCarWashViewsController(ClientsOfCarWashServices clients, DetailingsServises detailingsView)
        {
            _services = clients;
            _detailings = detailingsView;
        }

        // GET: ClientsOfCarWashViews
        public ActionResult Client()
        {
            var RedirectModel = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(_services.GetAll());
            return View(RedirectModel );
        }
                
        public ActionResult Checkout()
        {
            var RedirectModel = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(_services.GetAll());
            return View(RedirectModel);
        }

        // GET: ClientsOfCarWashViews/Details/5
        public ActionResult NewOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var CustomerOrders = Mapper.Map<ClientsOfCarWashView>(_services.CustomerOrders(id));

            ViewBag.Detailings = Mapper.Map<IEnumerable<DetailingsView>>(_detailings.GetAll());

            if (CustomerOrders == null)
            {
                return HttpNotFound();
            }

            return View(CustomerOrders);
        }

        #region
        //// GET: ClientsOfCarWashViews/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClientsOfCarWashView clientsOfCarWashView = db.ClientsOfCarWash.Find(id);
        //    if (clientsOfCarWashView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clientsOfCarWashView);
        //}

        //// GET: ClientsOfCarWashViews/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ClientsOfCarWashViews/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ib,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumderCar,IdClientsGroups,Idmark,Idmodel,IdBody,note,barcode")] ClientsOfCarWashView clientsOfCarWashView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ClientsOfCarWash.Add(clientsOfCarWashView);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(clientsOfCarWashView);
        //}

        //// GET: ClientsOfCarWashViews/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClientsOfCarWashView clientsOfCarWashView = db.ClientsOfCarWash.Find(id);
        //    if (clientsOfCarWashView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clientsOfCarWashView);
        //}

        //// POST: ClientsOfCarWashViews/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ib,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumderCar,IdClientsGroups,Idmark,Idmodel,IdBody,note,barcode")] ClientsOfCarWashView clientsOfCarWashView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(clientsOfCarWashView).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(clientsOfCarWashView);
        //}

        //// GET: ClientsOfCarWashViews/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClientsOfCarWashView clientsOfCarWashView = db.ClientsOfCarWash.Find(id);
        //    if (clientsOfCarWashView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clientsOfCarWashView);
        //}

        //// POST: ClientsOfCarWashViews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ClientsOfCarWashView clientsOfCarWashView = db.ClientsOfCarWash.Find(id);
        //    db.ClientsOfCarWash.Remove(clientsOfCarWashView);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
