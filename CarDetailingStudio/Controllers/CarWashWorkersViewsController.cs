using AutoMapper;
using CarDetailingStudio.BLL.EmployeesModules.Contract;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Checkout;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
    public class CarWashWorkersViewsController : Controller
    {
        private ICarWashWorkersServices _services;
        private IJobTitleTableServices _job;
        private IBrigadeForTodayServices _brigadeForToday;
        private IOrderCarWashWorkersServices _orderCarWash;
        private IOrderServicesCarWashServices _orderServices;
        private IPremiumAndRateServices _premiumAndRateServices;
        private IPosition _position;

        private readonly IEmployeesFacade _employeesFacade;

        public CarWashWorkersViewsController(
            ICarWashWorkersServices carWashWorkers,
            IJobTitleTableServices job,
            IBrigadeForTodayServices brigadeForToday,
            IOrderCarWashWorkersServices orderCarWash,
            IOrderServicesCarWashServices orderServices,
            IPremiumAndRateServices premiumAndRateServices,
            IPosition position,
            IEmployeesFacade employeesFacade)
        {
            _services = carWashWorkers;
            _job = job;
            _brigadeForToday = brigadeForToday;
            _orderCarWash = orderCarWash;
            _orderServices = orderServices;
            _premiumAndRateServices = premiumAndRateServices;
            _position = position;
            _employeesFacade = employeesFacade;
        }

        #region
        // GET: CarWashWorkersViews
        public async Task<ActionResult> Index()
        {
            if (TempData.ContainsKey("BrigadeId"))
            {
                return View(Mapper.Map<ChangeOfDayModel>(await _employeesFacade.SelectionOfEmployeesToShift(DateTime.Now)));
            }
            else
            {
                return View(Mapper.Map<ChangeOfDayModel>(await _employeesFacade.SelectionOfEmployeesToShift()));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Index(int? adminCarWosh, int? adminDetailing, List<int> chkRow)
        {
            if (adminCarWosh != null && adminDetailing != null && chkRow != null)
            {
                if (chkRow.Any(x => x == adminCarWosh) != true)
                    chkRow.Add(adminCarWosh.Value);

                if (chkRow.Any(x => x == adminDetailing) != true)
                    chkRow.Add(adminDetailing.Value);

                await _services.AddToCurrentShift(adminCarWosh, adminDetailing, chkRow);
                return Redirect("/Order/Index");
            }
            else if (chkRow != null && adminCarWosh == null && adminDetailing == null)
            {
                await _services.AddToCurrentShift(chkRow);
                return Redirect("/BrigadeForToday/TodayShift");
            }
            else if (adminCarWosh != null && adminDetailing != null)
            {
                await _services.AddToCurrentShift(adminCarWosh, adminDetailing);
                return Redirect("/BrigadeForToday/TodayShift");
            }
            else if (adminCarWosh != null || adminDetailing != null)
            {
                await _services.AddToCurrentShift(adminCarWosh, adminDetailing);
                return Redirect("/BrigadeForToday/TodayShift");
            }

            var ReirectModel = Mapper.Map<IEnumerable<CarWashWorkersView>>(await _services.GetChooseEmployees());

            return View(ReirectModel);
        }

        public async Task<ActionResult> Staff() =>
            View(Mapper.Map<IEnumerable<CarWashWorkersView>>(await _services.GetChooseEmployees()));

        public async Task<ActionResult> StaffArxiv() =>
            View(Mapper.Map<IEnumerable<CarWashWorkersView>>(await _services.GetChooseEmployees(false)));

        public async Task<ActionResult> EducationOfWages(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<PremiumAndRateView> premiumAndRate;
            premiumAndRate = Mapper.Map<IEnumerable<PremiumAndRateView>>(await _premiumAndRateServices.SelectPosition(id.Value)).ToList();

            if (premiumAndRate.Count() == 0)
            {
                var resultCarWashWorkersView = Mapper.Map<CarWashWorkersView>(await _services.CarWashWorkersId(id));
                await _premiumAndRateServices.CreatePremiumAndRateServices(resultCarWashWorkersView.IdPosition.Value, id.Value);

                premiumAndRate = Mapper.Map<IEnumerable<PremiumAndRateView>>(await _premiumAndRateServices.SelectPosition(id.Value)).ToList();
            }

            var position = Mapper.Map<IEnumerable<PositionView>>(await _position.GetTableAll());

            ViewBag.Position = new SelectList(position, "idPosition", "name");
            ViewBag.PositionAll = position;

            return View(premiumAndRate);
        }

        [HttpPost]
        public async Task<ActionResult> EducationOfWages(List<PremiumAndRateView> premiumAndRates)
        {
            if (premiumAndRates == null || premiumAndRates.Count() == 0)
            {
                return RedirectToAction("Staff");
            }

            if (premiumAndRates.Count() != 0)
            {
                foreach (var item in premiumAndRates)
                {
                    PremiumAndRateBll premiumMap = Mapper.Map<PremiumAndRateView, PremiumAndRateBll>(item);
                    await _premiumAndRateServices.Update(premiumMap);
                }

                return RedirectToAction("Staff");
            }

            var position = Mapper.Map<IEnumerable<PositionView>>(await _position.GetTableAll());

            ViewBag.Position = new SelectList(position, "idPosition", "name");
            ViewBag.PositionAll = position;

            return View();
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
                if (carWashWorkersView.AdministratorsInterestRate == null)
                {
                    carWashWorkersView.AdministratorsInterestRate = 0;
                }

                carWashWorkersView.status = true;

                var carWashWorkersResult = TransformAnEntity(carWashWorkersView);

                int idEmployee = await _services.InsertEmployee(carWashWorkersResult);
                await _premiumAndRateServices.CreatePremiumAndRateServices(carWashWorkersResult.IdPosition.Value, idEmployee);

                return RedirectToAction("EducationOfWages", "CarWashWorkersViews", new RouteValueDictionary(new
                {
                    id = idEmployee,
                }));
            }

            ViewBag.Job = new SelectList(await _job.SelectJobTitle(), "Id", "Position");
            ViewBag.Position = new SelectList(await _position.GetTableAll(), "idPosition", "name");

            return View();
        }

        // GET: CarWashWorkersViews/Edit/5
        public async Task<ActionResult> EditEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CarWashWorkersView carWashWorkersView = Mapper.Map<CarWashWorkersView>(await _services.CarWashWorkersId(id));
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
            "DataRegistration,DataDismissal,status,Photo,IdPosition,AdministratorsInterestRate ")] CarWashWorkersView carWashWorkersView, string command)
        {
            if (ModelState.IsValid)
            {
                if (carWashWorkersView.AdministratorsInterestRate == null)
                {
                    carWashWorkersView.AdministratorsInterestRate = 0;
                }

                await _services.UpdateEmploee(TransformAnEntity(carWashWorkersView), command);
                return RedirectToAction("Staff");
            }

            ViewBag.Job = new SelectList(await _job.SelectJobTitle(), "Id", "Position");
            return View(carWashWorkersView);
        }

        [HttpGet]
        public async Task<ActionResult> EditingStaffOnOrder(int? idOrder, int? idEmployee)
        {
            var resultGetChooseEmployees = Mapper.Map<IEnumerable<CarWashWorkersView>>(await _services.GetChooseEmployees());

            ViewBag.Brigade = resultGetChooseEmployees.OrderBy(x => x.IdPosition);
            ViewBag.Order = idOrder;

            return View(Mapper.Map<CarWashWorkersView>(await _services.CarWashWorkersId(idEmployee)));
        }

        [HttpPost]
        public async Task<ActionResult> ChangeEmployee(int? idOrder, int? idBrigade, int? idEmployee)
        {
            if (idBrigade == null)
            {
                return RedirectToAction("EditingStaffOnOrder", "CarWashWorkersViews", new RouteValueDictionary(new
                {
                    idOrder = idOrder,
                    idEmployee = idEmployee
                }));
            }
            else
            {
                OrderCarWashWorkersView order = Mapper.Map<OrderCarWashWorkersView>(await _orderCarWash.Change(idOrder, idEmployee));
                order.IdCarWashWorkers = idBrigade.Value;

                OrderCarWashWorkersBll carWashWorkersBll = Mapper.Map<OrderCarWashWorkersView, OrderCarWashWorkersBll>(order);
                await _orderCarWash.Update(carWashWorkersBll);
            }

            return RedirectToAction("CompletedOrders", "Order", new RouteValueDictionary(new
            {
                idOrder = idOrder
            }));
        }

        [HttpGet]
        public async Task<ActionResult> ArxivOrderCarWashWorkers()
        {
            var OrderArxivResult = Mapper.Map<IEnumerable<CarWashWorkersBll>>(await _services.EmployeeName());
            ViewBag.OrderArxivResult = new SelectList(OrderArxivResult, "id", "Name");

            return View(Mapper.Map<IEnumerable<OrderCarWashWorkersDayGroupView>>(await _orderCarWash.OrderCarWashWorkers(null, DateTime.Today.AddDays(-7), DateTime.Now)).OrderByDescending(x => x.Id));
        }

        [HttpPost]
        public async Task<ActionResult> ArxivOrderCarWashWorkers(int? idEmployee, DateTime? startDate, DateTime? finalDate)
        {
            if (startDate != null)
            {
                ViewBag.OrderArxivResult = new SelectList(Mapper.Map<IEnumerable<CarWashWorkersBll>>(await _services.EmployeeName()), "id", "Name");

                return View(Mapper.Map<IEnumerable<OrderCarWashWorkersDayGroupView>>(await _orderCarWash.OrderCarWashWorkers(idEmployee, startDate.Value, finalDate)));
            }

            var OrderArxivResult = Mapper.Map<IEnumerable<CarWashWorkersBll>>(await _services.EmployeeName());
            ViewBag.OrderArxivResult = new SelectList(OrderArxivResult, "id", "Name");

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> OrderDay(int? idEmployee, DateTime date)
        {
            return View(Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWash.DailyEmployeeOrders(idEmployee, date)));
        }

        [HttpGet]
        public async Task<ActionResult> EmployeeWorkDays(int idEmploye)
        {
            var emplotee = Mapper.Map<IEnumerable<OrderCarWashWorkersDayGroupView>>(await _orderCarWash.WhereCarWashWorkers(idEmploye)).OrderByDescending(x => x.ClosingData);
            ViewBag.FIO = Mapper.Map<CarWashWorkersView>(await _services.CarWashWorkersId(idEmploye));

            return View(emplotee);
        }

        private CarWashWorkersBll TransformAnEntity(CarWashWorkersView entity) => 
            Mapper.Map<CarWashWorkersView, CarWashWorkersBll>(entity);
        #endregion
    }
}
