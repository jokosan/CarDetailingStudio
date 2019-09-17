using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.IServices;
using CarDetailingStudio.BLL.Services.ServiceLogic;
using CarDetailingStudio.DataBase.db;
using CarDetailingStudio.Models.AuxiliaryModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    public class CarWashWorkersController : Controller
    {       
        private CarWashWorkersServices _ServicesClass;
        private FormationOfTheCurrentShift _Formation;

        public CarWashWorkersController(CarWashWorkersServices servicesClass, FormationOfTheCurrentShift formationOfTheCurrent)
        {
            _ServicesClass = servicesClass;
            _Formation = formationOfTheCurrent;
        }

        
        // GET: CarWashWorkers
        public ActionResult Index()
        {
            IEnumerable<CarWashWorkers> dbCarWashWorkers = _ServicesClass.GetChooseEmployees();
            return View(dbCarWashWorkers);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            _Formation.AddToCurrentShift(form);
            return Redirect("/Home/Index");
        }

        // GET: CarWashWorkers/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CarWashWorkers carWashWorkers = db.CarWashWorkers.Find(id);
            //if (carWashWorkers == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(carWashWorkers);
            throw new NotImplementedException();
        }

        // GET: CarWashWorkers/Create
        public ActionResult Create()
        {
            return View(_ServicesClass.GetChooseEmployees());
        }

        // POST: CarWashWorkers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idKey,Name,Surname,Patronymic,MobilePhone,Experience,DataRegistration,DataDismissal,status,Photo,IdPosition")] CarWashWorkers carWashWorkers)
        {
            if (ModelState.IsValid)
            {

                _ServicesClass.Create(carWashWorkers);

                return RedirectToAction("Index");
            }

            //ViewBag.IdPosition = new SelectList(_ServicesClass.JobTitleTable, "Id", "Position", carWashWorkers.IdPosition);
            return View(carWashWorkers);
            throw new NotImplementedException();
        }

        // GET: CarWashWorkers/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CarWashWorkers carWashWorkers = db.CarWashWorkers.Find(id);
            //if (carWashWorkers == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.IdPosition = new SelectList(db.JobTitleTable, "Id", "Position", carWashWorkers.IdPosition);
            //return View(carWashWorkers);
            throw new NotImplementedException();
        }

        // POST: CarWashWorkers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idKey,Name,Surname,Patronymic,MobilePhone,Experience,DataRegistration,DataDismissal,status,Photo,IdPosition")] CarWashWorkers carWashWorkers)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(carWashWorkers).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.IdPosition = new SelectList(db.JobTitleTable, "Id", "Position", carWashWorkers.IdPosition);
            //return View(carWashWorkers);
            throw new NotImplementedException();
        }

        // GET: CarWashWorkers/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CarWashWorkers carWashWorkers = db.CarWashWorkers.Find(id);
            //if (carWashWorkers == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(carWashWorkers);
            throw new NotImplementedException();
        }

        // POST: CarWashWorkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //CarWashWorkers carWashWorkers = db.CarWashWorkers.Find(id);
            //db.CarWashWorkers.Remove(carWashWorkers);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            throw new NotImplementedException();
        }
    }
}
