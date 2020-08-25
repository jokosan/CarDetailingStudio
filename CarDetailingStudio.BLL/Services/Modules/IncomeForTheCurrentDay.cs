using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class IncomeForTheCurrentDay : IIncomeForTheCurrentDay
    {
        private ISalaryExpenses _salaryExpenses;
        private IOrderServicesCarWashServices _orderServicesCarWash;
        private IUtilityCosts _utilityCosts;
        private IOtherExpenses _otherExpenses;
        private ICostsCarWashAndDeteyling _costsCarWashAndDeteyling;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private IBonusToSalary _bonusToSalary;

        public IncomeForTheCurrentDay(ISalaryExpenses salaryExpenses, IOrderServicesCarWashServices orderServicesCarWash,
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

        #region сумма всех расходов
        public async Task<double> SumOfAllExpenses(DateTime? startDate)
        {
            var costCarWashToDetailings = await _costsCarWashAndDeteyling.Reports(startDate.Value);
            var utilityCost = await _utilityCosts.Reports(startDate.Value);
            var otherExpenses = await _otherExpenses.Reports(startDate.Value);

            return costCarWashToDetailings.Sum(s => s.amount).Value + utilityCost.Sum(s => s.amount).Value + otherExpenses.Sum(s => s.amount).Value;
        }

        public async Task<double> SumOfAllExpenses(DateTime? startDate, DateTime? finalDate)
        {
            var costCarWashToDetailings = await _costsCarWashAndDeteyling.Reports(startDate.Value, finalDate.Value);
            var utilityCost = await _utilityCosts.Reports(startDate.Value, finalDate.Value);
            var otherExpenses = await _otherExpenses.Reports(startDate.Value, finalDate.Value);

            return costCarWashToDetailings.Sum(s => s.amount).Value + utilityCost.Sum(s => s.amount).Value + otherExpenses.Sum(s => s.amount).Value;
        }
        #endregion

        #region Заработная плата
        public async Task<EmployeeSalariesBll> EmployeeSalaries(DateTime? startDate)
        {
            var orderCarWashWorker = await _orderCarWashWorkers.Reports(startDate.Value);
            return AccruedSalaries(orderCarWashWorker);
        }

        public async Task<EmployeeSalariesBll> EmployeeSalaries(DateTime? startDate, DateTime? finalDate)
        {
            var orderCarWashWorker = await _orderCarWashWorkers.Reports(startDate.Value, finalDate.Value);
            return AccruedSalaries(orderCarWashWorker);
        }

        private EmployeeSalariesBll AccruedSalaries(IEnumerable<OrderCarWashWorkersBll> orderCarWashWorker)
        {
            var administratorCarWash = orderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition == 1) && (x.OrderServicesCarWash.typeOfOrder == 1));
            var administratorDetailings = orderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition == 2) && (x.OrderServicesCarWash.typeOfOrder == 1));
            var staffCarWashWorker = orderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition >= 4) && (x.OrderServicesCarWash.typeOfOrder == 1));
            var staffDetailing = orderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition == 3) && (x.OrderServicesCarWash.typeOfOrder == 1));

            var administratorCarpetWashing = orderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition <= 2) && (x.OrderServicesCarWash.typeOfOrder == 3));
            var staffCarpetWashing = orderCarWashWorker.Where(x => (x.CarWashWorkers.IdPosition >= 3) && (x.OrderServicesCarWash.typeOfOrder == 3));

            EmployeeSalariesBll employee = new EmployeeSalariesBll();

            employee.AdministratorCarWash = administratorCarWash.Sum(x => x.Payroll);         // ЗП админ мойки
            employee.AdministratorDetailings = administratorDetailings.Sum(x => x.Payroll);   // ЗП админ детейлинг
            employee.StaffCarWashWorker = staffCarWashWorker.Sum(x => x.Payroll);             // ЗП мойщик
            employee.StaffDetailing = staffDetailing.Sum(x => x.Payroll);                     // ЗП детейлинг
            employee.adminCarpet = administratorCarpetWashing.Sum(x => x.Payroll);            // ЗП админ ковры
            employee.staffCarpet = staffCarpetWashing.Sum(x => x.Payroll);                    // ЗП сотрудников ковры

            return employee;
        }
        #endregion

        #region Сумма выполненных заказов
        public async Task<OrderInformationWashingDetailingBll> AmountOfCompletedOrders(DateTime? startDate)
        {
            var order = await _orderServicesCarWash.Reports(startDate.Value);
            return InfoOrder(order);
        }
        public async Task<OrderInformationWashingDetailingBll> AmountOfCompletedOrders(DateTime? startDate, DateTime? finalDate)
        {
            var order = await _orderServicesCarWash.Reports(startDate.Value, finalDate.Value);
            return InfoOrder(order);
        }

        public OrderInformationWashingDetailingBll InfoOrder(IEnumerable<OrderServicesCarWashBll> orderServices)
        {
            var carWash = orderServices.Where(x => x.ServisesCarWashOrder.Any(y => y.Detailings.IdTypeService == 2));
            var detailings = orderServices.Where(x => x.ServisesCarWashOrder.Any(y => y.Detailings.IdTypeService == 1));
            var carpetWashing = orderServices.Where(x => x.typeOfOrder == 3);

            var cashWhere = orderServices.Where(x => x.PaymentState == 1);
            var nonCashWhere = orderServices.Where(x => x.PaymentState == 2);

            List<double> sumTet = new List<double>();

            foreach (var item in carpetWashing)
            {
                if (item.typeOfOrder == 3)
                {
                    sumTet.Add(item.OrderCarWashWorkers.Sum(s => s.Payroll).Value);
                }
            }

            OrderInformationWashingDetailingBll orderInformation = new OrderInformationWashingDetailingBll();

            orderInformation.OrderCarWashSum = carWash.Sum(x => x.DiscountPrice);        // Касса мойки
            orderInformation.OrderDetailing = detailings.Sum(x => x.DiscountPrice);      // Касса детейлинг
            orderInformation.carpetOrder = carpetWashing.Sum(x => x.DiscountPrice);      // Касса ковров
            orderInformation.cash = cashWhere.Sum(s => s.DiscountPrice);                 // Наличный расчет
            orderInformation.nonCash = nonCashWhere.Sum(s => s.DiscountPrice);           // Безналичный расчет

            orderInformation.OrderCount = carWash.Count();
            orderInformation.CarCountDetailings = detailings.Count();
            orderInformation.CauntCarpet = carpetWashing.Count();

            return orderInformation;           
        }
        #endregion

       


    }
}
