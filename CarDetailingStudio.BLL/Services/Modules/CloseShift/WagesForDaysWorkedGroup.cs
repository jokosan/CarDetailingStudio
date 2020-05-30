using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift
{
    public class WagesForDaysWorkedGroup : IWagesForDaysWorkedGroup
    {
        private IOrderCarWashWorkersServices _orderCarWash;
        private ISalaryExpenses _salaryExpenses;
        private ISalaryBalanceService _salaryBalance;

        public WagesForDaysWorkedGroup(IOrderCarWashWorkersServices orderCarWash, ISalaryExpenses salaryExpenses,
                                       ISalaryBalanceService salaryBalance)
        {
            _orderCarWash = orderCarWash;
            _salaryExpenses = salaryExpenses;
            _salaryBalance = salaryBalance;
        }

        public IEnumerable<WagesForDaysWorkedBll> DayOrderResult(int? Id)
        {
            var getResult = _orderCarWash.SampleForPayroll(Id);

            return getResult.GroupBy(x => x.OrderServicesCarWash.OrderDate?.ToString("dd.MM.yyyy"))
                              .Select(y => new WagesForDaysWorkedBll
                              {
                                  carWashWorkersId = y.First().IdCarWashWorkers,
                                  ClosingData = y.First().OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyy"),
                                  DiscountPrice = y.Sum(s => s.OrderServicesCarWash.DiscountPrice),
                                  orderCount = y.Count(),
                                  calculationStatus = y.First().CalculationStatus,
                                  payroll = y.Sum(s => s.Payroll)
                              });
        }

        public void PaymentOfPartOfTheSalary(int? employeeId, double payoutAmount, double totalPayable, bool closeMonth, bool NegativeBalance = false)
        {
            var lostMonthBalance = _salaryBalance.LastMonthBalance(employeeId);
            SalaryBalanceBll salaryBalanceBll = new SalaryBalanceBll();

            double balance = 0;

            if (lostMonthBalance != null)
            {
                balance = lostMonthBalance.accountBalance.Value - lostMonthBalance.payoutAmount.Value;

                if (payoutAmount >= lostMonthBalance.payoutAmount)
                {
                    lostMonthBalance.accountBalance = 0;
                    _salaryBalance.Update(lostMonthBalance);
                }
                else if (balance < 0)
                {
                    if (Math.Sign(balance) == -1)
                    {
                        balance = balance * -1;
                    }

                    if (NegativeBalance && payoutAmount <= balance)
                    {
                        lostMonthBalance.accountBalance = lostMonthBalance.accountBalance + payoutAmount;
                        _salaryBalance.Update(lostMonthBalance);
                    }
                }
            }

            salaryBalanceBll.CarWashWorkersId = employeeId;
            salaryBalanceBll.dateOfPayment = DateTime.Now;
            salaryBalanceBll.payoutAmount = payoutAmount;
            salaryBalanceBll.accountBalance = totalPayable;
            salaryBalanceBll.currentMonthStatus = closeMonth;

            _salaryBalance.Insert(salaryBalanceBll);
            PayrollExpenses(salaryBalanceBll);

            if (closeMonth == true)
            {
                OrderCarWashWorkersBll orderCarWash = new OrderCarWashWorkersBll();
                var getResult = _orderCarWash.SampleForPayroll(employeeId);

                foreach (var item in getResult)
                {
                    orderCarWash = item;
                    orderCarWash.CalculationStatus = true;
                    orderCarWash.salaryDate = salaryBalanceBll.dateOfPayment;

                    _orderCarWash.UpdateOrderCarWashWorkers(orderCarWash);
                }
            }
        }

        private void PayrollExpenses(SalaryBalanceBll salaryBalance)
        {
            SalaryExpensesBll salaryExpenses = new SalaryExpensesBll();

            salaryExpenses.idCarWashWorkers = salaryBalance.CarWashWorkersId;
            salaryExpenses.amount = salaryBalance.payoutAmount;
            salaryExpenses.dateExpenses = salaryBalance.dateOfPayment;
            salaryExpenses.expenseCategoryId = 21;

            _salaryExpenses.Insert(salaryExpenses);
        }
    }
}
