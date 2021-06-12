using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.AnalyticsModules.Models.WagesForCompletedOrders;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractFactory
{
    public interface IAbstractFactory
    {
        #region  AnalyticsModels
        Task<AnalyticsModels> AnalyticsForTheDay(DateTime date); // аналитика за день
        Task<AnalyticsModels> AnalyticsPerDayFormOfPayment(DateTime date, int paymentState); // аналитика за день наличные/безналичный расчет
        Task<AnalyticsModels> AnalyticsForTheSelectedPeriod(DateTime dateStart, DateTime dateFinl); // аналитика за выбранный период
        Task<AnalyticsModels> AnalyticsForTheSelectedPeriodFormOfPayment(DateTime dateStart, DateTime dateFinal, int paymentState); // аналитика за выбранный период наличные/ безналичный расчет
        #endregion

        #region OrderServicesCarWash start date 
        Task<IEnumerable<OrderServicesCarWashBll>> DetailsAboutOrders(int typeOrder, DateTime date, int statusOrder = 0);
        Task<IEnumerable<OrderServicesCarWashBll>> DetailsAboutOrders(int typeOrder, int paymentState, DateTime date);
        Task<IEnumerable<OrderServicesCarWashBll>> DetailsAboutOrders(int typeOrder, DateTime start, DateTime? finlDate, int statusOrder = 0);
        Task<IEnumerable<OrderServicesCarWashBll>> DetailsAboutOrders(int typeOrder, int paymentState, DateTime start, DateTime? finlDate);
        #endregion

        #region OrderServicesCarWash final date 
        Task<IEnumerable<OrderServicesCarWashBll>> InformationAboutOrderForThePreviousPeriod(int typeOrder, DateTime date);
        Task<IEnumerable<OrderServicesCarWashBll>> InformationAboutOrderForThePreviousPeriod(int typeOrder, int paymentState, DateTime date);
        #endregion

        #region Expenses
        Task<IEnumerable<ExpensesBll>> DetailsExpenses(int typeExpenses, DateTime date);
        Task<IEnumerable<ExpensesBll>> DetailsExpenses(int typeExpenses, int paymentState, DateTime date);
        Task<IEnumerable<ExpensesBll>> DetailsExpenses(int typeExpenses, DateTime start, DateTime? finlDate);
        Task<IEnumerable<ExpensesBll>> DetailsExpenses(int typeExpenses, int paymentState, DateTime start, DateTime? finlDate);

        Task<IEnumerable<ExpensesBll>> DetailsSalaryExpenses(int type, DateTime date);

        Task<IEnumerable<ExpensesClassModels>> ExpensesGroup(int paymentState, DateTime start, DateTime? finlDate, int typeExpenses = 0);
        Task<IEnumerable<ExpensesClassModels>> ExpensesGroup(DateTime start, DateTime? finlDate, int typeExpenses = 0);
        Task<IEnumerable<ExpensesClassModels>> ExpensesGroup(int paymentState, DateTime date, int typeExpenses = 0);
        Task<IEnumerable<ExpensesClassModels>> ExpensesGroup(DateTime date, int typeExpenses = 0);

        #endregion

        #region  Wages
      
        Task<IEnumerable<OrderCarWashWorkersBll>> DetailsWages(int typeServices, DateTime date);
        Task<IEnumerable<OrderCarWashWorkersBll>> DetailsWages(int typeServices, DateTime start, DateTime? finlDate);
        Task<IEnumerable<OrderCarWashWorkersBll>> DetailsWages(int typeServices, int idEmployees, DateTime date);
        Task<IEnumerable<OrderCarWashWorkersBll>> DetailsWages(int typeServices, int idEmployees, DateTime start, DateTime? finlDate);
        Task<IEnumerable<GroupingEmployeesWages>> GroupDetailsWages(DateTime date);
        Task<IEnumerable<GroupingEmployeesWages>> GroupDetailsWages(DateTime start, DateTime? finlDate);
        Task<AnalyticsModels> InformationOnAllWages(int idEmployees, DateTime date);
        Task<AnalyticsModels> InformationOnAllWages(int idEmployees, DateTime start, DateTime? finlDate);
        Task<IEnumerable<GroupingEmployeesWages>> GroupDetailsWages(int typeServices, DateTime date);
        Task<IEnumerable<GroupingEmployeesWages>> GroupDetailsWages(int typeServices, DateTime start, DateTime? finlDate);
        Task<IEnumerable<GroupingEmployeesWages>> GroupingEmployeesByPeriod(DateTime start, DateTime? finlDate);
        #endregion

        #region GoodsSold
        Task<IEnumerable<GoodsSoldBll>> DetailsGoodsSold(DateTime date);
        Task<IEnumerable<GoodsSoldBll>> DetailsGoodsSold(int paymentState, DateTime date);
        Task<IEnumerable<GoodsSoldBll>> DetailsGoodsSold(DateTime start, DateTime? finlDate);
        Task<IEnumerable<GoodsSoldBll>> DetailsGoodsSold(int paymentState, DateTime start, DateTime? finlDate);
        #endregion

        #region AdditionalIncome
        Task<IEnumerable<AdditionalIncomeBll>> DetailsAdditionalIncome(string IncomeCategory, DateTime date);
        Task<IEnumerable<AdditionalIncomeBll>> DetailsAdditionalIncome(string IncomeCategory, int paymentState, DateTime date);
        Task<IEnumerable<AdditionalIncomeBll>> DetailsAdditionalIncome(string IncomeCategory, DateTime start, DateTime? finlDate);
        Task<IEnumerable<AdditionalIncomeBll>> DetailsAdditionalIncome(string IncomeCategory, int paymentState, DateTime start, DateTime? finlDate);
        #endregion

        Task<AnalyticsIncomeModel> AnalyticsFinance(DateTime date);
        Task<AnalyticsIncomeModel> AnalyticsFinance(DateTime start, DateTime? finlDate);

        Task<IEnumerable<OrderServicesCarWashBll>> PaidServiceDebt(int typeServices, DateTime date);
        Task<IEnumerable<OrderServicesCarWashBll>> PaidServiceDebt(int typeServices, DateTime start, DateTime? finlDate);
    }
}
