using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.Contract;
using CarDetailingStudio.BLL.Services.Trade.TradeContract;
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
    public partial class AnalyticsController : Controller
    {
        private IIncomeForTheCurrentDay _incomeForTheCurrent;
        private IBonusToSalary _bonusToSalary;
        private ICashier _cashier;
        private IOrderCarWashWorkersServices _orderCarWash;
        private IBrigadeForTodayServices _brigadeForTodayServices;
        private IOrderServicesCarWashServices _orderServices;
        private IExpenses _expenses;
        private IGoodsSold _goodsSold;


        private double sumOfAllExpenses = 0;

        private EmployeeSalariesView employeeSalaries = new EmployeeSalariesView();
        private OrderInformationWashingDetailingView orderInformation = new OrderInformationWashingDetailingView();

        double? test { get; set; }

        public AnalyticsController(
            IIncomeForTheCurrentDay incomeForTheCurrent,
            IBonusToSalary bonusToSalary,
            ICashier cashier,
            IOrderServicesCarWashServices orderServices,
            IOrderCarWashWorkersServices orderCarWash,
            IBrigadeForTodayServices brigadeForTodayServices,
            IExpenses expenses,
            IGoodsSold goodsSold)
        {
            _incomeForTheCurrent = incomeForTheCurrent;
            _bonusToSalary = bonusToSalary;
            _cashier = cashier;
            _orderCarWash = orderCarWash;
            _brigadeForTodayServices = brigadeForTodayServices;
            _orderServices = orderServices;
            _expenses = expenses;
            _goodsSold = goodsSold;
        }

        // GET: Analytics
        public async Task<ActionResult> Report(DateTime? startDate = null, DateTime? finalDate = null)
        {
            if (startDate == null && finalDate == null)
            {
                startDate = DateTime.Now;
            }

            IEnumerable<BonusToSalaryView> BonusToSalary;
            IEnumerable<ExpensesView> salaryExpenses;

            if (finalDate == null)
            {
                sumOfAllExpenses = await _incomeForTheCurrent.SumOfAllExpenses(startDate.Value);
                employeeSalaries = Mapper.Map<EmployeeSalariesView>(await _incomeForTheCurrent.EmployeeSalaries(startDate.Value));
                orderInformation = Mapper.Map<OrderInformationWashingDetailingView>(await _incomeForTheCurrent.AmountOfCompletedOrders(startDate.Value));
                BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(startDate.Value));
                salaryExpenses = Mapper.Map<IEnumerable<ExpensesView>>(await _expenses.Reports(startDate.Value));
            }
            else
            {
                sumOfAllExpenses = await _incomeForTheCurrent.SumOfAllExpenses(startDate.Value, finalDate.Value);
                employeeSalaries = Mapper.Map<EmployeeSalariesView>(await _incomeForTheCurrent.EmployeeSalaries(startDate.Value, finalDate.Value));
                orderInformation = Mapper.Map<OrderInformationWashingDetailingView>(await _incomeForTheCurrent.AmountOfCompletedOrders(startDate.Value, finalDate.Value));
                BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(startDate.Value, finalDate.Value));
                salaryExpenses = Mapper.Map<IEnumerable<ExpensesView>>(await _expenses.Reports(startDate.Value, finalDate.Value));
            }

            var selectSalaryExpenses = salaryExpenses.Where(x => x.expenseCategoryId == 1);
            double salaryExpensesDay = selectSalaryExpenses.Sum(s => s.Amount).Value;
            ViewBag.SalaryExpensesDay = salaryExpensesDay;

            //  Carpet
            CarpetViewBag(employeeSalaries, orderInformation);

            // Tire
            Tire(employeeSalaries, orderInformation);

            //  BonusToSalary
            BonusSalary(BonusToSalary);

            // CarWash
            CarWash(employeeSalaries, orderInformation);

            // Detailings
            Detailings(employeeSalaries, orderInformation);

            //// ЗП Администратора мойки и детейлинга
            //// ЗП Сотрудников мойки и детейлинга
            AdministratorToStaff(employeeSalaries, orderInformation);

            // Наличный безналичный доходы
            ViewBag.Cash = orderInformation.cash;
            ViewBag.NonCash = orderInformation.nonCash;

            //итого доходы
            Total(employeeSalaries, orderInformation, salaryExpenses, sumOfAllExpenses);

            // Количество заказов ожидающих оплату
            ViewBag.NumberOfUnpaidOrders = orderInformation.numberOfUnpaidOrdersCarpetWashing +
                                           orderInformation.numberOfUnpaidOrdersCarWash +
                                           orderInformation.numberOfUnpaidOrdersDetailings;

            var invoice = new DateTime(startDate.Value.Year, startDate.Value.Month, 1);
            ViewBag.InvoiceDate = invoice.ToString("D");

            if (finalDate == null)
            {
                ViewBag.StartDate = startDate.Value.ToString("D");
                ViewBag.DateWhereStart = startDate;
            }
            else
            {
                ViewBag.StartDate = startDate.Value.ToString("D");
                ViewBag.FinalDate = finalDate.Value.ToString("D");

                ViewBag.DateWhereStart = startDate;
                ViewBag.DateWhereFinal = finalDate;
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
        public async Task<ActionResult> AdminReport(bool CloseDay = false, bool message = true)
        {
            var sumOfAllExpenses = await _incomeForTheCurrent.SumOfAllExpenses(DateTime.Now);
            var employeeSalaries = Mapper.Map<EmployeeSalariesView>(await _incomeForTheCurrent.EmployeeSalaries(DateTime.Now));
            var orderInformation = Mapper.Map<OrderInformationWashingDetailingView>(await _incomeForTheCurrent.AmountOfCompletedOrders(DateTime.Now));
            var BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(DateTime.Now));
            var Cashier = Mapper.Map<IEnumerable<CashierView>>(await _cashier.Reports(DateTime.Now));
            var salaryExpenses = Mapper.Map<IEnumerable<ExpensesView>>(await _expenses.Reports(DateTime.Now));

            double salaryExpensesDay = salaryExpenses.Sum(s => s.Amount).Value;
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

        [HttpGet]
        public async Task<ActionResult> SummaryOfTheDay(bool CloseDay = false, bool message = true)
        {
            var sumOfAllExpenses = await _incomeForTheCurrent.SumOfAllExpenses(DateTime.Now);
            var employeeSalaries = Mapper.Map<EmployeeSalariesView>(await _incomeForTheCurrent.EmployeeSalaries(DateTime.Now));
            var orderInformation = Mapper.Map<OrderInformationWashingDetailingView>(await _incomeForTheCurrent.AmountOfCompletedOrders(DateTime.Now));
            var BonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.Reports(DateTime.Now));
            var Cashier = Mapper.Map<IEnumerable<CashierView>>(await _cashier.Reports(DateTime.Now));
            var salaryExpenses = Mapper.Map<IEnumerable<ExpensesView>>(await _expenses.Reports(DateTime.Now));            

            double salaryExpensesDay = salaryExpenses.Sum(s => s.Amount).Value;
            double cashierDayStart = Cashier.Sum(x => x.amountBeginningOfTheDay);

            // Carpet
            CarpetViewBag(employeeSalaries, orderInformation);

            // Tire
            Tire(employeeSalaries, orderInformation);

            // BonusToSalary
            BonusSalary(BonusToSalary);

            // CarWash
            CarWash(employeeSalaries, orderInformation);

            // Detailings
            Detailings(employeeSalaries, orderInformation);

            //// ЗП Администратора мойки и детейлинга
            //// ЗП Сотрудников мойки и детейлинга
            AdministratorToStaff(employeeSalaries, orderInformation);

            // Наличный безналичный доходы
            ViewBag.Cash = orderInformation.cash;
            ViewBag.NonCash = orderInformation.nonCash;

            //итого доходы
            Total(employeeSalaries, orderInformation, salaryExpenses, sumOfAllExpenses);


            // Количество заказов ожидающих оплату
            ViewBag.NumberOfUnpaidOrders = orderInformation.numberOfUnpaidOrdersCarpetWashing +
                                           orderInformation.numberOfUnpaidOrdersCarWash +
                                           orderInformation.numberOfUnpaidOrdersDetailings;

            ViewBag.SalaryExpenses = salaryExpensesDay;                                                             // Выданно наличных за текущий день 
            ViewBag.CashStartDay = cashierDayStart;                                                                 // Касса начало дня (наличные)

            ViewBag.Cash = orderInformation.cash;                                                                   // Приход наличных
            ViewBag.NonCash = orderInformation.nonCash;                                                             // Приход безналичных

            ViewBag.CashEndDay = orderInformation.cash - sumOfAllExpenses + cashierDayStart + orderInformation.tireCeash;                     // Касса конец дня  (наличные)

            //итого доходы
            double goodsSoldResult = await GoodsSoldAnalytics();
            ViewBag.TotalIncome = orderInformation.OrderCarWashSum + orderInformation.OrderDetailing + orderInformation.carpetOrder + goodsSoldResult;

            ViewBag.StartDate = DateTime.Now.ToString("D");

            ViewBag.DateWhereStart = DateTime.Now;
            ViewBag.CloseDay = CloseDay;

            ViewBag.Message = message;

            ViewBag.Date = DateTime.Now.AddDays(1).ToString("d");

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
            DateTime date = DateTime.Now.AddDays(-3);
            var orderCarWashWorker = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWash.Reports(date));
            var brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigadeForTodayServices.Reports(date));
            var orderServices = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _orderServices.Reports(date));

            var adminDetailing = orderCarWashWorker.Where(x => x.typeServicesId == 1);                  // админ детейлинг
            var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 2);                    // админ мойка

            var adminCarpetWashing = orderCarWashWorker.Where(x => x.typeServicesId == 3);              // админ ковры
            var StaffDetailing = orderCarWashWorker.Where(x => x.typeServicesId == 4);                  // услуги детейлинга
            var staffCarWashWorker = orderCarWashWorker.Where(x => x.typeServicesId == 5);              // услуги мойки
            var staffCarWashWorkerCarpetWashing = orderCarWashWorker.Where(x => x.typeServicesId == 6); // услуги по стирке ковров
                                                                                                        // var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 7); // хранение шин админ
                                                                                                        // var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 8); // хранение шин сортудники 

            ViewBag.AdministratorCarWash = adminCarWash;
            ViewBag.AdministratorSum = adminCarWash.Sum(x => x.Payroll);                                // ЗП админ мойки

            ViewBag.StaffCarWashWorker = staffCarWashWorker;
            ViewBag.StaffCarWashWorkerSum = staffCarWashWorker.Sum(x => x.Payroll);                     // ЗП мойщик

            ViewBag.AdministratorCarpetWashing = adminCarpetWashing;
            ViewBag.AdministratorCarpetWashingSum = adminCarpetWashing.Sum(x => x.Payroll);             // ЗА админ коври

            ViewBag.StaffCarpetWashing = staffCarWashWorkerCarpetWashing;
            ViewBag.StaffCarpetWashingSum = staffCarWashWorkerCarpetWashing.Sum(x => x.Payroll);        // ЗП мойщик ковры

            ViewBag.AdministratorDetailings = adminDetailing;
            ViewBag.AdministratorDetailingsSum = adminDetailing.Sum(x => x.Payroll);

            ViewBag.StaffDetailing = StaffDetailing;
            ViewBag.StaffDetailingSum = StaffDetailing.Sum(x => x.Payroll);

            var OrderCarWashList = orderServices.Where(x => x.typeOfOrder == 2);
            var OrderDetelingList = orderServices.Where(x => x.typeOfOrder == 1);
            var OrderCarpetList = orderServices.Where(x => x.typeOfOrder == 3);


            ViewBag.OrderCarWash = OrderCarWashList;
            ViewBag.OrderCarWashSum = OrderCarWashList.Sum(x => x.DiscountPrice);                       // Касса мойки

            ViewBag.Detailings = OrderDetelingList;
            ViewBag.OrderDetailing = OrderDetelingList.Sum(x => x.DiscountPrice);                       // Касса детейлинг

            ViewBag.CarpetWashing = OrderCarpetList;
            ViewBag.carpetOrder = OrderCarpetList.Sum(x => x.DiscountPrice);                            // Касса ковров




            return View();
        }

        public async Task<ActionResult> DetailsAboutTheSalaryAdministrator()
        {
            return View();
        }

        public async Task<ActionResult> DetailsAboutOrders(int typeOrder, DateTime start, DateTime? finlDate = null)
        {
            var resultOrder = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _incomeForTheCurrent.AboutOrders(typeOrder, start, finlDate));

            // Количество машин
            ViewBag.CarCaunt = resultOrder.Count();

            // Сумма всех заказов
            ViewBag.SumOedweAll = resultOrder.Sum(x => x.DiscountPrice);

            // количество не оплаченных заказов
            var SumUnpaidOrders = resultOrder.Where(x => x.StatusOrder == 4);
            ViewBag.CountUnpaidOrders = SumUnpaidOrders.Count();

            // Сумма не оплаченных заказов
            ViewBag.SumUnpaidOrders = SumUnpaidOrders.Sum(x => x.DiscountPrice);

            var cashWhere = resultOrder.Where(x => x.PaymentState == 1);
            var nonCashWhere = resultOrder.Where(x => x.PaymentState == 2);

            ViewBag.Cash = cashWhere.Sum(s => s.DiscountPrice);                 // Наличный расчет
            ViewBag.nonCash = nonCashWhere.Sum(s => s.DiscountPrice);           // Безналичный расчет  

            ViewBag.TypeOrder = typeOrder;
            return View(resultOrder);
        }

        public async Task<ActionResult> DetailsOfSalaries(int typeOfEmployees, DateTime start, DateTime? finlDate = null)
        {
            var resultDatailsOfSalaries = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _incomeForTheCurrent.AboutWages(typeOfEmployees, start, finlDate));

            if (typeOfEmployees == 2 || typeOfEmployees == 5)
            {
                int idCarpet = typeOfEmployees + 1;

                var resultDatailsOfSalariesCarpet = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _incomeForTheCurrent.AboutWages(idCarpet, start, finlDate));
                var tableResultConcat = resultDatailsOfSalaries.Concat(resultDatailsOfSalariesCarpet);

                WebViewBagDetailsOfSalaries(tableResultConcat, typeOfEmployees);

                return View(tableResultConcat);
            }

            WebViewBagDetailsOfSalaries(resultDatailsOfSalaries, typeOfEmployees);
            return View(resultDatailsOfSalaries);
        }

        public void WebViewBagDetailsOfSalaries(IEnumerable<OrderCarWashWorkersView> orderCarWashes, int typeOfEmployees)
        {
            ViewBag.TypeOfEmployees = typeOfEmployees;

            // Всего на зарплату
            ViewBag.SumOfAllOrdersPayroll = orderCarWashes.Sum(s => s.Payroll);

            //  Сумма всех заказов
            ViewBag.SumOfAllOrdersDiscountPrice = orderCarWashes.Sum(s => s.OrderServicesCarWash.DiscountPrice);

            if (typeOfEmployees == 2 || typeOfEmployees == 5)
            {
                // Сумма заказов мойки
                var amountOfWashingOrders = orderCarWashes.Where(x => x.typeServicesId == typeOfEmployees);

                ViewBag.AmountOfWashingOrdersPayroll = amountOfWashingOrders.Sum(x => x.Payroll);
                ViewBag.AmountOfWashingOrdersDiscountPrice = amountOfWashingOrders.Sum(x => x.OrderServicesCarWash.DiscountPrice);

                int idCarpet = typeOfEmployees + 1;
                var amountOfWashingOrdersCarpet = orderCarWashes.Where(x => x.typeServicesId == idCarpet);

                ViewBag.AmountOfWashingOrdersCarpetSumPayroll = amountOfWashingOrdersCarpet.Sum(x => x.Payroll);
                ViewBag.AmountOfWashingOrdersCarpetSumDiscountPrice = amountOfWashingOrdersCarpet.Sum(x => x.OrderServicesCarWash.DiscountPrice);

            }
        }

        #region ViewBag

        public void Tire(EmployeeSalariesView employeeSalaries,
                               OrderInformationWashingDetailingView orderInformation)
        {

            // Tire 
            ViewBag.AdministratorTire = employeeSalaries.AdministratorTire;     // ЗП администратор шинтмонтаж
            ViewBag.StaffTire = employeeSalaries.StaffTire;                     // ЗП сотрудников
            ViewBag.TireOrders = orderInformation.TireOrders;                   // Заказ шиномантаж
            ViewBag.CauntTire = orderInformation.CauntTire;                     // Количество заказов шиномантаж
            ViewBag.numberOfUnpaidTire = orderInformation.numberOfUnpaidTire;   // Количество заказов ожидающих оплату (Шиномонтаж)
            ViewBag.TireCeash = orderInformation.tireCeash;                     // шиномонта наличные
        }

        public async Task<double> GoodsSoldAnalytics()
        {
            var goodsSold = Mapper.Map<IEnumerable<GoodsSoldView>>(await _goodsSold.Reports(DateTime.Now));

            double resultReturn = goodsSold.Sum(s => s.orderPrice).Value;

            ViewBag.CauntGoodsSold = goodsSold.Sum(x => x.amount); // Количество проданных товаров
            ViewBag.SumGoodsSold = resultReturn; // Касса проданных товаров
            ViewBag.SumAdminstrator = goodsSold.Sum(admin => admin.percentageOfSale); // Зп админа

            return resultReturn;
        }
            
        public void CarpetViewBag(EmployeeSalariesView employeeSalaries,
                               OrderInformationWashingDetailingView orderInformation)
        {
            //  Carpet
            ViewBag.CarpetOrder = orderInformation.carpetOrder; // Касса ковров  
            ViewBag.CauntCarpet = orderInformation.CauntCarpet; // Количество заказов ковров
            ViewBag.AdminCarpet = employeeSalaries.adminCarpet;
            ViewBag.StaffCarpet = employeeSalaries.staffCarpet;
        }

        public void CarWash(EmployeeSalariesView employeeSalaries,
                               OrderInformationWashingDetailingView orderInformation)
        {
            ViewBag.OrderCarWashSum = orderInformation.OrderCarWashSum; // Касса мойки
            ViewBag.OrderCount = orderInformation.OrderCount; // Количество Авто мойка
        }

        public void Detailings(EmployeeSalariesView employeeSalaries,
                             OrderInformationWashingDetailingView orderInformation)
        {
            ViewBag.OrderDetailingsSum = orderInformation.OrderDetailing; // Касса детейлинг
            ViewBag.CarCountDetailings = orderInformation.CarCountDetailings; // Количество авто детейлинг
        }

        public void BonusSalary(IEnumerable<BonusToSalaryView> BonusToSalary)
        {
            ViewBag.BonusToSalarySum = BonusToSalary.Sum(x => x.amount);
            ViewBag.BonusToSalaryCount = BonusToSalary.Count();
        }

        public void Total(EmployeeSalariesView employeeSalaries,
                             OrderInformationWashingDetailingView orderInformation,
                             IEnumerable<ExpensesView> salaryExpenses, double sumOfAllExpenses)
        {
            double salaryExpensesDay = salaryExpenses.Sum(s => s.Amount).Value;

            ViewBag.TotalIncome = orderInformation.OrderCarWashSum + orderInformation.OrderDetailing + orderInformation.carpetOrder;

            //итого расходы на обслуживание 
            ViewBag.TotalCosts = sumOfAllExpenses;

            //Итого расходы на зарплату
            ViewBag.TotalSalaryExpenses = employeeSalaries.adminCarpet + employeeSalaries.staffCarpet + employeeSalaries.AdministratorCarWash + employeeSalaries.AdministratorDetailings + employeeSalaries.StaffCarWashWorker + employeeSalaries.StaffDetailing;

            //Итого
            ViewBag.Total = ViewBag.TotalIncome - ViewBag.TotalCosts - ViewBag.TotalSalaryExpenses;
            //Итого
            ViewBag.TotalCash = ViewBag.TotalIncome - ViewBag.TotalCosts - salaryExpensesDay;
        }

        public void AdministratorToStaff(EmployeeSalariesView employeeSalaries,
                             OrderInformationWashingDetailingView orderInformation)
        {
            // ЗП Администратора мойки и детейлинга
            ViewBag.AdministratorCarWash = employeeSalaries.AdministratorCarWash + employeeSalaries.adminCarpet;
            ViewBag.AdministratorDetailings = employeeSalaries.AdministratorDetailings;

            // ЗП Сотрудников мойки и детейлинга
            ViewBag.StaffCarWashWorker = employeeSalaries.StaffCarWashWorker + employeeSalaries.staffCarpet;
            ViewBag.StaffDetailing = employeeSalaries.StaffDetailing;

            ViewBag.OrderCarWashSum = orderInformation.OrderCarWashSum;                                             // Касса мойки
            ViewBag.OrderCount = orderInformation.OrderCount;                                                       // Количество Авто мойка
            ViewBag.OrderDetailingsSum = orderInformation.OrderDetailing;                                           // Касса детейлинг

        }
        #endregion
    }
}