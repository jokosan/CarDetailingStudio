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

        public async Task<IEnumerable<OrderCarWashWorkersBll>> AnalyticsWages(DateTime date) =>
            await _orderCarWashWorkers.Reports(date);
        public async Task<IEnumerable<OrderCarWashWorkersBll>> AnalyticsWages(DateTime date, DateTime final) =>
            await _orderCarWashWorkers.Reports(date, final);

        public IEnumerable<OrderCarWashWorkersBll> EarningsPerEmployee(int typeServices, int idEmployees, IEnumerable<OrderCarWashWorkersBll> orderCarWashWorkers) =>
            orderCarWashWorkers.Where(x => x.IdCarWashWorkers == idEmployees && x.typeServicesId == typeServices);

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
                        orderSum = Math.Round(y.Select(o => new {o.IdOrder, o.OrderServicesCarWash.DiscountPrice }).Distinct().Sum(s => s.DiscountPrice.Value), 1),
                        idOrder = y.First().IdOrder,

                    }).AsEnumerable();
        }

        public IEnumerable<GroupingEmployeesWages> GroupingEmployees(IEnumerable<OrderCarWashWorkersBll> orderCarWashWorkers)
        {
            return orderCarWashWorkers.GroupBy(x => new { x.IdCarWashWorkers })
                    .Select(y => new GroupingEmployeesWages
                    {
                        idEmployees = y.First().IdCarWashWorkers,
                        nameEmployees = y.First().CarWashWorkers.Surname + " " +
                                        y.First().CarWashWorkers.Name + " " +
                                        y.First().CarWashWorkers.Patronymic,
                        orderCount = y.Count(),
                        wegesSum = y.Sum(s => s.Payroll.Value),
                        orderSum = Math.Round(y.Select(o => new { o.IdOrder, o.OrderServicesCarWash.DiscountPrice }).Distinct().Sum(s => s.DiscountPrice.Value), 1),
                        idOrder = y.First().IdOrder,
                        date = DateTime.MinValue
                    }).AsEnumerable();
        }

        public async Task<IEnumerable<GroupingEmployeesWages>> sumOfAllIncome(IEnumerable<GroupingEmployeesWages> employeesWages, DateTime start, DateTime final)
        {
            List<GroupingEmployeesWages> wages = new List<GroupingEmployeesWages>();
            wages = employeesWages.ToList();

            var bonusToSalaryInfo = await BonusToSalaryInfo(start, final);
            var employeeRateInfo = await EmployeeRateInfo(start, final);

            double sumBonus, sumRate = 0;

            foreach (var item in wages)
            {
                if (item.date == DateTime.MinValue)
                {
                    sumBonus = Math.Round(bonusToSalaryInfo.Where(x => x.carWashWorkersId == item.idEmployees).Sum(s => s.amount).Value, 1);
                    sumRate = Math.Round(employeeRateInfo.Where(x => x.brigadeForToday.IdCarWashWorkers == item.idEmployees).Sum(s => s.wage).Value, 1);
                }
                else
                {
                    sumBonus = Math.Round(bonusToSalaryInfo.Where(x => x.carWashWorkersId == item.idEmployees && x.date.Value.Date == item.date.Date).Sum(s => s.amount).Value, 1);
                    sumRate = Math.Round(employeeRateInfo.Where(x => x.brigadeForToday.IdCarWashWorkers == item.idEmployees && x.brigadeForToday.Date.Value.Date == item.date.Date).Sum(s => s.wage).Value, 1);
                }

                item.bonus = Math.Round(sumBonus + sumRate, 1);
            }

            return wages.AsEnumerable();
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

        public async Task<WagesForCompletedOrdersModels> WagesForCompletedOrdersForTheSelectedPeriod(int idEmployees, DateTime date, DateTime final)
        {
            var wagesResult = await _orderCarWashWorkers.Reports(date, final);
            var bonusToSalary = await BonusToSalaryInfo(date, final);
            var employeeRate = await EmployeeRateInfo(date, final);

            return AnalyticsFormationWages(
                    wagesResult.Where(x => x.IdCarWashWorkers == idEmployees),
                    bonusToSalary.Where(x => x.carWashWorkersId == idEmployees),
                    employeeRate.Where(x => x.brigadeForToday.IdCarWashWorkers == idEmployees));
        }

        public async Task<WagesForCompletedOrdersModels> WagesForCompletedOrdersPerDay(int idEmployees, DateTime date)
        {
            var wagesResult = await _orderCarWashWorkers.Reports(date);
            var bonusToSalary = await BonusToSalaryInfo(date);
            var employeeRate = await EmployeeRateInfo(date);

            return AnalyticsFormationWages(
                   wagesResult.Where(x => x.IdCarWashWorkers == idEmployees),
                   bonusToSalary.Where(x => x.carWashWorkersId == idEmployees && x.date.Value.Date == date.Date),
                   employeeRate.Where(x => x.brigadeForToday.IdCarWashWorkers == idEmployees && x.brigadeForToday.Date.Value.Date == date.Date)
                   );
        }

        public async Task<IEnumerable<BonusToSalaryBll>> BonusToSalaryInfo(DateTime date) 
            => await _bonusToSalary.Reports(date);

        public async Task<IEnumerable<BonusToSalaryBll>> BonusToSalaryInfo(DateTime dateStaart, DateTime dateFinal)
            => await _bonusToSalary.Reports(dateStaart, dateFinal);

        public async Task<IEnumerable<EmployeeRateBll>> EmployeeRateInfo(DateTime date)
            => await _employeeRate.Reports(date);

        public async Task<IEnumerable<EmployeeRateBll>> EmployeeRateInfo(DateTime dateStaart, DateTime dateFinal)
            => await _employeeRate.Reports(dateStaart, dateFinal);

        private WagesForCompletedOrdersModels AnalyticsFormationWages(IEnumerable<OrderCarWashWorkersBll> orderCarWashes, IEnumerable<BonusToSalaryBll> bonus, IEnumerable<EmployeeRateBll> employees)
        {
            WagesForCompletedOrdersModels models = new WagesForCompletedOrdersModels();

            models.Detailing = new SalaryInformation();
            models.Detailing.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 1).Sum(s => s.Payroll).Value, 1);
            models.Detailing.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 4).Sum(s => s.Payroll).Value, 1);

            models.Washing = new SalaryInformation();
            models.Washing.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 2).Sum(s => s.Payroll).Value, 1);
            models.Washing.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 5).Sum(s => s.Payroll).Value, 1);

            models.CarpetWashing = new SalaryInformation();
            models.CarpetWashing.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 3).Sum(s => s.Payroll).Value, 1);
            models.CarpetWashing.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 6).Sum(s => s.Payroll).Value, 1);

            models.TireStorage = new SalaryInformation();
            models.TireStorage.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 7).Sum(s => s.Payroll).Value, 1);
            models.TireStorage.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 8).Sum(s => s.Payroll).Value, 1);

            models.TireFitting = new SalaryInformation();
            models.TireFitting.SalaryAdministrator = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 9).Sum(s => s.Payroll).Value, 1);
            models.TireFitting.SalaryEmployees = Math.Round(orderCarWashes.Where(x => x.typeServicesId == 10).Sum(s => s.Payroll).Value, 1);

            models.SumBonusTOSalary = Math.Round(bonus.Sum(s => s.amount).Value, 1);
            models.SumEmployeeRate = Math.Round(employees.Sum(s => s.wage).Value, 1);

            models.TotalSumWages = Math.Round(orderCarWashes.Sum(s => s.Payroll).Value) + models.SumBonusTOSalary + models.SumEmployeeRate;

            return models;
        }
    }
}
