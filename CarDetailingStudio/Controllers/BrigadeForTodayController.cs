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
    }
}
