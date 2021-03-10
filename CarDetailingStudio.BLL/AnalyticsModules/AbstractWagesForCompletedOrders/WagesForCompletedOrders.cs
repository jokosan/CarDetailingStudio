using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.AnalyticsModules.Models.WagesForCompletedOrders;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractWagesForCompletedOrders
{
    class WagesForCompletedOrders : IWagesForCompletedOrders
    {
        private readonly IOrderCarWashWorkersServices _orderCarWashWorkers;
        private readonly IBonusToSalary _bonusToSalary;
        private readonly IEmployeeRate _employeeRate;

        public WagesForCompletedOrders(
            IOrderCarWashWorkersServices orderCarWashWorkers,
            IBonusToSalary bonusToSalary,
            IEmployeeRate employeeRate)
        {
            _orderCarWashWorkers = orderCarWashWorkers;
            _bonusToSalary = bonusToSalary;
            _employeeRate = employeeRate;
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> AnalyticsWages(DateTime date) => await _orderCarWashWorkers.Reports(date);
        public async Task<IEnumerable<OrderCarWashWorkersBll>> AnalyticsWages(DateTime date, DateTime final) => await _orderCarWashWorkers.Reports(date, final);

        public IEnumerable<OrderCarWashWorkersBll> EarningsPerEmployee(int typeServices, int idEmployees, IEnumerable<OrderCarWashWorkersBll> orderCarWashWorkers) =>
            orderCarWashWorkers.Where(x => x.IdCarWashWorkers == idEmployees && x.typeServicesId == typeServices );

        public IEnumerable<GroupingEmployeesWages> GroupingByDatesAndEmployees(IEnumerable<OrderCarWashWorkersBll> orderCarWashWorkers)
        {                        
            return orderCarWashWorkers.GroupBy(x => new { x.IdCarWashWorkers, x.OrderServicesCarWash.OrderDate.Value.Date })
                    .Select(y => new GroupingEmployeesWages 
                    {
                        idEmployees = y.First().IdCarWashWorkers,
                        date = y.First().OrderServicesCarWash.OrderDate.Value,
                        nameEmployees = y.First().CarWashWorkers.Surname + " " +
                                        y.First().CarWashWorkers.Name + " " + 
                                        y.First().CarWashWorkers.Patronymic,
                        orderCount = y.Count(),
                        wegesSum = y.Sum(s => s.Payroll.Value),
                        orderSum = y.Sum(s => s.OrderServicesCarWash.DiscountPrice.Value),
                        idOrder = y.First().IdOrder
                    }).AsEnumerable();
        }

        public async Task<WagesForCompletedOrdersModels> WagesForCompletedOrdersForTheSelectedPeriod(DateTime date, DateTime final)
        {
            var wagesResult = await _orderCarWashWorkers.Reports(date, final);
            var bonusToSalary = await BonusToSalaryInfo(date, final);
            var employeeRate = await EmployeeRateInfo(date, final);

            return AnalyticsFormationWages(wagesResult, bonusToSalary, employeeRate);
        }

        public async Task<WagesForCompletedOrdersModels> WagesForCompletedOrdersPerDay(DateTime date)
        {
            var wagesResult = await _orderCarWashWorkers.Reports(date);
            var bonusToSalary = await BonusToSalaryInfo(date);
            var employeeRate = await EmployeeRateInfo(date);

            return AnalyticsFormationWages(wagesResult, bonusToSalary, employeeRate);
        }

        public async Task<IEnumerable<BonusToSalaryBll>> BonusToSalaryInfo(DateTime date) => await _bonusToSalary.Reports(date);
        public async Task<IEnumerable<BonusToSalaryBll>> BonusToSalaryInfo(DateTime dateStaart, DateTime dateFinal) => await _bonusToSalary.Reports(dateStaart, dateFinal);

        public async Task<IEnumerable<EmployeeRateBll>> EmployeeRateInfo(DateTime date) => await _employeeRate.Reports(date);
        public async Task<IEnumerable<EmployeeRateBll>> EmployeeRateInfo(DateTime dateStaart, DateTime dateFinal) => await _employeeRate.Reports(dateStaart, dateFinal);

        private WagesForCompletedOrdersModels AnalyticsFormationWages(IEnumerable<OrderCarWashWorkersBll> orderCarWashes, IEnumerable<BonusToSalaryBll> bonus, IEnumerable<EmployeeRateBll> employees )
        {
            WagesForCompletedOrdersModels models = new WagesForCompletedOrdersModels();

            models.Detailing = new SalaryInformation();
            models.Detailing.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 1).Sum(s => s.Payroll).Value, 3);
            models.Detailing.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 4).Sum(s => s.Payroll).Value, 3);

            models.Washing = new SalaryInformation();
            models.Washing.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 2).Sum(s => s.Payroll).Value, 3);
            models.Washing.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 5).Sum(s => s.Payroll).Value, 3);

            models.CarpetWashing = new SalaryInformation();
            models.CarpetWashing.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 3).Sum(s => s.Payroll).Value, 3);
            models.CarpetWashing.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 6).Sum(s => s.Payroll).Value, 3);

            models.TireStorage = new SalaryInformation();
            models.TireStorage.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 7).Sum(s => s.Payroll).Value, 3);
            models.TireStorage.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 8).Sum(s => s.Payroll).Value, 3);

            models.TireFitting = new SalaryInformation();
            models.TireFitting.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 9).Sum(s => s.Payroll).Value, 3);
            models.TireFitting.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 10).Sum(s => s.Payroll).Value, 3);

            models.SumBonusTOSalary = Math.Round(bonus.Sum(s => s.amount).Value, 3);
            models.SumEmployeeRate = Math.Round(employees.Sum(s => s.wage).Value, 3);

            return models;
        }
    }
}
