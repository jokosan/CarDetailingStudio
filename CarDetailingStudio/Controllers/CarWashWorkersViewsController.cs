using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Modules.EmployeeSalary;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers
{
    public class CarWashWorkersViewsController : Controller
    {
        private ICarWashWorkersServices _services;
        private IJobTitleTableServices _job;

        public CarWashWorkersViewsController(ICarWashWorkersServices carWashWorkers, IJobTitleTableServices job)
        {
            _services = carWashWorkers;
            _job = job;
        }

        // GET: CarWashWorkersViews
        public ActionResult Index()
        {
            var ReirectModel = Mapper.Map<IEnumerable<CarWashWorkersView>>(_services.GetChooseEmployees());

            if (TempData.ContainsKey("BrigadeId"))
            {
                var resultBrigade = TempData["BrigadeId"] as IEnumerable<BrigadeForTodayView>;
                var result = new List<int>();

                foreach (var i in resultBrigade)
                    result.Add(i.IdCarWashWorkers.Value);

                return View(ReirectModel.Where(b => !result.Contains(b.id)));
            }
            else
            {
                return View(ReirectModel);
            }
        }

        [HttpPost]
        public ActionResult Index(int? adminCarWosh, int? adminDetailing, List<int> chkRow)
        {
            if (adminCarWosh != null && adminDetailing != null && chkRow != null)
            {                
                _services.AddToCurrentShift(adminCarWosh, adminDetailing, chkRow);
                return Redirect("/Order/Index");
            }

            var ReirectModel = Mapper.Map<IEnumerable<CarWashWorkersView>>(_services.GetChooseEmployees());   
            
            return View(ReirectModel);
        }

        public ActionResult Staff()
        {
            var StaffAll = Mapper.Map<IEnumerable<CarWashWorkersView>>(_services.GetStaffAll().OrderBy(item => item.status));
            return View(StaffAll);
        }

        // GET: CarWashWorkersViews/Create
        public ActionResult AddEmployee()
        {
            var carWashWorkersViewsGet = Mapper.Map<IEnumerable<CarWashWorkersView>>(_services.GetStaffAll());

            ViewBag.Status = "true";
            ViewBag.Job = new SelectList(_job.SelectJobTitle(), "Id", "Position");

            return View();
        }

        // POST: CarWashWorkersViews/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee([Bind(Include = "id,Name,Surname,Patronymic,MobilePhone,Experience,InterestRate,rate," +
            "DataRegistration,DataDismissal,status,Photo,IdPosition")] CarWashWorkersView carWashWorkersView)
        {
            if (ModelState.IsValid)
            {
                CarWashWorkersBll carWashWorkersBll = Mapper.Map<CarWashWorkersView, CarWashWorkersBll>(carWashWorkersView);
                _services.InsertEmployee(carWashWorkersBll);
                return RedirectToAction("Staff");
            }

            ViewBag.Job = new SelectList(_job.SelectJobTitle(), "Id", "Position");

            return View();
        }

        // GET: CarWashWorkersViews/Edit/5
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CarWashWorkersView carWashWorkersView = Mapper.Map<CarWashWorkersView>(_services.CarWashWorkersId(id));
            ViewBag.Job = new SelectList(_job.SelectJobTitle(), "Id", "Position");

            if (carWashWorkersView == null)
            {
                return HttpNotFound();
            }

            return View(carWashWorkersView);
        }

        // POST: CarWashWorkersViews/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee([Bind(Include = "id,Name,Surname,Patronymic,MobilePhone,Experience,InterestRate,rate," +
            "DataRegistration,DataDismissal,status,Photo,IdPosition")] CarWashWorkersView carWashWorkersView, string command)
        {
            if (ModelState.IsValid)
            {
                CarWashWorkersBll carWashWorkersBll = Mapper.Map<CarWashWorkersView, CarWashWorkersBll>(carWashWorkersView);
                _services.UpdateEmploee(carWashWorkersBll, command);
                return RedirectToAction("Staff");
            }

            ViewBag.Job = new SelectList(_job.SelectJobTitle(), "Id", "Position");
            return View(carWashWorkersView);
        }


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
