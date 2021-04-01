using CarDetailingStudio.BLL.EmployeesModules.Contract;
using CarDetailingStudio.BLL.EmployeesModules.Model;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.EmployeesModules
{
    public class AccrualOfPremiumFacade : IAccrualOfPremium
    {
        private readonly IPremiumAndRateServices _premiumAndRate;
        private readonly IOrderCarWashWorkersServices _orderCarWashWorkers;
        private readonly IPosition _position;
        private readonly IBonusToSalary _bonus;

        public AccrualOfPremiumFacade(
            IPremiumAndRateServices premiumAndRate,
            IPosition position,
            IOrderCarWashWorkersServices orderCarWashWorkers,
            IBonusToSalary bonus)
        {
            _premiumAndRate = premiumAndRate;
            _position = position;
            _orderCarWashWorkers = orderCarWashWorkers;
            _bonus = bonus;
        }

        public async Task CloseShift(int idPositionServises)
        {
            var position = await _position.ChoiceEmployees(idPositionServises);
            var ordersFulfilled = await _orderCarWashWorkers.SampleForPayroll(DateTime.Now);

            List<EmployeeDaySummary> employeeDaySummaryList = new List<EmployeeDaySummary>();

            foreach (var item in ordersFulfilled.GroupBy(g => g.IdCarWashWorkers))
            {
                var completedEmployeeOrders = ordersFulfilled.Where(x => x.IdCarWashWorkers == item.Key);
                var employees = await _premiumAndRate.SelectPosition(item.Key);

                foreach (var positionItem in position)
                {
                    bool bonusStatus = false;

                    var resultEmployee = employees.SingleOrDefault(x =>
                        x.positionId == positionItem.idPosition);

                    if (resultEmployee.servicePremium)
                    {
                        double sum = completedEmployeeOrders
                                     .Where(x => x.typeServicesId == positionItem.idPosition)
                                     .Sum(s => s.Payroll).Value;
                        double interestRate = sum / ((double)resultEmployee.percentageRatePerOrder / 100);
                        double bonusResult = MathFloor(interestRate, resultEmployee.multiplicityOfTheSum.Value);

                        if (bonusResult > 0)
                        {
                            BonusToSalaryBll bonusToSalaryResult = new BonusToSalaryBll();

                            bonusToSalaryResult.amount = bonusResult * resultEmployee.prizeAmount;
                            bonusToSalaryResult.carWashWorkersId = item.Key;
                            bonusToSalaryResult.date = DateTime.Now;
                            bonusToSalaryResult.note = $"{positionItem.name} сумма кассы {interestRate }";

                            bonusStatus = await InsertTableBonus(bonusToSalaryResult);                            
                        }
                        else if (interestRate != 0)
                        {
                            bonusStatus = true;
                            employeeDaySummaryList.Add(new EmployeeDaySummary
                            {
                                idServises = positionItem.idPosition,
                                idEmployee = item.Key,
                                NameServises = positionItem.name,
                                Sum = sum,
                                status = bonusStatus,
                                multiplicityOfTheSum = resultEmployee.multiplicityOfTheSum.Value,
                                prizeAmount = resultEmployee.prizeAmount.Value,
                                InterestRate = (double)resultEmployee.percentageRatePerOrder / 100
                            });
                        }
                    }
                }
            }

            if (employeeDaySummaryList.Any(x => x.status == true))
                await TotalShiftBonus(employeeDaySummaryList);
        }

        private double MathFloor(double x, double y) => Math.Floor(x / y);

        private async Task TotalShiftBonus(List<EmployeeDaySummary> employeeDaySummaries)
        {
            foreach (var item in employeeDaySummaries.GroupBy(x => x.idEmployee))
            {
                var resultEmployee = employeeDaySummaries.Where(x => x.idEmployee == item.Key);

                double multiplicityAverage = resultEmployee.Average(a => a.multiplicityOfTheSum);
                double prizeAmountAverage = resultEmployee.Average(a => a.prizeAmount);
                double interestRateAverage = resultEmployee.Average(a => a.InterestRate);
                double sum = resultEmployee.Sum(a => a.Sum);
                double bonusResultAverage = MathFloor(sum / interestRateAverage, multiplicityAverage);

                var carWashWorker = employeeDaySummaries.First();

                if (bonusResultAverage > 0)
                {
                    BonusToSalaryBll bonusToSalaryResult = new BonusToSalaryBll();

                    bonusToSalaryResult.amount = bonusResultAverage * prizeAmountAverage;
                    bonusToSalaryResult.carWashWorkersId = carWashWorker.idEmployee;
                    bonusToSalaryResult.date = DateTime.Now;
                    bonusToSalaryResult.note = $"Премия сформирована по средним показателям: {sum / interestRateAverage}";

                    await InsertTableBonus(bonusToSalaryResult);
                }
            }        
        }

        private async Task<bool> InsertTableBonus(BonusToSalaryBll bonusToSalary)
        {
            await _bonus.Insert(bonusToSalary);
            return true;
        }
    }
}

