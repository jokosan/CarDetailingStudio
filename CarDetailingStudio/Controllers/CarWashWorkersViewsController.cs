using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    public class CarWashWorkersViewsController : Controller
    {
        private ICarWashWorkersServices _services;
        private IJobTitleTableServices _job;
        private IBrigadeForTodayServices _brigadeForToday;

        public CarWashWorkersViewsController(ICarWashWorkersServices carWashWorkers, IJobTitleTableServices job, IBrigadeForTodayServices brigadeForToday)
        {
            _services = carWashWorkers;
            _job = job;
            _brigadeForToday = brigadeForToday;
        }

        // GET: CarWashWorkersViews
        public async Task<ActionResult> Index()
        {
            var ReirectModel = Mapper.Map<IEnumerable<CarWashWorkersView>>(await _services.GetChooseEmployees());

            if (TempData.ContainsKey("BrigadeId"))
            {
                var resultBrigade = TempData["BrigadeId"] as IEnumerable<BrigadeForTodayView>;
                var result = new List<int>();

                var AdministtratorCarWash = resultBrigade.Any(x => (x.StatusId == 1) && (x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")));
                var AdministtratorDeteling = resultBrigade.Any(x => (x.StatusId == 2) && (x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")));

                ViewBag.StatusAdministtratorCarWash = AdministtratorCarWash;
                ViewBag.StatusAdministtratorDeteling = AdministtratorDeteling;

                foreach (var i in resultBrigade)
                    result.Add(i.IdCarWashWorkers.Value);

                return View(ReirectModel.Where(b => !result.Contains(b.id)));
            }
            else
            {
                var brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigadeForToday.GetDateTimeNow());

                ViewBag.StatusAdministtratorCarWash = brigade.Any(x => (x.StatusId == 1) && (x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")));
                ViewBag.StatusAdministtratorDeteling = brigade.Any(x => (x.StatusId == 2) && (x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")));

                return View(ReirectModel);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Index(int? adminCarWosh, int? adminDetailing, List<int> chkRow)
        {
            if (adminCarWosh != null && adminDetailing != null && chkRow != null)
            {
                await _services.AddToCurrentShift(adminCarWosh, adminDetailing, chkRow);
                return Redirect("/Order/Index");
            }
            else if (chkRow != null)
            {
                await _services.AddToCurrentShift(adminCarWosh, adminDetailing, chkRow);
                return Redirect("/BrigadeForToday/TodayShift");
            }

            var ReirectModel = Mapper.Map<IEnumerable<CarWashWorkersView>>(_services.GetChooseEmployees());

            return View(ReirectModel);
        }

        public async Task<ActionResult> Staff()
        {
            var StaffAll = Mapper.Map<IEnumerable<CarWashWorkersView>>(await _services.GetChooseEmployees());
            return View(StaffAll);
        }

        // GET: CarWashWorkersViews/Create
        public async Task<ActionResult> AddEmployee()
        {
            var carWashWorkersViewsGet = Mapper.Map<IEnumerable<CarWashWorkersView>>(await _services.GetStaffAll());

            ViewBag.Status = "true";
            ViewBag.Job = new SelectList(await _job.SelectJobTitle(), "Id", "Position");

            return View();
        }

        // POST: CarWashWorkersViews/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee([Bind(Include = "id,Name,Surname,Patronymic,MobilePhone,Experience,AdministratorsInterestRate,InterestRate,rate,DataRegistration,DataDismissal,status,Photo,IdPosition")] CarWashWorkersView carWashWorkersView)
        {
            if (ModelState.IsValid)
            {
                carWashWorkersView.status = "true";
                CarWashWorkersBll carWashWorkersBll = Mapper.Map<CarWashWorkersView, CarWashWorkersBll>(carWashWorkersView);

                await _services.InsertEmployee(carWashWorkersBll);
                return RedirectToAction("Staff");
            }

            ViewBag.Job = new SelectList(await _job.SelectJobTitle(), "Id", "Position");

            return View();
        }

        // GET: CarWashWorkersViews/Edit/5
        public async Task<ActionResult> EditEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CarWashWorkersView carWashWorkersView = Mapper.Map<CarWashWorkersView>(_services.CarWashWorkersId(id));
            ViewBag.Job = new SelectList(await _job.SelectJobTitle(), "Id", "Position");

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
        public async Task<ActionResult> EditEmployee([Bind(Include = "id,Name,Surname,Patronymic,MobilePhone,Experience,InterestRate,rate," +
            "DataRegistration,DataDismissal,status,Photo,IdPosition")] CarWashWorkersView carWashWorkersView, string command)
        {
            if (ModelState.IsValid)
            {
                CarWashWorkersBll carWashWorkersBll = Mapper.Map<CarWashWorkersView, CarWashWorkersBll>(carWashWorkersView);
                await _services.UpdateEmploee(carWashWorkersBll, command);
                return RedirectToAction("Staff");
            }

            ViewBag.Job = new SelectList(await _job.SelectJobTitle(), "Id", "Position");
            return View(carWashWorkersView);
        }

    }
}
