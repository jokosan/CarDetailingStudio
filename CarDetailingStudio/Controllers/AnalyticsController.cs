using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
    public class AnalyticsController : Controller
    {
        private ISalaryExpenses _salaryExpenses;
        private IOrderServicesCarWashServices _orderServicesCarWash;
        private IUtilityCosts _utilityCosts;
        private IOtherExpenses _otherExpenses;
        private ICostsCarWashAndDeteyling _costsCarWashAndDeteyling;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private IBonusToSalary _bonusToSalary;


        double? test { get; set; }

        public AnalyticsController(ISalaryExpenses salaryExpenses, IOrderServicesCarWashServices orderServicesCarWash,
                                   IUtilityCosts utilityCosts, IOtherExpenses otherExpenses, ICostsCarWashAndDeteyling costsCarWashAndDeteyling,
                                   IOrderCarWashWorkersServices orderCarWashWorkers, IBonusToSalary bonusToSalary)
        {
            _salaryExpenses = salaryExpenses;
            _orderServicesCarWash = orderServicesCarWash;
            _utilityCosts = utilityCosts;
            _otherExpenses = otherExpenses;
            _costsCarWashAndDeteyling = costsCarWashAndDeteyling;
            _orderCarWashWorkers = orderCarWashWorkers;
            _bonusToSalary = bonusToSalary;
        }

        // GET: Analytics
        public async Task<ActionResult> Report(DateTime? startDate = null, DateTime? finalDate = null)
        {
            if (startDate == null && finalDate == null)
            {
                startDate = DateTime.Now;
            }

            IEnumerable<OrderServicesCarWashView> order;
            IEnumerable<CostsCarWashAndDeteylingView> costCarWashToDetailings;
            IEnumerable<UtilityCostsView> utilityCost;
            IEnumerable<OtherExpensesView> otherExpenses;
            IEnumerable<OrderCarWashWorkersView> OrderCarWashWorker;
            IEnumerable<BonusToSalaryView> BonusToSalary;

            if (finalDate == null)
            {
                order = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _orderServicesCarWash.Reports(startDate.Value));
                costCarWashToDetailings = Mapper.Map<IEnumerable<CostsCarWashAndDeteylingView>>(await _costsCarWashAndDeteyling.Reports(startDate.Value));
                utilityCost = Mapper.Map<IEnumerable<UtilityCostsView>>(await _utilityCosts.Reports(startDate.Value));
                otherExpenses = Mapper.Map<IEnumerable<OtherExpensesView>>(await _otherExpenses.Reports(startDate.Value));
                OrderCarWashWorker = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWashWorkers.Reports(startDate.Value));
                BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(startDate.Value));  
            }
            else
            {
                order = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _orderServicesCarWash.Reports(startDate.Value, finalDate.Value));
                costCarWashToDetailings = Mapper.Map<IEnumerable<CostsCarWashAndDeteylingView>>(await _costsCarWashAndDeteyling.Reports(startDate.Value, finalDate.Value));
                utilityCost = Mapper.Map<IEnumerable<UtilityCostsView>>(await _utilityCosts.Reports(startDate.Value, finalDate.Value));
                otherExpenses = Mapper.Map<IEnumerable<OtherExpensesView>>(await _otherExpenses.Reports(startDate.Value, finalDate.Value));
                OrderCarWashWorker = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWashWorkers.Reports(startDate.Value, finalDate.Value));
                BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(startDate.Value, finalDate.Value));
            }

            int orderCount = order.Count();

            var carWash = order.Where(x => x.ServisesCarWashOrder.Any(y => y.Detailings.IdTypeService == 2));
            var detailings = order.Where(x => x.ServisesCarWashOrder.Any(y => y.Detailings.IdTypeService == 1));
            var carpetWashing = order.Where(x => x.typeOfOrder == 3 );

            List<double> sumTet = new List<double>();

            foreach (var item in carpetWashing)
            {
                if (item.typeOfOrder == 3)
                {
                    sumTet.Add(item.OrderCarWashWorkers.Sum(s => s.Payroll).Value);                
                }
            }

            ViewBag.Test = sumTet.Sum();

            var administratorCarWash = OrderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition == 1) && (x.OrderServicesCarWash.typeOfOrder == 1));
            var administratorDetailings = OrderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition == 2) && (x.OrderServicesCarWash.typeOfOrder == 1));
            var staffCarWashWorker = OrderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition >= 4) && (x.OrderServicesCarWash.typeOfOrder == 1)); 
            var staffDetailing = OrderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition == 3) && (x.OrderServicesCarWash.typeOfOrder == 1)); 

            var administratorCarpetWashing = OrderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition <= 2) && (x.OrderServicesCarWash.typeOfOrder == 3));
            var staffCarpetWashing = OrderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition >= 3) && (x.OrderServicesCarWash.typeOfOrder == 3));

            double? AdministratorCarWash = administratorCarWash.Sum(x => x.Payroll);
            double? AdministratorDetailings = administratorDetailings.Sum(x => x.Payroll);
            double? StaffCarWashWorker = staffCarWashWorker.Sum(x => x.Payroll);
            double? StaffDetailing = staffDetailing.Sum(x => x.Payroll);

            var costCarWash = costCarWashToDetailings.Where(x => x.typeServicesId == 1);
            var costDetailing = costCarWashToDetailings.Where(x => x.typeServicesId == 2);
          
            double? OrderCarWashSum = carWash.Sum(x => x.DiscountPrice);
            double? OrderDetailing = detailings.Sum(x => x.DiscountPrice);
            double? costCarWashSum = costCarWash.Sum(x => x.amount);
            double? costDetailingsSum = costDetailing.Sum(x => x.amount);

            double? carpetOrder = carpetWashing.Sum(x => x.DiscountPrice);
            double? adminCarpet = administratorCarpetWashing.Sum(x => x.Payroll);
            double? staffCarpet = staffCarpetWashing.Sum(x => x.Payroll);
          
            //  Carpet
            ViewBag.CarpetOrder = carpetOrder; // Касса ковров
            ViewBag.AdminCarpet = adminCarpet; // ЗП админ 
            ViewBag.StaffCarpet = staffCarpet; // ЗП сотрудников 
            ViewBag.CauntCarpet = carpetWashing.Count();
            ViewBag.TotalOrderCarpet = carpetOrder - adminCarpet - staffCarpet;

            //  BonusToSalary
            ViewBag.BonusToSalarySum = BonusToSalary.Sum(x => x.amount);
            ViewBag.BonusToSalaryCount = BonusToSalary.Count();

            // CarWash
            ViewBag.OrderCarWashSum = OrderCarWashSum; // Касса мойки
            ViewBag.OrderCount = carWash.Count(); // Количество Авто мойка
            ViewBag.CostCarWash = costCarWashSum;

            // Detailings
            ViewBag.OrderDetailingsSum = OrderDetailing; // Касса детейлинг
            ViewBag.CarCountDetailings = detailings.Count(); // Количество авто детейлинг
            ViewBag.CostDetailings = costDetailingsSum;
           
            // ЗП Администратора мойки и детейлинга
            ViewBag.AdministratorCarWash = AdministratorCarWash + adminCarpet;
            ViewBag.AdministratorDetailings = AdministratorDetailings;

            // ЗП Сотрудников мойки и детейлинга
            ViewBag.StaffCarWashWorker = StaffCarWashWorker + staffCarpet;
            ViewBag.StaffDetailing = StaffDetailing;

            // комунальные услуги
            ViewBag.UtilityCost = utilityCost.GroupBy(x => x.expenseCategoryId)
                                              .Select(y => new UtilityCostsView
                                              {
                                                  idUtilityCosts = y.First().idUtilityCosts,
                                                  dateExpenses = y.First().dateExpenses,
                                                  amount = y.First().amount,
                                                  expenseCategoryId = y.First().expenseCategoryId,
                                                  name = y.First().expenseCategory.name
                                              });

            ViewBag.UtilityCostSum = utilityCost.Sum(x => x.amount);

            // Прочие расходы 
            ViewBag.OtherExpenses = otherExpenses.GroupBy(x => x.expenseCategoryId)
                                                  .Select(y => new OtherExpensesView
                                                  {
                                                      idOtherExpenses = y.First().idOtherExpenses,
                                                      amount = y.First().amount,
                                                      expenseCategoryId = y.First().expenseCategoryId,
                                                      name = y.First().expenseCategory.name
                                                  });

            ViewBag.OtherExpensesSum = otherExpenses.Sum(x => x.amount);

            //Итого
            ViewBag.TotalCarWash = Math.Round(OrderCarWashSum.Value - costCarWashSum.Value - AdministratorCarWash.Value - StaffCarWashWorker.Value, 2);
            ViewBag.TotalDetailing = Math.Round(OrderDetailing.Value - costDetailingsSum.Value - AdministratorDetailings.Value - StaffDetailing.Value, 2);

            ViewBag.Total = ViewBag.TotalCarWash + ViewBag.TotalDetailing + ViewBag.TotalOrderCarpet - ViewBag.UtilityCostSum - ViewBag.OtherExpensesSum - ViewBag.BonusToSalarySum;

            var invoice = new DateTime(startDate.Value.Year, startDate.Value.Month, 1);
            ViewBag.InvoiceDate = invoice.ToString("D");

            if (finalDate == null)
            {             
                ViewBag.StartDate = startDate.Value.ToString("D");
            }
            else
            {
                ViewBag.StartDate = startDate.Value.ToString("D");
                ViewBag.FinalDate = finalDate.Value.ToString("D");
            }

            return View();
        }

        [HttpPost]
        public ActionResult MonthlyReport(DateTime? startdateViewUser, DateTime? finaldateViewUser)
        {
            return RedirectToAction("Report", "Analytics", new RouteValueDictionary(new
            {
                startDate = startdateViewUser,
                finalDate = finaldateViewUser
            }));
        }
    }
}