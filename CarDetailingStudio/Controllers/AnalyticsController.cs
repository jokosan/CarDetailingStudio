﻿using AutoMapper;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractFactory;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.AnalyticsView;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
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
            var analyticsCash = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsForTheSelectedPeriodFormOfPayment(startDate.Value, finalDate.Value, 2));
            var analyticsNoCash = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsForTheSelectedPeriodFormOfPayment(startDate.Value, finalDate.Value, 1));
            var Cashier = Mapper.Map<IEnumerable<CashierView>>(await _cashier.Reports(startDate.Value, finalDate.Value));

            AnalyticsFull analyticsFull = new AnalyticsFull();

            analyticsFull.analyticsViewResult = new AnalyticsView();
            analyticsFull.analyticsViewResult = analyticsResult;

            analyticsFull.analyticsViewCash = new AnalyticsView();
            analyticsFull.analyticsViewCash = analyticsCash;

            analyticsFull.analyticsViewNoCash = new AnalyticsView();
            analyticsFull.analyticsViewNoCash = analyticsNoCash;

            analyticsFull.cashStartOfTheDay = Cashier.Sum(x => x.amountBeginningOfTheDay);

            analyticsFull.CashEndDay = SumTotalGeneral(analyticsResult);
            analyticsFull.CashEndDayCash = SumTotal(analyticsCash) + analyticsFull.cashStartOfTheDay;
            analyticsFull.CashEndDayNoCash = SumTotal(analyticsNoCash);

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

            return View(analyticsFull);
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
            var analyticsResult = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsForTheDay(DateTime.Now));
            var analyticsCash = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsPerDayFormOfPayment(DateTime.Now, 2));
            var analyticsNoCash = Mapper.Map<AnalyticsView>(await _abstractFactory.AnalyticsPerDayFormOfPayment(DateTime.Now, 1));
            var Cashier = Mapper.Map<IEnumerable<CashierView>>(await _cashier.Reports(DateTime.Now));

            AnalyticsFull analyticsFull = new AnalyticsFull();

            analyticsFull.analyticsViewResult = new AnalyticsView();
            analyticsFull.analyticsViewResult = analyticsResult;

            analyticsFull.analyticsViewCash = new AnalyticsView();
            analyticsFull.analyticsViewCash = analyticsCash;

            analyticsFull.analyticsViewNoCash = new AnalyticsView();
            analyticsFull.analyticsViewNoCash = analyticsNoCash;

            analyticsFull.cashStartOfTheDay = Cashier.Sum(x => x.amountBeginningOfTheDay);

            analyticsFull.SumWegesAdministrator = analyticsResult.wagesForCompletedOrders.CarpetWashing.SalaryAdministrator + analyticsResult.wagesForCompletedOrders.Washing.SalaryAdministrator;
            analyticsFull.SumWegesEmployees = analyticsResult.wagesForCompletedOrders.CarpetWashing.SalaryEmployees + analyticsResult.wagesForCompletedOrders.Washing.SalaryEmployees;
            analyticsFull.SumPendingPayment = SumOrderPendingPayment(analyticsResult);


            analyticsFull.CashEndDay = SumTotalGeneral(analyticsResult);
            analyticsFull.CashEndDayCash = SumTotal(analyticsCash) + analyticsFull.cashStartOfTheDay + SumOrdersForThePreviousPeriod(analyticsCash.informationPreviousPeriod);
            analyticsFull.CashEndDayNoCash = SumTotal(analyticsNoCash) + SumOrdersForThePreviousPeriod(analyticsNoCash.informationPreviousPeriod);

            ViewBag.StartDate = DateTime.Now.ToString("D");
            ViewBag.DateWhereStart = DateTime.Now;
            ViewBag.CloseDay = CloseDay;
            ViewBag.Message = message;
            ViewBag.Date = DateTime.Now.AddDays(1).ToString("d");


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

        private double SumOrderPendingPayment(AnalyticsView analyticsViews)
        {
            double washing = analyticsViews.completedOrders.Washing.OrdersAwaitingPaymentCashier;
            double detailing = analyticsViews.completedOrders.Detailing.OrdersAwaitingPaymentCashier;
            double tireFitting = analyticsViews.completedOrders.TireFitting.OrdersAwaitingPaymentCashier;
            double carpetWashing = analyticsViews.completedOrders.CarpetWashing.OrdersAwaitingPaymentCashier;
            double tireStorage = analyticsViews.completedOrders.TireStorage.OrdersAwaitingPaymentCashier;

            return washing + detailing + tireFitting + tireStorage + carpetWashing;
        }

        private double SumTotal(AnalyticsView analyticsView)
        {
            return analyticsView.completedOrders.Washing.Cashier + analyticsView.completedOrders.Detailing.Cashier + analyticsView.completedOrders.CarpetWashing.Cashier +
                   analyticsView.completedOrders.TireFitting.Cashier + analyticsView.completedOrders.TireStorage.Cashier + analyticsView.saleOfGoods.SumGoodsSold +
                   analyticsView.additionalIncome.CashierAvtomir + analyticsView.additionalIncome.CashierCaravan + analyticsView.additionalIncome.CashierDryCleaningKohler -
            //analyticsView.expenses.AmountExpenses;
            analyticsView.expensesClassModels.Sum(s => s.Amount);
        }

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

            // var expenses = analyticsView.expenses.AmountExpenses - analyticsView.expenses.PayrollCosts;
            var expenses = analyticsView.expensesClassModels.Where(x => x.expenseCategoryId != 1).Sum(s => s.Amount);

            var resultServAndWages = services - wages;

            return (resultServAndWages + analyticsView.saleOfGoods.SumGoodsSold + analyticsView.additionalIncome.CashierAvtomir +
                   analyticsView.additionalIncome.CashierCaravan + analyticsView.additionalIncome.CashierDryCleaningKohler) - expenses;
        }

        private double SumOrdersForThePreviousPeriod(InformationPreviousPeriod informationSum)
        {
            return informationSum.CarpetWashing.OrderSum + informationSum.Washing.OrderSum + informationSum.Detailing.OrderSum +
                    informationSum.TireFitting.OrderSum + informationSum.TireStorage.OrderSum;
        }

        public async Task<ActionResult> DetailsAboutOrders(int typeOrder, int paymentState, DateTime start, DateTime? finlDate, int type = 0, int statusOrder = 0)
        {
            if (finlDate == null)
            {
                if (paymentState == 0)
                {
                    if (statusOrder != 0)
                    {
                        ViewBagDateGroup(start, finlDate);
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, start, statusOrder)));
                    }

                    if (type == 0)
                    {
                        ViewBagDateGroup(start, finlDate);
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, start)));
                    }
                    else
                    {
                        ViewBagDateGroup(start, finlDate);
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.InformationAboutOrderForThePreviousPeriod(typeOrder, start)));
                    }

                }
                else
                {
                    if (type == 0)
                    {
                        ViewBagDateGroup(start, finlDate);
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, paymentState, start)));
                    }
                    else
                    {
                        ViewBagDateGroup(start, finlDate);
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
                        ViewBagDateGroup(start, finlDate);
                        return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, start, finlDate, statusOrder)));
                    }

                    ViewBagDateGroup(start, finlDate);
                    return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, start, finlDate)));
                }
                else
                {
                    ViewBagDateGroup(start, finlDate);
                    return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _abstractFactory.DetailsAboutOrders(typeOrder, paymentState, start, finlDate)));
                }
            }
        }

        public async Task<ActionResult> DetailsExpenses(int paymentState, DateTime dateStart, DateTime? dateFinal, int typeExpenses = 0)
        {
            ViewBag.TypeExpenses = typeExpenses;

            if (dateFinal == null)
            {
                if (typeExpenses == 1)
                {
                    var salaryExpenses = Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>( await _abstractFactory.DetailsSalaryExpenses(1, dateStart));
                  
                    ViewBagDateGroup(dateStart, dateFinal);

                    return View(salaryExpenses);
                }

                if (paymentState != 0)
                {
                    var expensesPaymentStateResult = Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsExpenses(typeExpenses, paymentState, dateStart));
                 
                    ViewBagDateGroup(dateStart, dateFinal);

                    return View(expensesPaymentStateResult);
                }

                var expensesResult = Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsExpenses(typeExpenses, dateStart));
        
                ViewBagDateGroup(dateStart, dateFinal);

                return View(expensesResult);
            }
            else
            {
                if (paymentState != 0)
                {
                    var expensesPaymentStateResult = Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsExpenses(typeExpenses, paymentState, dateStart, dateFinal));
                 
                    ViewBagDateGroup(dateStart, dateFinal);

                    return View(expensesPaymentStateResult);
                }

                var expensesResult = Mapper.Map<IEnumerable<Models.ModelViews.ExpensesView>>(await _abstractFactory.DetailsExpenses(typeExpenses, dateStart, dateFinal));
             
                ViewBagDateGroup(dateStart, dateFinal);

                return View(expensesResult);
            }
        }

        public async Task<ActionResult> ExpensesGroup(int paymentState, DateTime dateStart, DateTime? dateFinal, int typeExpenses = 0)
        {
            ViewBag.PaymentState = paymentState;
            ViewBag.DateStart = dateStart;
            ViewBag.DateFinal = dateFinal;

            if (dateFinal == null)
            {
                if (paymentState != 0)
                {
                    var expensesPaymentStateResult = Mapper.Map<IEnumerable<ExpensesClassView>>(await _abstractFactory.ExpensesGroup(paymentState, dateStart, typeExpenses));
                 
                    ViewBagDateGroup(dateStart, dateFinal);

                    return View(expensesPaymentStateResult);
                }

                var expensesResult = Mapper.Map<IEnumerable<ExpensesClassView>>(await _abstractFactory.ExpensesGroup(dateStart, typeExpenses));
           
                ViewBagDateGroup(dateStart, dateFinal);

                return View(expensesResult);
            }
            else
            {
                if (paymentState != 0)
                {
                    var expensesPaymentStateResult = Mapper.Map<IEnumerable<ExpensesClassView>>(await _abstractFactory.ExpensesGroup(paymentState, dateStart, dateFinal, typeExpenses));
                  
                    ViewBagDateGroup(dateStart, dateFinal);

                    return View(expensesPaymentStateResult);
                }

                var expensesResult = Mapper.Map<IEnumerable<ExpensesClassView>>(await _abstractFactory.ExpensesGroup(dateStart, dateFinal, typeExpenses));
               
                ViewBagDateGroup(dateStart, dateFinal);

                return View(expensesResult);
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

            if (dateFinal == null)
            {
                var result = Mapper.Map<IEnumerable<GroupingEmployeesWagesView>>(await _abstractFactory.GroupDetailsWages(typeExpenses.Value, dateStart.Value));
              
                ViewBagDateGroup(dateStart.Value, dateFinal);
                return View(result);
            }
            else
            {
                var result = Mapper.Map<IEnumerable<GroupingEmployeesWagesView>>(await _abstractFactory.GroupDetailsWages(typeExpenses.Value, dateStart.Value, dateFinal));
             
                ViewBagDateGroup(dateStart.Value, dateFinal);
                return View(result);
            }
        }

        public async Task<ActionResult> GoodsSoldInfo(int paymentState, DateTime dateStart, DateTime? dateFinal)
        {
            if (dateFinal == null)
            {
                if (paymentState != 0)
                {
                    var goodsSoldInforesult = Mapper.Map<IEnumerable<GoodsSoldView>>(await _abstractFactory.DetailsGoodsSold(paymentState, dateStart));
                    ViewBagDateGroup(dateStart, dateFinal);

                    return View(goodsSoldInforesult);
                }

                var goodsSoldInfo = Mapper.Map<IEnumerable<GoodsSoldView>>(await _abstractFactory.DetailsGoodsSold(dateStart));
                ViewBagDateGroup(dateStart, dateFinal);

                return View();
            }
            else
            {
                if (paymentState != 0)
                {
                    var goodsSoldInforesult = Mapper.Map<IEnumerable<GoodsSoldView>>(await _abstractFactory.DetailsGoodsSold(paymentState, dateStart, dateFinal));
                    ViewBagDateGroup(dateStart, dateFinal);

                    return View(goodsSoldInforesult);
                }

                var goodsSoldInfo = Mapper.Map<IEnumerable<GoodsSoldView>>(await _abstractFactory.DetailsGoodsSold(dateStart, dateFinal));
                ViewBagDateGroup(dateStart, dateFinal);

                return View(goodsSoldInfo);
            }
        }

        public async Task<ActionResult> AdditionalIncomeInfo(string category, int paymentState, DateTime dateStart, DateTime? dateFinal)
        {
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

            if (dateFinal == null)
            {
                var result = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _abstractFactory.DetailsWages(typeExpenses.Value, idEmployees.Value, dateStart.Value));
              //  ViewBagDateWagesInfoGroup(result);
                ViewBagDateGroup(dateStart.Value, dateFinal);

                return View(result);
            }
            else
            {
                var result = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _abstractFactory.DetailsWages(typeExpenses.Value, idEmployees.Value, dateStart.Value, dateFinal));
               // ViewBagDateWagesInfoGroup(result);
                ViewBagDateGroup(dateStart.Value, dateFinal);
                return View(result);
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
        }

        #endregion
    }
}

