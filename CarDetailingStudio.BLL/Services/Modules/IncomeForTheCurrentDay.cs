using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.Contract;
using CarDetailingStudio.BLL.Services.Modules.ModulesModel;
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
        private IBrigadeForTodayServices _brigadeForToday;

        public IncomeForTheCurrentDay(ISalaryExpenses salaryExpenses, IOrderServicesCarWashServices orderServicesCarWash,
                                      IUtilityCosts utilityCosts, IOtherExpenses otherExpenses, ICostsCarWashAndDeteyling costsCarWashAndDeteyling,
                                      IOrderCarWashWorkersServices orderCarWashWorkers, IBonusToSalary bonusToSalary, IBrigadeForTodayServices brigadeForToday)
        {
            _salaryExpenses = salaryExpenses;
            _orderServicesCarWash = orderServicesCarWash;
            _utilityCosts = utilityCosts;
            _otherExpenses = otherExpenses;
            _costsCarWashAndDeteyling = costsCarWashAndDeteyling;
            _orderCarWashWorkers = orderCarWashWorkers;
            _bonusToSalary = bonusToSalary;
            _brigadeForToday = brigadeForToday;
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
            var brigade = await _brigadeForToday.Reports(startDate.Value);

            return AccruedSalaries(orderCarWashWorker, brigade);
        }

        public async Task<EmployeeSalariesBll> EmployeeSalaries(DateTime? startDate, DateTime? finalDate)
        {
            var orderCarWashWorker = await _orderCarWashWorkers.Reports(startDate.Value, finalDate.Value);
            var brigade = await _brigadeForToday.Reports(startDate.Value, finalDate.Value);

            return AccruedSalaries(orderCarWashWorker, brigade);
        }

        private EmployeeSalariesBll AccruedSalaries(IEnumerable<OrderCarWashWorkersBll> orderCarWashWorker, IEnumerable<BrigadeForTodayBll> brigades)
        {
            EmployeeSalariesBll employee = new EmployeeSalariesBll();

            var adminDetailing = orderCarWashWorker.Where(x => x.typeServicesId == 1); // админ детейлинг
            var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 2); // админ мойка
            var adminCarpetWashing = orderCarWashWorker.Where(x => x.typeServicesId == 3); // админ ковры
            var StaffDetailing = orderCarWashWorker.Where(x => x.typeServicesId == 4); // услуги детейлинга
            var staffCarWashWorker = orderCarWashWorker.Where(x => x.typeServicesId == 5); // услуги мойки
            var staffCarWashWorkerCarpetWashing = orderCarWashWorker.Where(x => x.typeServicesId == 6); // услуги по стирке ковров
                                                                                                        // var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 7); // хранение шин админ
                                                                                                        // var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 8); // хранение шин сортудники 

            employee.AdministratorDetailings = adminDetailing.Sum(x => x.Payroll);   // ЗП админ детейлинг            
            employee.StaffDetailing = StaffDetailing.Sum(x => x.Payroll);                     // ЗП детейлинг

            employee.AdministratorCarWash = adminCarWash.Sum(x => x.Payroll);                        // ЗП админ мойки
            employee.StaffCarWashWorker = staffCarWashWorker.Sum(x => x.Payroll);             // ЗП мойщик

            employee.adminCarpet = adminCarpetWashing.Sum(x => x.Payroll);                    // ЗП админ ковры
            employee.staffCarpet = staffCarWashWorkerCarpetWashing.Sum(x => x.Payroll);       // ЗП сотрудников ковры

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
            var OrderCarWashList = orderServices.Where(x => x.typeOfOrder == 2);
            var OrderDetelingList = orderServices.Where(x => x.typeOfOrder == 1);
            var OrderCarpetList = orderServices.Where(x => x.typeOfOrder == 3);

            var cashWhere = orderServices.Where(x => x.PaymentState == 1);
            var nonCashWhere = orderServices.Where(x => x.PaymentState == 2);

            var numberOfUnpaidOrdersCarWash = OrderCarWashList.Where(x => x.StatusOrder == 4);
            var numberOfUnpaidOrdersDetailings = OrderDetelingList.Where(x => x.StatusOrder == 4);
            var numberOfUnpaidOrdersCarpetWashing = OrderCarpetList.Where(x => x.StatusOrder == 4);
          
            OrderInformationWashingDetailingBll orderInformation = new OrderInformationWashingDetailingBll();

            orderInformation.OrderCarWashSum = OrderCarWashList.Sum(x => x.DiscountPrice);        // Касса мойки
            orderInformation.OrderDetailing = OrderDetelingList.Sum(x => x.DiscountPrice);      // Касса детейлинг
            orderInformation.carpetOrder = OrderCarpetList.Sum(x => x.DiscountPrice);      // Касса ковров

            orderInformation.cash = cashWhere.Sum(s => s.DiscountPrice);                 // Наличный расчет
            orderInformation.nonCash = nonCashWhere.Sum(s => s.DiscountPrice);           // Безналичный расчет          

            // Количество заказов ожидающих оплату
            orderInformation.numberOfUnpaidOrdersCarWash = numberOfUnpaidOrdersCarWash.Sum(x => x.DiscountPrice);  // Количество заказов ожидающих оплату (мойка)
            orderInformation.numberOfUnpaidOrdersDetailings = numberOfUnpaidOrdersDetailings.Sum(x => x.DiscountPrice);   // Количество заказов ожидающих оплату (Детейлинг)
            orderInformation.numberOfUnpaidOrdersCarpetWashing = numberOfUnpaidOrdersCarpetWashing.Sum(x => x.DiscountPrice);     // Количество заказов ожидающих оплату (Ковры)

            orderInformation.OrderCount = OrderCarWashList.Count();
            orderInformation.CarCountDetailings = OrderDetelingList.Count();
            orderInformation.CauntCarpet = OrderCarpetList.Count();

            return orderInformation;
        }
        #endregion
    }
}
