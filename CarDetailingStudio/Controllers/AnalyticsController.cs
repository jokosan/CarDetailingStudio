using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace CarDetailingStudio.Controllers
{
    public class AnalyticsController : Controller
    {
        private IIncomeForTheCurrentDay _incomeForTheCurrent;
        private IBonusToSalary _bonusToSalary;
        private ICashier _cashier;
        private ISalaryExpenses _salary;
        private IOrderCarWashWorkersServices _orderCarWash;
        private IBrigadeForTodayServices _brigadeForTodayServices;
        private IOrderServicesCarWashServices _orderServices;

        private double sumOfAllExpenses = 0;

        private EmployeeSalariesView employeeSalaries = new EmployeeSalariesView();
        private OrderInformationWashingDetailingView orderInformation = new OrderInformationWashingDetailingView();

        double? test { get; set; }

        public AnalyticsController(IIncomeForTheCurrentDay incomeForTheCurrent, IBonusToSalary bonusToSalary, ICashier cashier, IOrderServicesCarWashServices orderServices,
                                   ISalaryExpenses salary, IOrderCarWashWorkersServices orderCarWash, IBrigadeForTodayServices brigadeForTodayServices)
        {
            _incomeForTheCurrent = incomeForTheCurrent;
            _bonusToSalary = bonusToSalary;
            _cashier = cashier;
            _salary = salary;
            _orderCarWash = orderCarWash;
            _brigadeForTodayServices = brigadeForTodayServices;
            _orderServices = orderServices;
        }

        // GET: Analytics
        public async Task<ActionResult> Report(DateTime? startDate = null, DateTime? finalDate = null)
        {
            if (startDate == null && finalDate == null)
            {
                startDate = DateTime.Now;
            }

            IEnumerable<BonusToSalaryView> BonusToSalary;
            IEnumerable<SalaryExpensesView> salaryExpenses;

            if (finalDate == null)
            {
                sumOfAllExpenses = await _incomeForTheCurrent.SumOfAllExpenses(startDate.Value);
                employeeSalaries = Mapper.Map<EmployeeSalariesView>(await _incomeForTheCurrent.EmployeeSalaries(startDate.Value));
                orderInformation = Mapper.Map<OrderInformationWashingDetailingView>(await _incomeForTheCurrent.AmountOfCompletedOrders(startDate.Value));
                BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(startDate.Value));
                salaryExpenses = Mapper.Map<IEnumerable<SalaryExpensesView>>(await _salary.Reports(startDate.Value));
            }
            else
            {
                sumOfAllExpenses = await _incomeForTheCurrent.SumOfAllExpenses(startDate.Value, finalDate.Value);
                employeeSalaries = Mapper.Map<EmployeeSalariesView>(await _incomeForTheCurrent.EmployeeSalaries(startDate.Value, finalDate.Value));
                orderInformation = Mapper.Map<OrderInformationWashingDetailingView>(await _incomeForTheCurrent.AmountOfCompletedOrders(startDate.Value, finalDate.Value));
                BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(startDate.Value, finalDate.Value));
                salaryExpenses = Mapper.Map<IEnumerable<SalaryExpensesView>>(await _salary.Reports(startDate.Value, finalDate.Value));
            }

            double salaryExpensesDay = salaryExpenses.Sum(s => s.amount).Value;
            ViewBag.SalaryExpensesDay = salaryExpensesDay;

            //  Carpet
            ViewBag.CarpetOrder = orderInformation.carpetOrder; // Касса ковров  
            ViewBag.CauntCarpet = orderInformation.CauntCarpet; // Количество заказов ковров
            ViewBag.AdminCarpet = employeeSalaries.adminCarpet;
            ViewBag.StaffCarpet = employeeSalaries.staffCarpet;

            //  BonusToSalary
            ViewBag.BonusToSalarySum = BonusToSalary.Sum(x => x.amount);
            ViewBag.BonusToSalaryCount = BonusToSalary.Count();

            // CarWash
            ViewBag.OrderCarWashSum = orderInformation.OrderCarWashSum; // Касса мойки
            ViewBag.OrderCount = orderInformation.OrderCount; // Количество Авто мойка

            // Detailings
            ViewBag.OrderDetailingsSum = orderInformation.OrderDetailing; // Касса детейлинг
            ViewBag.CarCountDetailings = orderInformation.CarCountDetailings; // Количество авто детейлинг

            // ЗП Администратора мойки и детейлинга
            ViewBag.AdministratorCarWash = employeeSalaries.AdministratorCarWash + employeeSalaries.adminCarpet;
            ViewBag.AdministratorDetailings = employeeSalaries.AdministratorDetailings;

            // ЗП Сотрудников мойки и детейлинга
            ViewBag.StaffCarWashWorker = employeeSalaries.StaffCarWashWorker + employeeSalaries.staffCarpet;
            ViewBag.StaffDetailing = employeeSalaries.StaffDetailing;

            // Наличный безналичный доходы
            ViewBag.Cash = orderInformation.cash;
            ViewBag.NonCash = orderInformation.nonCash;

            //итого доходы
            ViewBag.TotalIncome = orderInformation.OrderCarWashSum + orderInformation.OrderDetailing + orderInformation.carpetOrder;

            //итого расходы на обслуживание 
            ViewBag.TotalCosts = sumOfAllExpenses;

            //Итого расходы на зарплату
            ViewBag.TotalSalaryExpenses = employeeSalaries.adminCarpet + employeeSalaries.staffCarpet + employeeSalaries.AdministratorCarWash + employeeSalaries.AdministratorDetailings + employeeSalaries.StaffCarWashWorker + employeeSalaries.StaffDetailing;

            //Итого
            ViewBag.Total = ViewBag.TotalIncome - ViewBag.TotalCosts - ViewBag.TotalSalaryExpenses;
            //Итого
            ViewBag.TotalCash = ViewBag.TotalIncome - ViewBag.TotalCosts - salaryExpensesDay;

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

        [HttpGet]
        public async Task<ActionResult> SummaryOfTheDay(bool CloseDay = false, bool message = true)
        {
            var sumOfAllExpenses = await _incomeForTheCurrent.SumOfAllExpenses(DateTime.Now);
            var employeeSalaries = Mapper.Map<EmployeeSalariesView>(await _incomeForTheCurrent.EmployeeSalaries(DateTime.Now));
            var orderInformation = Mapper.Map<OrderInformationWashingDetailingView>(await _incomeForTheCurrent.AmountOfCompletedOrders(DateTime.Now));
            var BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(DateTime.Now));
            var Cashier = Mapper.Map<IEnumerable<CashierView>>(await _cashier.Reports(DateTime.Now));
            var salaryExpenses = Mapper.Map<IEnumerable<SalaryExpensesView>>(await _salary.Reports(DateTime.Now));

            double salaryExpensesDay = salaryExpenses.Sum(s => s.amount).Value;
            double cashierDayStart = Cashier.Sum(x => x.amountBeginningOfTheDay);

            ViewBag.AdministratorCarWash = employeeSalaries.AdministratorCarWash + employeeSalaries.adminCarpet;    // ЗП администратора мойки
            ViewBag.StaffDetailing = employeeSalaries.StaffDetailing;                                               // ЗП администратора детейлинга
            ViewBag.StaffCarWashWorker = employeeSalaries.StaffCarWashWorker + employeeSalaries.staffCarpet;        // ЗП сотрудников мойки
            ViewBag.AdministratorDetailings = employeeSalaries.AdministratorDetailings;                             // ЗП сотрудников детейлинга
            
            ViewBag.OrderCarWashSum = orderInformation.OrderCarWashSum;                                             // Касса мойки
            ViewBag.OrderCount = orderInformation.OrderCount;                                                       // Количество Авто мойка
            ViewBag.OrderDetailingsSum = orderInformation.OrderDetailing;                                           // Касса детейлинг
            ViewBag.CarCountDetailings = orderInformation.CarCountDetailings;                                       // Количество авто детейлинг  
            
            ViewBag.CarpetOrder = orderInformation.carpetOrder;                                                     // Касса ковров
            ViewBag.CauntCarpet = orderInformation.CauntCarpet;                                                     // Количество заказов ковров

            ViewBag.TotalCosts = sumOfAllExpenses;                                                                  // Сумма затрат 

            // Количество заказов ожидающих оплату
            ViewBag.NumberOfUnpaidOrders = orderInformation.numberOfUnpaidOrdersCarpetWashing +
                                           orderInformation.numberOfUnpaidOrdersCarWash +
                                           orderInformation.numberOfUnpaidOrdersDetailings;

            ViewBag.SalaryExpenses = salaryExpensesDay;                                                             // Выданно наличных за текущий день 
            ViewBag.CashStartDay = cashierDayStart;                                                                 // Касса начало дня (наличные)

            ViewBag.Cash = orderInformation.cash;                                                                   // Приход наличных
            ViewBag.NonCash = orderInformation.nonCash;                                                             // Приход безналичных

            ViewBag.CashEndDay = orderInformation.cash - sumOfAllExpenses + cashierDayStart;                     // Касса конец дня  (наличные)

            //итого доходы
            ViewBag.TotalIncome = orderInformation.OrderCarWashSum + orderInformation.OrderDetailing + orderInformation.carpetOrder;
            //Итого расходы на зарплату
            ViewBag.TotalSalaryExpenses = employeeSalaries.adminCarpet + employeeSalaries.staffCarpet + employeeSalaries.AdministratorCarWash + employeeSalaries.AdministratorDetailings + employeeSalaries.StaffCarWashWorker + employeeSalaries.StaffDetailing;
            ViewBag.Total = ViewBag.TotalIncome - ViewBag.TotalCosts - ViewBag.TotalSalaryExpenses;
            ViewBag.StartDate = DateTime.Now.ToString("D");

            ViewBag.CloseDay = CloseDay;

            ViewBag.Message = message;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SummaryOfTheDay(double? amountStartDay, double? amountFinalDay)
        {
            if (amountStartDay != null)
            {
                CashierView cashierView = new CashierView();
                DateTime date = DateTime.Now;

                var cashiesInfo = Mapper.Map<IEnumerable<CashierView>>(await _cashier.Reports(date));
                var singlEnd = cashiesInfo.LastOrDefault(x => x.date.Day == date.Day);

                if (singlEnd != null)
                {
                    singlEnd.amountEndOfTheDay = amountFinalDay.Value - amountStartDay.Value;
                    CashierBll cashierEnd = Mapper.Map<CashierView, CashierBll>(singlEnd);

                    await _cashier.Update(cashierEnd);
                }

                CashierBll cashier = Mapper.Map<CashierView, CashierBll>(cashierView);
                cashier.date = DateTime.Now.AddDays(+1);
                cashier.amountBeginningOfTheDay = amountStartDay.Value;

                await _cashier.Insert(cashier);

                return Redirect("/Order/Index");
            }

            return RedirectToAction("SummaryOfTheDay", "Analytics", new RouteValueDictionary(new
            {
                CloseDay = true,
                message = false
            }));
        }

        public async Task<ActionResult> EmployeeSalaryDetails()
        {
            var orderCarWashWorker = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWash.Reports(DateTime.Now.AddDays(-7)));
            var brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigadeForTodayServices.Reports(DateTime.Now.AddDays(-7)));
            var orderServices = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _orderServices.Reports(DateTime.Now.AddDays(-7)));

            var adminDetailing = orderCarWashWorker.Where(x => x.typeServicesId == 1); // админ детейлинг
            var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 2); // админ мойка

            var adminCarpetWashing = orderCarWashWorker.Where(x => x.typeServicesId == 3); // админ ковры
            var StaffDetailing = orderCarWashWorker.Where(x => x.typeServicesId == 4); // услуги детейлинга
            var staffCarWashWorker = orderCarWashWorker.Where(x => x.typeServicesId == 5); // услуги мойки
            var staffCarWashWorkerCarpetWashing = orderCarWashWorker.Where(x => x.typeServicesId == 6); // услуги по стирке ковров
                                                                                                        // var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 7); // хранение шин админ
                                                                                                        // var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 8); // хранение шин сортудники 

            ViewBag.AdministratorCarWash = adminCarWash;
            ViewBag.AdministratorSum = adminCarWash.Sum(x => x.Payroll); // ЗП админ мойки

            ViewBag.StaffCarWashWorker = staffCarWashWorker;
            ViewBag.StaffCarWashWorkerSum = staffCarWashWorker.Sum(x => x.Payroll);             // ЗП мойщик

            ViewBag.AdministratorCarpetWashing = adminCarpetWashing;
            ViewBag.AdministratorCarpetWashingSum = adminCarpetWashing.Sum(x => x.Payroll);  // ЗА админ коври

            ViewBag.StaffCarpetWashing = staffCarWashWorkerCarpetWashing;
            ViewBag.StaffCarpetWashingSum = staffCarWashWorkerCarpetWashing.Sum(x => x.Payroll);  // ЗП мойщик ковры

            ViewBag.AdministratorDetailings = adminDetailing;
            ViewBag.AdministratorDetailingsSum = adminDetailing.Sum(x => x.Payroll);

            ViewBag.StaffDetailing = StaffDetailing;
            ViewBag.StaffDetailingSum = StaffDetailing.Sum(x => x.Payroll);

            var OrderCarWashList = orderServices.Where(x => x.typeOfOrder == 2);
            var OrderDetelingList = orderServices.Where(x => x.typeOfOrder == 1);
            var OrderCarpetList = orderServices.Where(x => x.typeOfOrder == 3);


            ViewBag.OrderCarWash = OrderCarWashList;
            ViewBag.OrderCarWashSum = OrderCarWashList.Sum(x => x.DiscountPrice);        // Касса мойки

            ViewBag.Detailings = OrderDetelingList;
            ViewBag.OrderDetailing = OrderDetelingList.Sum(x => x.DiscountPrice);      // Касса детейлинг

            ViewBag.CarpetWashing = OrderCarpetList;
            ViewBag.carpetOrder = OrderCarpetList.Sum(x => x.DiscountPrice);      // Касса ковров


           

            return View();
        }

        public async Task<ActionResult> DetailsAboutTheSalaryAdministrator()
        {
            return View();
        }
    }
}