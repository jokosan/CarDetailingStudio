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
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Filters;

namespace CarDetailingStudio.Controllers
{
    public class BrigadeForTodayController : Controller
    {
        private IBrigadeForTodayServices _services;

        public BrigadeForTodayController(IBrigadeForTodayServices brigade)
        {
            _services = brigade;
        }

        [MonitoringTheNumberOfEmployeesFilter]
        // GET: BrigadeForToday
        public ActionResult TodayShift()
        {
            var brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(_services.GetDateTimeNow());

            TempData["BrigadeId"] = brigade.Where(x => x.EarlyTermination == true);               

            return View(brigade);
        }

        // POST: BrigadeForToday/Delete/5
        [HttpPost, ActionName("TodayShift")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                _services.RemoveFromBrigade(id);
            }
          
            return RedirectToAction("TodayShift");
        }

        // GET: BrigadeForToday/Details/5
        public ActionResult EmployeeInformation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var brigadeForTodayView = Mapper.Map<IEnumerable<BrigadeForTodayView>>(_services.Info(id));

            if (brigadeForTodayView == null)
            {
                return HttpNotFound();
            }

            var info = brigadeForTodayView.First(x => x.IdCarWashWorkers == id);

            ViewData["Info"] = $"{info.CarWashWorkers.Surname} {info.CarWashWorkers.Name} {info.CarWashWorkers.Patronymic} ({info.CarWashWorkers.JobTitleTable.Position})";
          
            return View(brigadeForTodayView);
        }
        #region
        //// GET: BrigadeForToday/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BrigadeForToday/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,Date,Time,EndTime,EarlyTermination,IdCarWashWorkers")] BrigadeForTodayView brigadeForTodayView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.brigadeForToday.Add(brigadeForTodayView);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(brigadeForTodayView);
        //}

        //// GET: BrigadeForToday/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BrigadeForTodayView brigadeForTodayView = db.brigadeForToday.Find(id);
        //    if (brigadeForTodayView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(brigadeForTodayView);
        //}

        //// POST: BrigadeForToday/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,Date,Time,EndTime,EarlyTermination,IdCarWashWorkers")] BrigadeForTodayView brigadeForTodayView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brigadeForTodayView).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(brigadeForTodayView);
        //}

        //// GET: BrigadeForToday/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BrigadeForTodayView brigadeForTodayView = db.brigadeForToday.Find(id);
        //    if (brigadeForTodayView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(brigadeForTodayView);
        //}

        //// POST: BrigadeForToday/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    BrigadeForTodayView brigadeForTodayView = db.brigadeForToday.Find(id);
        //    db.brigadeForToday.Remove(brigadeForTodayView);
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
