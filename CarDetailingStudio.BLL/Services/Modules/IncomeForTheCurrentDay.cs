using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.Contract;
using CarDetailingStudio.BLL.Services.Modules.ModulesModel;
using CarDetailingStudio.BLL.Services.Trade.TradeContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class IncomeForTheCurrentDay : IIncomeForTheCurrentDay
    {
        private ISalaryExpenses _salaryExpenses;
        private IOrderServicesCarWashServices _orderServicesCarWash;
        private IUtilityCosts _utilityCosts;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private IBonusToSalary _bonusToSalary;
        private IBrigadeForTodayServices _brigadeForToday;
        private IExpenses _expenses;
        private IGoodsSold _goodsSold;

        public IncomeForTheCurrentDay(
            ISalaryExpenses salaryExpenses, 
            IOrderServicesCarWashServices orderServicesCarWash,
            IUtilityCosts utilityCosts, 
            IOrderCarWashWorkersServices orderCarWashWorkers,
            IBonusToSalary bonusToSalary,
            IBrigadeForTodayServices brigadeForToday,
            IExpenses expenses,
            IGoodsSold goodsSold)
        {
            _salaryExpenses = salaryExpenses;
            _orderServicesCarWash = orderServicesCarWash;
            _utilityCosts = utilityCosts;
            _orderCarWashWorkers = orderCarWashWorkers;
            _bonusToSalary = bonusToSalary;
            _brigadeForToday = brigadeForToday;
            _expenses = expenses;
            _goodsSold = goodsSold;
        }

        #region сумма всех расходов
        public async Task<double> SumOfAllExpenses(DateTime? startDate)
        {
            var expensesResult = await _expenses.Reports(startDate.Value);
            return expensesResult.Sum(x => x.Amount.Value);
        }

        public async Task<double> SumOfAllExpenses(DateTime? startDate, DateTime? finalDate)
        {
            var expensesResult = await _expenses.Reports(startDate.Value, finalDate.Value);
            return expensesResult.Sum(x => x.Amount.Value);
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
            var adminTire = orderCarWashWorker.Where(x => x.typeServicesId == 9); // админ шиномонтаж
            var StaffDetailing = orderCarWashWorker.Where(x => x.typeServicesId == 4); // услуги детейлинга
            var staffCarWashWorker = orderCarWashWorker.Where(x => x.typeServicesId == 5); // услуги мойки
            var staffCarWashWorkerCarpetWashing = orderCarWashWorker.Where(x => x.typeServicesId == 6); // услуги по стирке ковров
                                                                                                        // var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 7); // хранение шин админ
                                                                                                        // var adminCarWash = orderCarWashWorker.Where(x => x.typeServicesId == 8); // хранение шин сортудники 
            var StaffTire = orderCarWashWorker.Where(x => x.typeServicesId == 10);                      // услуги шиномонтажа

            employee.AdministratorDetailings = adminDetailing.Sum(x => x.Payroll);            // ЗП админ детейлинг            
            employee.StaffDetailing = StaffDetailing.Sum(x => x.Payroll);                     // ЗП детейлинг

            employee.AdministratorCarWash = adminCarWash.Sum(x => x.Payroll);                 // ЗП админ мойки
            employee.StaffCarWashWorker = staffCarWashWorker.Sum(x => x.Payroll);             // ЗП мойщик

            employee.adminCarpet = adminCarpetWashing.Sum(x => x.Payroll);                    // ЗП админ ковры
            employee.staffCarpet = staffCarWashWorkerCarpetWashing.Sum(x => x.Payroll);       // ЗП сотрудников ковры

            employee.AdministratorTire = adminTire.Sum(x => x.Payroll);                       // ЗП админ шиномонтаж
            employee.StaffTire = StaffTire.Sum(x => x.Payroll);                               // ЗП сотрудники шиномонтаж

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
            var OrderTireList = orderServices.Where(x => x.typeOfOrder == 4);

            var cashWhere = orderServices.Where(x => x.PaymentState == 1);
            var nonCashWhere = orderServices.Where(x => x.PaymentState == 2);

            var numberOfUnpaidOrdersCarWash = OrderCarWashList.Where(x => x.StatusOrder == 4);
            var numberOfUnpaidOrdersDetailings = OrderDetelingList.Where(x => x.StatusOrder == 4);
            var numberOfUnpaidOrdersCarpetWashing = OrderCarpetList.Where(x => x.StatusOrder == 4);
            var numberOfUnpaidOrdersTire = OrderTireList.Where(x => x.StatusOrder == 4);

            OrderInformationWashingDetailingBll orderInformation = new OrderInformationWashingDetailingBll();

            orderInformation.OrderCarWashSum = OrderCarWashList.Sum(x => x.DiscountPrice);        // Касса мойки
            orderInformation.OrderDetailing = OrderDetelingList.Sum(x => x.DiscountPrice);        // Касса детейлинг
            orderInformation.carpetOrder = OrderCarpetList.Sum(x => x.DiscountPrice);      // Касса ковров
            orderInformation.TireOrders = OrderTireList.Sum(x => x.DiscountPrice);       // Касса шиномонтаж

            orderInformation.cash = cashWhere.Sum(s => s.DiscountPrice);                 // Наличный расчет
            orderInformation.nonCash = nonCashWhere.Sum(s => s.DiscountPrice);           // Безналичный расчет          

            // Количество заказов ожидающих оплату
            orderInformation.numberOfUnpaidOrdersCarWash = numberOfUnpaidOrdersCarWash.Sum(x => x.DiscountPrice);  // Количество заказов ожидающих оплату (мойка)
            orderInformation.numberOfUnpaidOrdersDetailings = numberOfUnpaidOrdersDetailings.Sum(x => x.DiscountPrice);   // Количество заказов ожидающих оплату (Детейлинг)
            orderInformation.numberOfUnpaidOrdersCarpetWashing = numberOfUnpaidOrdersCarpetWashing.Sum(x => x.DiscountPrice);     // Количество заказов ожидающих оплату (Ковры)
            orderInformation.numberOfUnpaidTire = numberOfUnpaidOrdersTire.Sum(x => x.DiscountPrice);

            // касса наличных 
            var carWashCash = numberOfUnpaidOrdersCarWash.Where(x => x.PaymentState == 1); //автомойка наличные
            var detailingCash = numberOfUnpaidOrdersDetailings.Where(x => x.PaymentState == 1);// детейлинг наличные
            var cashCarpets = numberOfUnpaidOrdersCarpetWashing.Where(x => x.PaymentState == 1);// ковры наличные
            var tireCash = numberOfUnpaidOrdersTire.Where(x => x.PaymentState == 1); //шиномонтаж наличные

            orderInformation.carWashCashSum = carWashCash.Sum(x => x.DiscountPrice);
            orderInformation.detailingCashSum = detailingCash.Sum(x => x.DiscountPrice);
            orderInformation.cashCarpets = cashCarpets.Sum(x => x.DiscountPrice);
            orderInformation.tireCeash = tireCash.Sum(x => x.DiscountPrice);

            orderInformation.ordersInProgressCarWash = OrderCarWashList.Count(); // заказы мойки ожидают оплаты
            orderInformation.ordersInProgressDetailings = OrderDetelingList.Count(); // заказы детейлиинг ожидает оплаты
            orderInformation.ordersInProgressCarpetWashing = OrderCarpetList.Count(); // заказы чистка ковров ожидает оплаты

            orderInformation.OrderCount = OrderCarWashList.Count();
            orderInformation.CarCountDetailings = OrderDetelingList.Count();
            orderInformation.CauntCarpet = OrderCarpetList.Count();
            orderInformation.CauntTire = OrderTireList.Count();

            return orderInformation;
        }
        #endregion

        public async Task<IEnumerable<OrderServicesCarWashBll>> AboutOrders(int typeOrder, DateTime dateStaer, DateTime? dateFinis)
        {
            IEnumerable<OrderServicesCarWashBll> order;
           
            if (dateFinis != null)
            {
                order = await _orderServicesCarWash.Reports(dateStaer, dateFinis.Value);
            }
            else
            {
                order = await _orderServicesCarWash.Reports(dateStaer);
            }

            return order.Where(x => x.typeOfOrder == typeOrder);

        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> AboutWages(int typeOfEmployees, DateTime dateStaer, DateTime? dateFinis)
        {
            IEnumerable<OrderCarWashWorkersBll> resultDetailsOfSalaries;

            if (dateFinis != null)
            {
                resultDetailsOfSalaries = await _orderCarWashWorkers.Reports(dateStaer, dateFinis.Value);
            }
            else
            {
                resultDetailsOfSalaries = await _orderCarWashWorkers.Reports(dateStaer);
            }

            return resultDetailsOfSalaries.Where(x => x.typeServicesId == typeOfEmployees);
        }
    }
}
