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
        private IOrderCarWashWorkersServices _orderCarWashWorkersServices;
        private IWagesForDaysWorkedGroup _wagesForDays;
        private ISalaryBalanceService _salaryBalance;
        private IBonusToSalary _bonusToSalary;

        public DayResult(IOrderCarWashWorkersServices orderCarWashWorkersServices, IWagesForDaysWorkedGroup wagesForDays,
                         ISalaryBalanceService salaryBalance, IBonusToSalary bonusToSalary)
        {
            _orderCarWashWorkersServices = orderCarWashWorkersServices;
            _wagesForDays = wagesForDays;
            _salaryBalance = salaryBalance;
            _bonusToSalary = bonusToSalary;
        }

        public async Task<IEnumerable<DayResultModelBll>> DayResultViewInfo()
        {
            var resultGetInclud = await _orderCarWashWorkersServices.GetClosedDay();
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
            var result = await _orderCarWashWorkersServices.GetTableInclud();
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
            var viewResult = Task.Run(() => _wagesForDays.DayOrderResult(idCarWash)).Result;
            var payouts = Task.Run(() => _salaryBalance.SelectIdToDate(idCarWash, DateTime.Now)).Result;
            var salaryBalance = Task.Run(() => _salaryBalance.LastMonthBalance(idCarWash)).Result;
            var bonusSalary = Task.Run(() => _bonusToSalary.WhereMontsBonusToSalary()).Result;

            double monthlySalary = viewResult.Sum(s => s.payroll).Value;
            double paidPerMonth = payouts.Sum(s => s.payoutAmount).Value;
            double bonus = bonusSalary.Sum(s => s.amount).Value;

            double lastMonthBalance = 0;

            if (salaryBalance != null)
            {
                lastMonthBalance = salaryBalance.accountBalance.Value - salaryBalance.payoutAmount.Value;
            }

            return Math.Round(monthlySalary - paidPerMonth + lastMonthBalance + bonus, 3);
        }
    }
}
