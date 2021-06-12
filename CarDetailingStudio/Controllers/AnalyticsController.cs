using AutoMapper;
using CarDetailingStudio.BLL;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractFactory;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.AnalyticsView;
using CarDetailingStudio.Models.AnalyticsView.WagesForCompletedOrders;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
    [AuthorizeAttribute]
    [Authorize(Roles = "Admin, Owner, Manager, SuperUser")]
    public partial class AnalyticsController : Controller
    {
        private readonly ICashier _cashier;
        private readonly IAbstractFactory _abstractFactory;

        public AnalyticsController(
            ICashier cashier,
            IAbstractFactory abstractFactory)
        {
            _cashier = cashier;
            _abstractFactory = abstractFactory;
        }

        public async Task<ActionResult> VisualizeStudentResult()
            => Json(Mapper.Map<IEnumerable<IncomeView>>(await _abstractFactory.AnalyticsFinance(DateTime.Now)).ToList(), JsonRequestBehavior.AllowGet);
        

        // GET: Analytics
        [HttpGet]
        public async Task<ActionResult> Report(DateTime? startDate = null, DateTime? finalDate = null)
        {
            if (startDate == null && finalDate == null)
            {
                startDate = DateTime.Now.AddDays(-7);
                finalDate = DateTime.Now;
            }
            else if (finalDate == null)
            {
                startDate = DateTime.Now.AddDays(-7);
                finalDate = DateTime.Now;
            }

            var analyticsResult = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsForTheSelectedPeriod(startDate.Value, finalDate.Value));
            var analyticsCash = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsForTheSelectedPeriodFormOfPayment(startDate.Value, finalDate.Value, (int)PaymentMethod.cash));
            var analyticsNoCash = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsForTheSelectedPeriodFormOfPayment(startDate.Value, finalDate.Value, (int)PaymentMethod.nonСash));
            var Cashier = Mapper.Map<IEnumerable<CashierView>>(await _cashier.Reports(startDate.Value, finalDate.Value));

            AnalyticsFull analyticsFull = new AnalyticsFull();

            analyticsFull.analyticsViewResult = new AnalyticsView();
            analyticsFull.analyticsViewResult = analyticsResult;

            analyticsFull.analyticsViewCash = new AnalyticsView();
            analyticsFull.analyticsViewCash = analyticsCash;

            analyticsFull.analyticsViewNoCash = new AnalyticsView();
            analyticsFull.analyticsViewNoCash = analyticsNoCash;

            analyticsFull.cashStartOfTheDay = Cashier.Sum(x => x.amountBeginningOfTheDay);

            // analyticsFull.CashEndDay = SumTotalGeneral(analyticsResult);
            // analyticsFull.CashEndDayCash = SumTotal(analyticsCash) + analyticsFull.cashStartOfTheDay;
            // analyticsFull.CashEndDayNoCash = SumTotal(analyticsNoCash);

            analyticsFull.SumWegesAdministrator = analyticsResult.wagesForCompletedOrders.CarpetWashing.SalaryAdministrator + analyticsResult.wagesForCompletedOrders.Washing.SalaryAdministrator;
            analyticsFull.SumWegesEmployees = analyticsResult.wagesForCompletedOrders.CarpetWashing.SalaryEmployees + analyticsResult.wagesForCompletedOrders.Washing.SalaryEmployees;
            analyticsFull.SumPendingPayment = SumOrderPendingPayment(analyticsResult);

            ViewBag.StartDate = DateTime.Now.ToString("D");
            ViewBag.DateWhereStart = DateTime.Now;
            ViewBag.Date = DateTime.Now.AddDays(1).ToString("d");

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

            var resultIncome = Mapper.Map<AnalyticsIncomeView>(await _abstractFactory.AnalyticsFinance(startDate.Value, finalDate));

            analyticsResult.incomeViews = resultIncome.incomeViews;
            analyticsResult.paymentofArrears = resultIncome.paymentofArrears;

            analyticsFull.PaymentOfArrearsTotal = resultIncome.paymentofArrears.Sum(s => s.SumOfIncome);
            analyticsFull.PaymentOfArrearsCash = resultIncome.paymentofArrears.Sum(s => s.SumCash);
            analyticsFull.PaymentOfArrearsNoCash = resultIncome.paymentofArrears.Sum(s => s.SumNoCash);

            analyticsFull.CashEndDay = Math.Round(analyticsResult.incomeViews.Sum(s => s.SumOfIncome) - analyticsResult.expensesClassModels.Sum(s => s.Amount), 1);
            analyticsFull.CashEndDayCash = SumCashEndDayCash(analyticsResult.incomeViews.Sum(s => s.SumCash), analyticsCash.expensesClassModels.Sum(s => s.Amount), analyticsFull.cashStartOfTheDay, analyticsFull.PaymentOfArrearsCash);
            analyticsFull.CashEndDayNoCash = Math.Round(analyticsResult.incomeViews.Sum(s => s.SumNoCash) - analyticsNoCash.expensesClassModels.Sum(s => s.Amount), 1);
            analyticsFull.SumTotal = Math.Round(analyticsFull.CashEndDayCash + analyticsFull.cashStartOfTheDay, 1);
            analyticsFull.Profit = Math.Round(analyticsResult.incomeViews.Sum(x => x.SumOfIncome) - analyticsResult.expensesClassModels.Sum(x => x.Amount), 1);

            analyticsFull.SumTotal = Math.Round(analyticsResult.incomeViews.Sum(s => s.SumOfIncome) + analyticsResult.incomeViews.Sum(s => s.AwaitingPayment));

            return View(analyticsFull);
        }

        private double SumCashEndDayCash(double income, double expenses, double cashStartOfTheDay, double PaymentOfArrearsCash) => Math.Round(income + cashStartOfTheDay + PaymentOfArrearsCash - expenses, 1);

        [HttpPost]
        public ActionResult MonthlyReport(DateTime? startdateViewUser, DateTime? finaldateViewUser)
        {
            return RedirectToAction("Report", "Analytics", new RouteValueDictionary(new
            {
                startDate = startdateViewUser,
                finalDate = finaldateViewUser
            }));
        }

        #region SummaryOfTheDay
        [HttpGet]
        public async Task<ActionResult> SummaryOfTheDay(bool CloseDay = false, bool message = true)
        {
            var analyticsResult = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsForTheDay(DateTime.Now));
            var analyticsCash = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsPerDayFormOfPayment(DateTime.Now, (int)PaymentMethod.cash));
            var analyticsNoCash = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsPerDayFormOfPayment(DateTime.Now, (int)PaymentMethod.nonСash));
            var Cashier = Mapper.Map<IEnumerable<CashierView>>(await _cashier.Reports(DateTime.Now));

            AnalyticsFull analyticsFull = new AnalyticsFull();

            analyticsFull.analyticsViewResult = new AnalyticsView();
            analyticsFull.analyticsViewResult = analyticsResult;

            analyticsFull.analyticsViewCash = new AnalyticsView();
            analyticsFull.analyticsViewCash = analyticsCash;

            analyticsFull.analyticsViewNoCash = new AnalyticsView();
            analyticsFull.analyticsViewNoCash = analyticsNoCash;

            analyticsFull.cashStartOfTheDay = Math.Round(Cashier.Sum(x => x.amountBeginningOfTheDay), 1);

            analyticsFull.SumWegesAdministrator = Math.Round(analyticsResult.wagesForCompletedOrders.CarpetWashing.SalaryAdministrator
                                                + analyticsResult.wagesForCompletedOrders.Washing.SalaryAdministrator, 1);

            analyticsFull.SumWegesEmployees = Math.Round(analyticsResult.wagesForCompletedOrders.CarpetWashing.SalaryEmployees
                                            + analyticsResult.wagesForCompletedOrders.Washing.SalaryEmployees, 1);

            analyticsFull.SumPendingPayment = Math.Round(SumOrderPendingPayment(analyticsResult), 1);

            //analyticsFull.CashEndDay = Math.Round(SumTotalGeneral(analyticsCash), 1);
            //analyticsFull.CashEndDayCash = Math.Round(SumTotal(analyticsCash)
            //                            + analyticsFull.cashStartOfTheDay
            //                            + SumOrdersForThePreviousPeriod(analyticsCash.informationPreviousPeriod), 1);

            //analyticsFull.CashEndDayCash = analyticsFull.CashEndDayCash - analyticsCash.expensesClassModels.Sum(x => x.Amount);
            //analyticsFull.CashEndDayNoCash = Math.Round(SumTotal(analyticsNoCash) 
            //                               + SumOrdersForThePreviousPeriod(analyticsNoCash.informationPreviousPeriod), 1);

            ViewBag.StartDate = DateTime.Now.ToString("D");
            ViewBag.DateWhereStart = DateTime.Now;
            ViewBag.CloseDay = CloseDay;
            ViewBag.Message = message;
            ViewBag.Date = DateTime.Now.AddDays(1).ToString("d");

            var resultIncome = Mapper.Map<AnalyticsIncomeView>(await _abstractFactory.AnalyticsFinance(DateTime.Now));

            analyticsResult.incomeViews = resultIncome.incomeViews;
            analyticsResult.paymentofArrears = resultIncome.paymentofArrears;

            analyticsFull.PaymentOfArrearsTotal = resultIncome.paymentofArrears.Sum(s => s.SumOfIncome);
            analyticsFull.PaymentOfArrearsCash = resultIncome.paymentofArrears.Sum(s => s.SumCash);
            analyticsFull.PaymentOfArrearsNoCash = resultIncome.paymentofArrears.Sum(s => s.SumNoCash);

            analyticsFull.CashEndDay = Math.Round(analyticsResult.incomeViews.Sum(s => s.SumOfIncome) - analyticsResult.expensesClassModels.Sum(s => s.Amount), 1);
            analyticsFull.CashEndDayCash = SumCashEndDayCash(analyticsResult.incomeViews.Sum(s => s.SumCash), analyticsCash.expensesClassModels.Sum(s => s.Amount), analyticsFull.cashStartOfTheDay, analyticsFull.PaymentOfArrearsCash);
            analyticsFull.CashEndDayNoCash = Math.Round(analyticsResult.incomeViews.Sum(s => s.SumNoCash) - analyticsNoCash.expensesClassModels.Sum(s => s.Amount), 1);
            analyticsFull.SumTotal = Math.Round(analyticsFull.CashEndDayCash + analyticsFull.cashStartOfTheDay, 1);

            analyticsFull.SumTotal = Math.Round(analyticsResult.incomeViews.Sum(s => s.SumOfIncome) + analyticsResult.incomeViews.Sum(s => s.AwaitingPayment));
            return View(analyticsFull);
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
        #endregion

        #region private 
        private double SumOrderPendingPayment(AnalyticsView analyticsViews)
        {
            double washing = analyticsViews.completedOrders.Washing.OrdersAwaitingPaymentCashier;
            double detailing = analyticsViews.completedOrders.Detailing.OrdersAwaitingPaymentCashier;
            double tireFitting = analyticsViews.completedOrders.TireFitting.OrdersAwaitingPaymentCashier;
            double carpetWashing = analyticsViews.completedOrders.CarpetWashing.OrdersAwaitingPaymentCashier;
            double tireStorage = analyticsViews.completedOrders.TireStorage.OrdersAwaitingPaymentCashier;

            return Math.Round(washing + detailing + tireFitting + tireStorage + carpetWashing, 1);
        }

        private double SumTotal(AnalyticsView analyticsView)
            => Math.Round(
                analyticsView.completedOrders.Washing.Cashier
                + analyticsView.completedOrders.Detailing.Cashier
                + analyticsView.completedOrders.CarpetWashing.Cashier
                + analyticsView.completedOrders.TireFitting.Cashier
                + analyticsView.completedOrders.TireStorage.Cashier
                + analyticsView.saleOfGoods.SumGoodsSold
                + analyticsView.additionalIncome.CashierAvtomir
                + analyticsView.additionalIncome.CashierCaravan
                + analyticsView.additionalIncome.CashierDryCleaningKohler, 1);

        private double SumTotalGeneral(AnalyticsView analyticsView)
        {
            var services = analyticsView.completedOrders.Washing.SumCashierAndOrdersAwaitingPaymentCashier + analyticsView.completedOrders.Detailing.SumCashierAndOrdersAwaitingPaymentCashier +
                   analyticsView.completedOrders.CarpetWashing.SumCashierAndOrdersAwaitingPaymentCashier + analyticsView.completedOrders.TireFitting.SumCashierAndOrdersAwaitingPaymentCashier +
                   analyticsView.completedOrders.TireStorage.SumCashierAndOrdersAwaitingPaymentCashier;

            var wages = analyticsView.wagesForCompletedOrders.Washing.SalaryAdministrator + analyticsView.wagesForCompletedOrders.Washing.SalaryEmployees +
                   analyticsView.wagesForCompletedOrders.Detailing.SalaryAdministrator + analyticsView.wagesForCompletedOrders.Detailing.SalaryEmployees +
                   analyticsView.wagesForCompletedOrders.TireFitting.SalaryAdministrator + analyticsView.wagesForCompletedOrders.TireFitting.SalaryEmployees +
                   analyticsView.wagesForCompletedOrders.TireStorage.SalaryAdministrator + analyticsView.wagesForCompletedOrders.TireStorage.SalaryEmployees +
                   analyticsView.wagesForCompletedOrders.SumBonusTOSalary + analyticsView.wagesForCompletedOrders.SumEmployeeRate;

            var expenses = analyticsView.expensesClassModels.Where(x => x.expenseCategoryId != 1).Sum(s => s.Amount);

            var resultServAndWages = services - wages;

            return Math.Round((resultServAndWages + analyticsView.saleOfGoods.SumGoodsSold + analyticsView.additionalIncome.CashierAvtomir +
                   analyticsView.additionalIncome.CashierCaravan + analyticsView.additionalIncome.CashierDryCleaningKohler) - expenses, 1);
        }

        private double SumOrdersForThePreviousPeriod(InformationPreviousPeriod informationSum)
        {
            return Math.Round(informationSum.CarpetWashing.OrderSum + informationSum.Washing.OrderSum + informationSum.Detailing.OrderSum +
                    informationSum.TireFitting.OrderSum + informationSum.TireStorage.OrderSum, 1);
        }
        #endregion

        public async Task<ActionResult> DetailsAboutOrders(int typeOrder, int paymentState, DateTime start, DateTime? finlDate, int type = 0, int statusOrder = 0)
        {
            ViewBagDateGroup(start, finlDate);
            ViewBag.TypeOrder = typeOrder;

            if (finlDate == null)
            {
                if (paymentState == 0)
                {
                    if (statusOrder != 0)
                    {
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, start, statusOrder)));
                    }

                    if (type == 0)
                    {
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, start)));
                    }
                    else
                    {
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.InformationAboutOrderForThePreviousPeriod(typeOrder, start)));
                    }
                }
                else
                {
                    if (type == 0)
                    {
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, paymentState, start)));
                    }
                    else
                    {
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.InformationAboutOrderForThePreviousPeriod(typeOrder, paymentState, start)));
                    }
                }
            }
            else
            {
                if (paymentState == 0)
                {
                    if (statusOrder != 0)
                    {
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, start, finlDate, statusOrder)));
                    }

                    return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, start, finlDate)));
                }
                else
                {
                    return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, paymentState, start, finlDate)));
                }
            }
        }

        public async Task<ActionResult> DetailsExpenses(int paymentState, DateTime dateStart, DateTime? dateFinal, int typeExpenses = 0)
        {
            ViewBag.TypeExpenses = typeExpenses;
            ViewBagDateGroup(dateStart, dateFinal);

            if (dateFinal == null)
            {
                if (typeExpenses == 1)
                {
                    return View(Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsSalaryExpenses(1, dateStart)));
                }

                if (paymentState != 0)
                {
                    return View(Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsExpenses(typeExpenses, paymentState, dateStart)));
                }

                return View(Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsExpenses(typeExpenses, dateStart)));
            }
            else
            {
                if (paymentState != 0)
                {
                    return View(Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsExpenses(typeExpenses, paymentState, dateStart, dateFinal)));
                }

                return View(Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsExpenses(typeExpenses, dateStart, dateFinal)));
            }
        }

        public async Task<ActionResult> ExpensesGroup(int paymentState, DateTime dateStart, DateTime? dateFinal, int typeExpenses = 0)
        {
            ViewBag.PaymentState = paymentState;
            ViewBag.DateStart = dateStart;
            ViewBag.DateFinal = dateFinal;
            ViewBagDateGroup(dateStart, dateFinal);

            if (dateFinal == null)
            {
                if (paymentState != 0)
                {
                    return View(Mapper.Map<IEnumerable<ExpensesClassView>>(await _abstractFactory.ExpensesGroup(paymentState, dateStart, typeExpenses)));
                }

                return View(Mapper.Map<IEnumerable<ExpensesClassView>>(await _abstractFactory.ExpensesGroup(dateStart, typeExpenses)));
            }
            else
            {
                if (paymentState != 0)
                {
                    return View(Mapper.Map<IEnumerable<ExpensesClassView>>(await _abstractFactory.ExpensesGroup(paymentState, dateStart, dateFinal, typeExpenses)));
                }

                return View(Mapper.Map<IEnumerable<ExpensesClassView>>(await _abstractFactory.ExpensesGroup(dateStart, dateFinal, typeExpenses)));
            }
        }

        public async Task<ActionResult> WagesInfo(int? typeExpenses, DateTime? dateStart, DateTime? dateFinal)
        {
            if (typeExpenses == null)
            {
                return RedirectToAction("Report", "Analytics", new RouteValueDictionary(new
                {
                    startDate = DateTime.Now
                }));
            }

            ViewBag.TypeExpenses = typeExpenses;
            ViewBag.DateWhereStart = dateStart;
            ViewBag.DateWhereFinal = dateFinal;
            ViewBagDateGroup(dateStart.Value, dateFinal);

            if (dateFinal == null)
            {
                return View(Mapper.Map<IEnumerable<GroupingEmployeesWagesView>>(await _abstractFactory.GroupDetailsWages(typeExpenses.Value, dateStart.Value)));
            }
            else
            {
                return View(Mapper.Map<IEnumerable<GroupingEmployeesWagesView>>(await _abstractFactory.GroupDetailsWages(typeExpenses.Value, dateStart.Value, dateFinal)));
            }
        }

        public async Task<ActionResult> GoodsSoldInfo(int paymentState, DateTime dateStart, DateTime? dateFinal)
        {
            ViewBagDateGroup(dateStart, dateFinal);

            if (dateFinal == null)
            {
                if (paymentState != 0)
                {
                    return View(Mapper.Map<IEnumerable<GoodsSoldView>>(await _abstractFactory.DetailsGoodsSold(paymentState, dateStart)));
                }

                return View(Mapper.Map<IEnumerable<GoodsSoldView>>(await _abstractFactory.DetailsGoodsSold(dateStart)));
            }
            else
            {
                if (paymentState != 0)
                {
                    return View(Mapper.Map<IEnumerable<GoodsSoldView>>(await _abstractFactory.DetailsGoodsSold(paymentState, dateStart, dateFinal)));
                }

                return View(Mapper.Map<IEnumerable<GoodsSoldView>>(await _abstractFactory.DetailsGoodsSold(dateStart, dateFinal)));
            }
        }

        public async Task<ActionResult> AdditionalIncomeInfo(string category, int paymentState, DateTime dateStart, DateTime? dateFinal)
        {
            ViewBagDateGroup(dateStart, dateFinal);

            if (dateFinal == null)
            {
                if (paymentState != 0)
                {
                    return View(Mapper.Map<IEnumerable<AdditionalIncomeBll>>(await _abstractFactory.DetailsAdditionalIncome(category, paymentState, dateStart)));
                }

                return View(Mapper.Map<IEnumerable<Models.ModelViews.AdditionalIncomeView>>(await _abstractFactory.DetailsAdditionalIncome(category, dateStart)));
            }
            else
            {
                if (paymentState != 0)
                {
                    return View(Mapper.Map<IEnumerable<Models.ModelViews.AdditionalIncomeView>>(await _abstractFactory.DetailsAdditionalIncome(category, paymentState, dateStart, dateFinal)));
                }

                return View(Mapper.Map<IEnumerable<Models.ModelViews.AdditionalIncomeView>>(await _abstractFactory.DetailsAdditionalIncome(category, dateStart, dateFinal)));
            }
        }

        [HttpGet]
        public async Task<ActionResult> WagesInfoGroup(int? typeExpenses, int? idEmployees, DateTime? dateStart, DateTime? dateFinal)
        {
            if (idEmployees == null)
            {
                return RedirectToAction("Report", "Analytics", new RouteValueDictionary(new
                {
                    startDate = DateTime.Now
                }));
            }

            ViewBagDateGroup(dateStart.Value, dateFinal);

            if (dateFinal == null)
            {
                return View(Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _abstractFactory.DetailsWages(typeExpenses.Value, idEmployees.Value, dateStart.Value)));
            }
            else
            {
                return View(Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _abstractFactory.DetailsWages(typeExpenses.Value, idEmployees.Value, dateStart.Value, dateFinal)));
            }
        }



        public async Task<ActionResult> GroupDetailsWages(DateTime? dateStart, DateTime? dateFinal, bool? page)
        {
            if (page == null)
            {
                if (dateStart == null && dateFinal == null)
                {
                    dateStart = DateTime.Now.AddDays(-7);
                    dateFinal = DateTime.Now;
                }
                else if (dateFinal == null)
                {
                    dateStart = DateTime.Now.AddDays(-7);
                    dateFinal = DateTime.Now;
                }
            }

            ViewBagDateGroup(dateStart.Value, dateFinal);

            if (dateFinal == null)
            {
                return View(Mapper.Map<IEnumerable<GroupingEmployeesWagesView>>(await _abstractFactory.GroupDetailsWages(dateStart.Value)));
            }
            else
            {
                var groupingEmployeesByPeriod = Mapper.Map<IEnumerable<GroupingEmployeesWagesView>>(await _abstractFactory.GroupingEmployeesByPeriod(dateStart.Value, dateFinal));
                var groupDetailsWages = Mapper.Map<IEnumerable<GroupingEmployeesWagesView>>(await _abstractFactory.GroupDetailsWages(dateStart.Value, dateFinal));

                GroupingEmployeesWagesTwoTable result = new GroupingEmployeesWagesTwoTable();

                result.groupDetailsWages = groupDetailsWages;
                result.groupingEmployeesByPeriod = groupingEmployeesByPeriod;

                return View(result);
            }
        }

        public async Task<ActionResult> TotalChargesForAllServices(int? idEmployees, string nameEmployees, DateTime? dateStart, DateTime? dateFinal)
        {
            if (idEmployees == null)
            {
                return RedirectToAction("Report", "Analytics", new RouteValueDictionary(new
                {
                    startDate = DateTime.Now
                }));
            }

            ViewBagDateGroup(dateStart.Value, dateFinal);
            ViewBag.Name = nameEmployees;
            ViewBag.Employees = idEmployees;

            if (dateFinal == null)
                return View(Mapper.Map<AnalyticsView>(await _abstractFactory.InformationOnAllWages(idEmployees.Value, dateStart.Value)));
            else
                return View(Mapper.Map<AnalyticsView>(await _abstractFactory.InformationOnAllWages(idEmployees.Value, dateStart.Value, dateFinal)));
        }

        public async Task<ActionResult> ServiceDebt(int typeOrder, DateTime start, DateTime? finlDate)
        {
            ViewBagDateGroup(start, finlDate);

            if (finlDate == null)
            {
                return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.PaidServiceDebt(typeOrder, start)));
            }
            else
            {
                return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.PaidServiceDebt(typeOrder, start, finlDate)));
            }
        }

        #region ViewBag

        public void ViewBagDateGroup(DateTime dateStart, DateTime? dateFinal = null)
        {
            List<DateTime> dateTimesGrups = new List<DateTime>();

            if (dateFinal != null)
            {
                for (DateTime date = dateStart.Date; date <= dateFinal.Value.Date; date = date.AddDays(1.0))
                {
                    dateTimesGrups.Add(date);
                }
            }
            else
            {
                dateTimesGrups.Add(dateStart.Date);
            }

            ViewBag.ExpensesDate = dateTimesGrups;
            ViewBag.DateWhereStart = dateStart;
            ViewBag.DateWhereFinal = dateFinal;
        }

        #endregion
    }
}

