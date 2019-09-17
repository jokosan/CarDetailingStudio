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
    public class BrigadeForTodayController : Controller
    {
        private BrigadeForTodayServices _brigadeForToday;
        private CarWashWorkersServices _ServicesClass;

        public BrigadeForTodayController(BrigadeForTodayServices todayServices, CarWashWorkersServices workersServices)
        {
            _brigadeForToday = todayServices;
            _ServicesClass = workersServices;
        }

        // GET: BrigadeForToday
        public ActionResult Index()
        {
            return View(_brigadeForToday.GetAll());
        }

        // GET: BrigadeForToday/Details/5
        public ActionResult Details(int? id)
        {
            throw new NotImplementedException();
        }

        // GET: BrigadeForToday/Create
        public ActionResult Create()
        {            
            return View(_ServicesClass.GetChooseEmployees());
        }

        // POST: BrigadeForToday/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Date,Time,EndTime,EarlyTermination,IdCarWashWorkers")] brigadeForToday brigadeForToday)
        {
            if (ModelState.IsValid)
            {
                
                _brigadeForToday.Create(brigadeForToday);
                
                return RedirectToAction("Index");
            }

            //ViewBag.IdCarWashWorkers = new SelectList(db.CarWashWorkers, "id", "idKey", brigadeForToday.IdCarWashWorkers);
            return Redirect("/Home/index");
        }

        // GET: BrigadeForToday/Edit/5
        public ActionResult Edit(int? id)
        {
            throw new NotImplementedException();
        }

        // POST: BrigadeForToday/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Date,Time,EndTime,EarlyTermination,IdCarWashWorkers")] brigadeForToday brigadeForToday)
        {
            throw new NotImplementedException();
        }

        // GET: BrigadeForToday/Delete/5
        public ActionResult Delete(int? id)
        {
            throw new NotImplementedException();
        }

        // POST: BrigadeForToday/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            throw new NotImplementedException();
        }

    
    }
}
