using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift
{
    public class DayResult : IDayResult
    {
        private readonly IOrderCarWashWorkersServices _orderCarWashWorkersServices;
        private readonly IWagesForDaysWorkedGroup _wagesForDays;
        private readonly ISalaryBalanceService _salaryBalance;
        private readonly IBonusToSalary _bonusToSalary;
        private readonly ISalaryArchive _salaryArchive;
        private readonly IEmployeeRate _employeeRate;
        private readonly IJobTitleTableServices _jobTitleTable;

        public DayResult(
            IOrderCarWashWorkersServices orderCarWashWorkersServices,
            IWagesForDaysWorkedGroup wagesForDays,
            ISalaryBalanceService salaryBalance,
            IBonusToSalary bonusToSalary,
            ISalaryArchive salaryArchive,
            IEmployeeRate employeeRate,
            IJobTitleTableServices jobTitleTable)
        {
            _orderCarWashWorkersServices = orderCarWashWorkersServices;
            _wagesForDays = wagesForDays;
            _salaryBalance = salaryBalance;
            _bonusToSalary = bonusToSalary;
            _salaryArchive = salaryArchive;
            _employeeRate = employeeRate;
            _jobTitleTable = jobTitleTable;
        }

        public async Task<IEnumerable<DayResultModelBll>> DayResultViewInfo()
        {
            //  var resultGetInclud = await _orderCarWashWorkersServices.GetClosedDay();
            var resultGetInclud = await _orderCarWashWorkersServices.Reports(DateTime.Now);
            return resultGetInclud.GroupBy(x => x.IdCarWashWorkers)
                                  .Select(y => new DayResultModelBll
                                  {
                                      carWashWorkersId = y.First().CarWashWorkers.id,
                                      orderCount = y.Count(),
                                      surname = y.First().CarWashWorkers.Surname,
                                      name = y.First().CarWashWorkers.Name,
                                      patronymic = y.First().CarWashWorkers.Patronymic,
                                      calculationStatus = y.First().CalculationStatus,
                                      payroll = y.Sum(s => s.Payroll),
                                      salaryDate = test(y.First().salaryDate)
                                  });
        }

        public async Task<IEnumerable<DayResultModelBll>> TotalForEachEmployee()
        {
            var result = await _orderCarWashWorkersServices.GetTableInclud(DateTime.Now.Month, DateTime.Now.Year);            
            return GrupDayResult(result);
        }

        private IEnumerable<DayResultModelBll> GrupDayResult(IEnumerable<OrderCarWashWorkersBll> getResult)
        {
            return getResult.GroupBy(x => x.IdCarWashWorkers)
                                  .Select(y => new DayResultModelBll
                                  {
                                      carWashWorkersId = y.First().CarWashWorkers.id,
                                      orderCount = y.Count(),
                                      surname = y.First().CarWashWorkers.Surname,
                                      name = y.First().CarWashWorkers.Name,
                                      patronymic = y.First().CarWashWorkers.Patronymic,
                                      calculationStatus = y.First().CalculationStatus,
                                      payroll = TotalPayroll(y.First().CarWashWorkers.id),
                                      salaryDate = test(y.First().salaryDate)
                                  });
        }

        public DateTime test(DateTime? date) => Convert.ToDateTime(date?.ToString("dd.MM.yyyy"));

        public double? TotalPayroll(int idCarWash)
        {
            var viewResult = Task.Run(() => _wagesForDays.MonthOrderResult(idCarWash, DateTime.Now.Month, DateTime.Now.Year)).Result;
            var payouts = Task.Run(() => _salaryBalance.SelectIdToDate(idCarWash, DateTime.Now)).Result;
            var salaryBalance = Task.Run(() => _salaryBalance.LastMonthBalance(idCarWash)).Result;
            var bonusSalary = Task.Run(() => _bonusToSalary.WhereMontsBonusToSalary(idCarWash)).Result;
            var salaryArchive = Task.Run(() => _salaryArchive.LastMonth(idCarWash)).Result;
            var employeeRate = Task.Run(() => _employeeRate.WherySumRate(idCarWash)).Result;

            double monthlySalary = viewResult.Sum(s => s.payroll).Value;
            double paidPerMonth = payouts.Sum(s => s.payoutAmount).Value;
            double bonus = bonusSalary.Sum(s => s.amount).Value;
            double rate = employeeRate.Sum(s => s.wage).Value;

            double lastMonthBalance = 0;
            double resultSalaryArchive = 0;
         
            if (salaryArchive != null)
            {
                resultSalaryArchive = salaryArchive.balanceAtTheEndOfTheMonth.Value;
            }

            if (salaryBalance != null)
            {
                lastMonthBalance = salaryBalance.accountBalance.Value - salaryBalance.payoutAmount.Value;
            }

            return Math.Round(monthlySalary - paidPerMonth + lastMonthBalance + resultSalaryArchive + bonus + rate, 3);
        }
    }
}

