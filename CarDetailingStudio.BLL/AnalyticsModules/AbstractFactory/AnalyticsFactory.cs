using CarDetailingStudio.BLL.AnalyticsModules.AbstractCompletedOrders;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractExpenses;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractPaymentForThePreviousPeriod;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractSalaryExpenses;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractSaleOfGoods;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractWagesForCompletedOrders;
using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractFactory
{
    partial class AnalyticsFactory : IAbstractFactory
    {
        private readonly IAbstractExpenses _expenses;
        private readonly ICompletedOrders _completedOrders;
        private readonly IWagesForCompletedOrders _wagesForCompleted;
        private readonly ISaleOfGoods _saleOfGoods;
        private readonly IAdditionalIncome _additionalIncome;
        private readonly IPaymentForThePreviousPeriod _paymentForThePrevious;
        private readonly IAbstractSalaryExpenses _abstractSalaryExpenses;

        private AnalyticsModels analytics = new AnalyticsModels();

        public AnalyticsFactory(
            IAbstractExpenses expenses,
            ICompletedOrders completedOrders,
            IWagesForCompletedOrders wagesForCompleted,
            ISaleOfGoods saleOfGoods,
            IAdditionalIncome additionalIncome,
            IPaymentForThePreviousPeriod paymentForThePrevious,
            IAbstractSalaryExpenses abstractSalaryExpenses)
        {
            _expenses = expenses;
            _completedOrders = completedOrders;
            _wagesForCompleted = wagesForCompleted;
            _saleOfGoods = saleOfGoods;
            _additionalIncome = additionalIncome;
            _paymentForThePrevious = paymentForThePrevious;
            _abstractSalaryExpenses = abstractSalaryExpenses;
        }

        #region AnalyticsModels
        public async Task<AnalyticsModels> AnalyticsForTheDay(DateTime date)
        {
            analytics.completedOrders = new Models.CompletedOrders.CompletedOrdersModels();
            analytics.completedOrders = _completedOrders.AnalyticsFormationCompletedOrders(await _completedOrders.CompletedOrdersPerDay(date));

            analytics.expensesClassModels = new List<ExpensesClassModels>();
            analytics.expensesClassModels = _expenses.GroupExpenseCategory(await _expenses.ExpensesPerDay(date)).ToList();

            analytics.wagesForCompletedOrders = new Models.WagesForCompletedOrders.WagesForCompletedOrdersModels();
            analytics.wagesForCompletedOrders = await _wagesForCompleted.WagesForCompletedOrdersPerDay(date);

            analytics.saleOfGoods = new SaleOfGoodsModels();
            analytics.saleOfGoods = _saleOfGoods.AnalyticsFormationSaleOfGoods(await _saleOfGoods.SaleOfGoodsPerDay(date));

            analytics.additionalIncome = new AdditionalIncomeModels();
            analytics.additionalIncome = _additionalIncome.AnalyticsFormationAdditionalIncome(await _additionalIncome.SaleOfGoodsPerDay(date));

            analytics.informationPreviousPeriod = new InformationPreviousPeriod();
            analytics.informationPreviousPeriod = _paymentForThePrevious.AnalyticsFormationPaymentForThePreviousPeriod(await _paymentForThePrevious.PaymentForThePreviousPeriodPerDay(date));

            return analytics;
        }

        public async Task<AnalyticsModels> AnalyticsPerDayFormOfPayment(DateTime date, int paymentState)
        {
            analytics.completedOrders = new Models.CompletedOrders.CompletedOrdersModels();
            analytics.completedOrders = _completedOrders.AnalyticsFormationCompletedOrders(await _completedOrders.CompletedOrdersPerDay(date, paymentState));

            analytics.expensesClassModels = new List<ExpensesClassModels>();
            analytics.expensesClassModels = _expenses.GroupExpenseCategory(await _expenses.ExpensesPerDay(date, paymentState)).ToList();

            analytics.wagesForCompletedOrders = new Models.WagesForCompletedOrders.WagesForCompletedOrdersModels();
            analytics.wagesForCompletedOrders = await _wagesForCompleted.WagesForCompletedOrdersPerDay(date);

            analytics.saleOfGoods = new SaleOfGoodsModels();
            analytics.saleOfGoods = _saleOfGoods.AnalyticsFormationSaleOfGoods(await _saleOfGoods.SaleOfGoodsPerDay(date, paymentState));

            analytics.additionalIncome = new AdditionalIncomeModels();
            analytics.additionalIncome = _additionalIncome.AnalyticsFormationAdditionalIncome(await _additionalIncome.SaleOfGoodsPerDay(date, paymentState));

            analytics.informationPreviousPeriod = new InformationPreviousPeriod();
            analytics.informationPreviousPeriod = _paymentForThePrevious.AnalyticsFormationPaymentForThePreviousPeriod(await _paymentForThePrevious.PaymentForThePreviousPeriodPerDay(date, paymentState));

            return analytics;
        }

        public async Task<AnalyticsModels> AnalyticsForTheSelectedPeriod(DateTime dateStart, DateTime dateFinal)
        {
            analytics.completedOrders = new Models.CompletedOrders.CompletedOrdersModels();
            analytics.completedOrders = _completedOrders.AnalyticsFormationCompletedOrders(await _completedOrders.CompletedOrdersForTheSelectedPeriod(dateStart, dateFinal));

            analytics.expensesClassModels = new List<ExpensesClassModels>();
            analytics.expensesClassModels = _expenses.GroupExpenseCategory(await _expenses.ExpensesForTheSelectedPeriod(dateStart, dateFinal)).ToList();

            analytics.wagesForCompletedOrders = new Models.WagesForCompletedOrders.WagesForCompletedOrdersModels();
            analytics.wagesForCompletedOrders = await _wagesForCompleted.WagesForCompletedOrdersForTheSelectedPeriod(dateStart, dateFinal);

            analytics.saleOfGoods = new SaleOfGoodsModels();
            analytics.saleOfGoods = _saleOfGoods.AnalyticsFormationSaleOfGoods(await _saleOfGoods.SaleOfGoodsForTheSelectedPeriod(dateStart, dateFinal));

            analytics.additionalIncome = new AdditionalIncomeModels();
            analytics.additionalIncome = _additionalIncome.AnalyticsFormationAdditionalIncome(await _additionalIncome.SaleOfGoodsForTheSelectedPeriod(dateStart, dateFinal));

            analytics.informationPreviousPeriod = new InformationPreviousPeriod();

            return analytics;
        }

        public async Task<AnalyticsModels> AnalyticsForTheSelectedPeriodFormOfPayment(DateTime dateStart, DateTime dateFinal, int paymentState)
        {
            analytics.completedOrders = new Models.CompletedOrders.CompletedOrdersModels();
            analytics.completedOrders = _completedOrders.AnalyticsFormationCompletedOrders(await _completedOrders.CompletedOrdersForTheSelectedPeriod(dateStart, dateFinal, paymentState));

            analytics.expensesClassModels = new List<ExpensesClassModels>();
            analytics.expensesClassModels = _expenses.GroupExpenseCategory(await _expenses.ExpensesForTheSelectedPeriod(dateStart, dateFinal, paymentState)).ToList();

            analytics.wagesForCompletedOrders = new Models.WagesForCompletedOrders.WagesForCompletedOrdersModels();
            analytics.wagesForCompletedOrders = await _wagesForCompleted.WagesForCompletedOrdersForTheSelectedPeriod(dateStart, dateFinal);

            analytics.saleOfGoods = new SaleOfGoodsModels();
            analytics.saleOfGoods = _saleOfGoods.AnalyticsFormationSaleOfGoods(await _saleOfGoods.SaleOfGoodsForTheSelectedPeriod(dateStart, dateFinal, paymentState));

            analytics.additionalIncome = new AdditionalIncomeModels();
            analytics.additionalIncome = _additionalIncome.AnalyticsFormationAdditionalIncome(await _additionalIncome.SaleOfGoodsForTheSelectedPeriod(dateStart, dateFinal, paymentState));

            analytics.informationPreviousPeriod = new InformationPreviousPeriod();

            return analytics;
        }
        #endregion

        #region OrderServicesCarWashBll
        public async Task<IEnumerable<OrderServicesCarWashBll>> DetailsAboutOrders(int typeOrder, DateTime date, int statusOrder = 0)
        {
            var result = await _completedOrders.CompletedOrdersPerDay(date);

            if (statusOrder != 0)
            {
                return result.Where(x => (x.typeOfOrder == typeOrder)
                                          && x.StatusOrder == statusOrder);
            }

            return result.Where(x => x.typeOfOrder == typeOrder);
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> DetailsAboutOrders(int typeOrder, int paymentState, DateTime date)
        {
            var result = await _completedOrders.CompletedOrdersPerDay(date, paymentState);
            return result.Where(x => x.typeOfOrder == typeOrder);
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> DetailsAboutOrders(int typeOrder, DateTime start, DateTime? finlDate, int statusOrder = 0)
        {
            var result = await _completedOrders.CompletedOrdersForTheSelectedPeriod(start, finlDate.Value);

            if (statusOrder != 0)
            {
                return result.Where(x => (x.typeOfOrder == typeOrder)
                                          && x.StatusOrder == statusOrder);
            }

            return result.Where(x => x.typeOfOrder == typeOrder);
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> DetailsAboutOrders(int typeOrder, int paymentState, DateTime start, DateTime? finlDate)
        {
            var result = await _completedOrders.CompletedOrdersForTheSelectedPeriod(start, finlDate.Value, paymentState);
            return result.Where(x => x.typeOfOrder == typeOrder);
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> InformationAboutOrderForThePreviousPeriod(int typeOrder, DateTime date)
        {
            var result = _paymentForThePrevious.InfoOrdersForThePreviousPeriod(await _paymentForThePrevious.PaymentForThePreviousPeriodPerDay(date));
            return result.Where(x => x.typeOfOrder == typeOrder);
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> InformationAboutOrderForThePreviousPeriod(int typeOrder, int paymentState, DateTime date)
        {
            var result = _paymentForThePrevious.InfoOrdersForThePreviousPeriod(await _paymentForThePrevious.PaymentForThePreviousPeriodPerDay(date, paymentState));
            return result.Where(x => x.typeOfOrder == typeOrder);
        }
        #endregion

        #region Expenses

        #region ExpensesGroup
        public async Task<IEnumerable<ExpensesClassModels>> ExpensesGroup(int paymentState, DateTime start, DateTime? finlDate, int typeExpenses = 0)
        {
            var resultExpenses = await _expenses.ExpensesForTheSelectedPeriod(start, finlDate.Value, paymentState);

            if (typeExpenses == 0)
                return _expenses.GroupCostCategories(resultExpenses);

            return _expenses.GroupCostCategories(resultExpenses.Where(x => x.expenseCategoryId == typeExpenses));
        }

        public async Task<IEnumerable<ExpensesClassModels>> ExpensesGroup(DateTime start, DateTime? finlDate, int typeExpenses = 0)
        {
            var resultExpenses = await _expenses.ExpensesForTheSelectedPeriod(start, finlDate.Value);

            if (typeExpenses == 0)
                return _expenses.GroupCostCategories(resultExpenses);

            return _expenses.GroupCostCategories(resultExpenses.Where(x => x.expenseCategoryId == typeExpenses));
        }

        public async Task<IEnumerable<ExpensesClassModels>> ExpensesGroup(int paymentState, DateTime date, int typeExpenses = 0)
        {
            var resultExpenses = await _expenses.ExpensesPerDay(date, paymentState);

            if (typeExpenses == 0)
                return _expenses.GroupCostCategories(resultExpenses);

            return _expenses.GroupCostCategories(resultExpenses.Where(x => x.expenseCategoryId == typeExpenses));
        }

        public async Task<IEnumerable<ExpensesClassModels>> ExpensesGroup(DateTime date, int typeExpenses = 0)
        {
            var resultExpenses = await _expenses.ExpensesPerDay(date);

            if (typeExpenses == 0)
                return _expenses.GroupCostCategories(resultExpenses);

            return _expenses.GroupCostCategories(resultExpenses.Where(x => x.expenseCategoryId == typeExpenses));
        }
        #endregion

        #region SalaryExpenses
        public async Task<IEnumerable<ExpensesBll>> DetailsSalaryExpenses(int type, DateTime date) =>
            _abstractSalaryExpenses.ExpenseCategoryWages(await _abstractSalaryExpenses.SalaryExpensesInfo(date, type));

        #endregion

        #region DetailsExpenses

        public async Task<IEnumerable<ExpensesBll>> DetailsExpenses(int typeExpenses, DateTime date)
        {
            var resultExpenses = await _expenses.ExpensesPerDay(date);

            if (typeExpenses == 0)
                return resultExpenses;

            return resultExpenses.Where(x => x.expenseCategoryId == typeExpenses);
        }

        public async Task<IEnumerable<ExpensesBll>> DetailsExpenses(int typeExpenses, int paymentState, DateTime date)
        {
            var result = await _expenses.ExpensesPerDay(date, paymentState);

            if (typeExpenses == 0)
                return result;

            return result.Where(x => x.expenseCategoryId == typeExpenses);
        }

        public async Task<IEnumerable<ExpensesBll>> DetailsExpenses(int typeExpenses, DateTime start, DateTime? finlDate)
        {
            var result = await _expenses.ExpensesForTheSelectedPeriod(start, finlDate.Value);

            if (typeExpenses == 0)
                return result;

            return result.Where(x => x.expenseCategoryId == typeExpenses);
        }

        public async Task<IEnumerable<ExpensesBll>> DetailsExpenses(int typeExpenses, int paymentState, DateTime start, DateTime? finlDate)
        {
            var result = await _expenses.ExpensesForTheSelectedPeriod(start, finlDate.Value, paymentState);

            if (typeExpenses == 0)
                return result;

            return result.Where(x => x.expenseCategoryId == typeExpenses);
        }

        #endregion
        #endregion


        #region Wages

        public async Task<IEnumerable<OrderCarWashWorkersBll>> DetailsWages(int typeServices, DateTime date)
        {
            var resultExpenses = await _wagesForCompleted.AnalyticsWages(date);
            return resultExpenses.Where(x => x.typeServicesId == typeServices);
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> DetailsWages(int typeServices, DateTime start, DateTime? finlDate)
        {
            var result = await _wagesForCompleted.AnalyticsWages(start, finlDate.Value);
            return result.Where(x => x.typeServicesId == typeServices);
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> DetailsWages(int typeServices, int idEmployees, DateTime date)
        {
            var result = await _wagesForCompleted.AnalyticsWages(date);
            return _wagesForCompleted.EarningsPerEmployee(typeServices, idEmployees, result);
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> DetailsWages(int typeServices, int idEmployees, DateTime start, DateTime? finlDate)
        {
            var result = await _wagesForCompleted.AnalyticsWages(start, finlDate.Value);
            return _wagesForCompleted.EarningsPerEmployee(typeServices, idEmployees, result);
        }

        public async Task<IEnumerable<GroupingEmployeesWages>> GroupDetailsWages(int typeServices, DateTime date)
        {
            var resultExpenses = await _wagesForCompleted.AnalyticsWages(date);
            return _wagesForCompleted.GroupingByDatesAndEmployees(resultExpenses.Where(x => x.typeServicesId == typeServices));
        }

        public async Task<IEnumerable<GroupingEmployeesWages>> GroupDetailsWages(int typeServices, DateTime start, DateTime? finlDate)
        {
            var result = await _wagesForCompleted.AnalyticsWages(start, finlDate.Value);
            return _wagesForCompleted.GroupingByDatesAndEmployees(result.Where(x => x.typeServicesId == typeServices)).AsEnumerable();
        }

        #endregion

        #region GoodsSold
        public async Task<IEnumerable<GoodsSoldBll>> DetailsGoodsSold(DateTime date) =>
            _saleOfGoods.GoodsSoldGroup(await _saleOfGoods.SaleOfGoodsPerDay(date));

        public async Task<IEnumerable<GoodsSoldBll>> DetailsGoodsSold(int paymentState, DateTime date) =>
            _saleOfGoods.GoodsSoldGroup(await _saleOfGoods.SaleOfGoodsPerDay(date, paymentState));

        public async Task<IEnumerable<GoodsSoldBll>> DetailsGoodsSold(DateTime start, DateTime? finlDate) =>
            _saleOfGoods.GoodsSoldGroup(await _saleOfGoods.SaleOfGoodsForTheSelectedPeriod(start, finlDate.Value));

        public async Task<IEnumerable<GoodsSoldBll>> DetailsGoodsSold(int paymentState, DateTime start, DateTime? finlDate) =>
            _saleOfGoods.GoodsSoldGroup(await _saleOfGoods.SaleOfGoodsForTheSelectedPeriod(start, finlDate.Value, paymentState));

        #endregion

        #region AdditionalIncome
        public async Task<IEnumerable<AdditionalIncomeBll>> DetailsAdditionalIncome(string IncomeCategory, DateTime date)
        {
            var result = await _additionalIncome.SaleOfGoodsPerDay(date);
            return result.Where(x => x.IncomeCategory == IncomeCategory);
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> DetailsAdditionalIncome(string IncomeCategory, int paymentState, DateTime date)
        {
            var result = await _additionalIncome.SaleOfGoodsPerDay(date, paymentState);
            return result.Where(x => x.IncomeCategory == IncomeCategory);
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> DetailsAdditionalIncome(string IncomeCategory, DateTime start, DateTime? finlDate)
        {
            var result = await _additionalIncome.SaleOfGoodsForTheSelectedPeriod(start, finlDate.Value);
            return result.Where(x => x.IncomeCategory == IncomeCategory);
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> DetailsAdditionalIncome(string IncomeCategory, int paymentState, DateTime start, DateTime? finlDate)
        {
            var result = await _additionalIncome.SaleOfGoodsForTheSelectedPeriod(start, finlDate.Value, paymentState);
            return result.Where(x => x.IncomeCategory == IncomeCategory);
        }
        #endregion
    }
}

