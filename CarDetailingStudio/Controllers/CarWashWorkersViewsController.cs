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
    public class CarWashWorkersViewsController : Controller
    {
        private CarWashWorkersServices _services;

        public CarWashWorkersViewsController(CarWashWorkersServices carWashWorkers)
        {           
            _services = carWashWorkers;
        }

        // GET: CarWashWorkersViews
        public ActionResult Index()
        {
            var ReirectModel = Mapper.Map<IEnumerable<CarWashWorkersView>>(_services.GetChooseEmployees());
            return View(ReirectModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            _services.AddToCurrentShift(form);
            return Redirect("/Order/Index");
        }

        public ActionResult Staff()
        {
            var StaffAll = Mapper.Map<IEnumerable<CarWashWorkersView>>(_services.GetStaffAll());
            return View(StaffAll);
        }

        //public ActionResult SalaryPreparation()
        //{
        //    var salary = Mapper.Map<IEnumerable<CarWashWorkersView>>(_services.Salary());
        //    return View();
        //}

        #region
        //// GET: CarWashWorkersViews/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CarWashWorkersView carWashWorkersView = db.CarWashWorkers.Find(id);
        //    if (carWashWorkersView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(carWashWorkersView);
        //}

        //// GET: CarWashWorkersViews/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: CarWashWorkersViews/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,idKey,Name,Surname,Patronymic,MobilePhone,Experience,DataRegistration,DataDismissal,status,Photo,IdPosition")] CarWashWorkersView carWashWorkersView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CarWashWorkers.Add(carWashWorkersView);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(carWashWorkersView);
        //}

        //// GET: CarWashWorkersViews/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CarWashWorkersView carWashWorkersView = db.CarWashWorkers.Find(id);
        //    if (carWashWorkersView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(carWashWorkersView);
        //}

        //// POST: CarWashWorkersViews/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,idKey,Name,Surname,Patronymic,MobilePhone,Experience,DataRegistration,DataDismissal,status,Photo,IdPosition")] CarWashWorkersView carWashWorkersView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(carWashWorkersView).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(carWashWorkersView);
        //}

        //// GET: CarWashWorkersViews/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CarWashWorkersView carWashWorkersView = db.CarWashWorkers.Find(id);
        //    if (carWashWorkersView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(carWashWorkersView);
        //}

        //// POST: CarWashWorkersViews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CarWashWorkersView carWashWorkersView = db.CarWashWorkers.Find(id);
        //    db.CarWashWorkers.Remove(carWashWorkersView);
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
