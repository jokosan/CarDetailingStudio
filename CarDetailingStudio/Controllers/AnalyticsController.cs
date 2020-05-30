using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
      

        public AnalyticsController(ISalaryExpenses salaryExpenses, IOrderServicesCarWashServices orderServicesCarWash,
                                   IUtilityCosts utilityCosts, IOtherExpenses otherExpenses, ICostsCarWashAndDeteyling costsCarWashAndDeteyling, IOrderCarWashWorkersServices orderCarWashWorkers)
        {
            _salaryExpenses = salaryExpenses;
            _orderServicesCarWash = orderServicesCarWash;
            _utilityCosts = utilityCosts;
            _otherExpenses = otherExpenses;
            _costsCarWashAndDeteyling = costsCarWashAndDeteyling;
            _orderCarWashWorkers = orderCarWashWorkers;
        }

        // GET: Analytics
        public ActionResult MonthlyReport(DateTime? date = null)
        {           
            if (date == null)
            {
                 date = DateTime.Now;
            }         

            var order = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(_orderServicesCarWash.MonthlyReport(date.Value));
            var costCarWashToDetailings = Mapper.Map<IEnumerable<CostsCarWashAndDeteylingView>>(_costsCarWashAndDeteyling.MonthlyReport(date.Value));
            var utilityCost = Mapper.Map<IEnumerable<UtilityCostsView>>(_utilityCosts.MonthlyReport(date.Value));
            var otherExpenses = Mapper.Map<IEnumerable<OtherExpensesView>>(_otherExpenses.MonthlyReport(date.Value));

            var carWash = order.Where(x => x.ServisesCarWashOrder.Any(y => y.Detailings.IdTypeService == 2));
            var detailings = order.Where(x => x.ServisesCarWashOrder.Any(y => y.Detailings.IdTypeService == 1));
            var administratorCarWash = order.Where(x => x.OrderCarWashWorkers.Any(q => q.CarWashWorkers.IdPosition == 1));
            var administratorDetailings = order.Where(x => x.OrderCarWashWorkers.Any(q => q.CarWashWorkers.IdPosition == 2));
            var staffCarWashWorker = order.Where(x => x.OrderCarWashWorkers.Any(q => q.CarWashWorkers.IdPosition >= 4));
            var staffDetailing = order.Where(x => x.OrderCarWashWorkers.Any(q => q.CarWashWorkers.IdPosition == 3));
            var costCarWash = costCarWashToDetailings.Where(x => x.typeServicesId == 1);
            var costDetailing = costCarWashToDetailings.Where(x => x.typeServicesId == 2);

            double? AdministratorCarWash = administratorCarWash.Sum(x => x.OrderCarWashWorkers.Sum(t => t.Payroll));
            double? AdministratorDetailings = administratorDetailings.Sum(x => x.OrderCarWashWorkers.Sum(t => t.Payroll));
            double? StaffCarWashWorker = staffCarWashWorker.Sum(x => x.OrderCarWashWorkers.Sum(t => t.Payroll));
            double? StaffDetailing = staffDetailing.Sum(x => x.OrderCarWashWorkers.Sum(t => t.Payroll));
            double? OrderCarWashSum = carWash.Sum(x => x.DiscountPrice);
            double? OrderDetailing = detailings.Sum(x => x.DiscountPrice);
            double? costCarWashSum = costCarWash.Sum(x => x.amount);
            double? costDetailingsSum = costDetailing.Sum(x => x.amount);

            // CarWash
            ViewBag.OrderCarWashSum = OrderCarWashSum; // Касса мойки
            ViewBag.OrderCount = carWash.Count(); // Количество Авто мойка
            ViewBag.CostCarWash = costCarWashSum;

            // Detailings
            ViewBag.OrderDetailingsSum = OrderDetailing; // Касса детейлинг
            ViewBag.CarCountDetailings = detailings.Count(); // Количество авто детейлинг
            ViewBag.CostDetailings = costDetailingsSum;
           
            // ЗП Администратора мойки и детейлинга
            ViewBag.AdministratorCarWash = AdministratorCarWash;
            ViewBag.AdministratorDetailings = AdministratorDetailings;

            // ЗП Сотрудников мойки и детейлинга
            ViewBag.StaffCarWashWorker = StaffCarWashWorker;
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

            var invoice = new DateTime(date.Value.Year, date.Value.Month, 1);
            ViewBag.InvoiceDate = invoice.ToString("D");

            if (date.Value.Month != DateTime.Now.Month)
            {
                var due = invoice.AddMonths(1).AddDays(-1);
                ViewBag.DueDate = due.ToString("D");
            }
            else
            {
                ViewBag.DueDate = DateTime.Now.ToString("D");
            }

            return View();
        }

        [HttpPost]
        public ActionResult MonthlyReport(DateTime dateViewUser)        
        {
            return RedirectToAction("MonthlyReport", "Analytics", new RouteValueDictionary(new
            {
                date = dateViewUser
            }));
        }
    }
}