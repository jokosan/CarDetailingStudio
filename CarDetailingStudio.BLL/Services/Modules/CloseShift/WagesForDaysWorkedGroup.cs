using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift
{
    public class WagesForDaysWorkedGroup : IWagesForDaysWorkedGroup
    {
        private IOrderCarWashWorkersServices _orderCarWash;
        private ISalaryExpenses _salaryExpenses;
        private ISalaryBalanceService _salaryBalance;
        private IExpenses _expenses;

        public WagesForDaysWorkedGroup(
            IOrderCarWashWorkersServices orderCarWash,
            ISalaryExpenses salaryExpenses,
            ISalaryBalanceService salaryBalance,
            IExpenses expenses)
        {
            _orderCarWash = orderCarWash;
            _salaryExpenses = salaryExpenses;
            _salaryBalance = salaryBalance;
            _expenses = expenses;
        }

        public async Task<IEnumerable<WagesForDaysWorkedBll>> DayOrderResult(int? Id)
        {
            var getResult = await _orderCarWash.SampleForPayroll(Id);

            return getResult.GroupBy(x => x.OrderServicesCarWash.OrderDate?.ToString("dd.MM.yyyy"))
                              .Select(y => new WagesForDaysWorkedBll
                              {
                                  carWashWorkersId = y.First().IdCarWashWorkers,
                                  ClosingData = y.First().OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy"),
                                  DiscountPrice = y.Sum(s => s.OrderServicesCarWash.DiscountPrice),
                                  orderCount = y.Count(),
                                  calculationStatus = y.First().CalculationStatus,
                                  payroll = y.Sum(s => s.Payroll)
                              });
        }

        public async Task PaymentOfPartOfTheSalary(int? employeeId, double payoutAmount, double totalPayable, bool closeMonth, bool NegativeBalance = false)
        {
            var lostMonthBalance = await _salaryBalance.LastMonthBalance(employeeId);
            SalaryBalanceBll salaryBalanceBll = new SalaryBalanceBll();

            double balance = 0;

            if (lostMonthBalance != null)
            {
                balance = lostMonthBalance.accountBalance.Value - lostMonthBalance.payoutAmount.Value;

                if (payoutAmount >= lostMonthBalance.payoutAmount)
                {
                    lostMonthBalance.accountBalance = 0;
                    await _salaryBalance.Update(lostMonthBalance);
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
                        await _salaryBalance.Update(lostMonthBalance);
                    }
                }
            }

            salaryBalanceBll.CarWashWorkersId = employeeId;
            salaryBalanceBll.dateOfPayment = DateTime.Now;
            salaryBalanceBll.payoutAmount = payoutAmount;
            salaryBalanceBll.accountBalance = totalPayable;
            salaryBalanceBll.currentMonthStatus = closeMonth;

            await _salaryBalance.Insert(salaryBalanceBll);
            await PayrollExpenses(salaryBalanceBll);

            if (closeMonth == true)
            {
                OrderCarWashWorkersBll orderCarWash = new OrderCarWashWorkersBll();
                var getResult = await _orderCarWash.SampleForPayroll(employeeId);

                foreach (var item in getResult)
                {
                    orderCarWash = item;
                    orderCarWash.CalculationStatus = true;
                    orderCarWash.salaryDate = salaryBalanceBll.dateOfPayment;

                   await _orderCarWash.UpdateOrderCarWashWorkers(orderCarWash);
                }
            }
        }

        // ВНИМАНИЕЕ !!!!!!!!!
        // Изменить в соответствии с новой логикой Расходов
        private async Task PayrollExpenses(SalaryBalanceBll salaryBalance)
        {
            ExpensesBll expenses = new ExpensesBll();
            expenses.Amount = salaryBalance.payoutAmount;
            expenses.dateExpenses = salaryBalance.dateOfPayment;
            expenses.expenseCategoryId = 1;

            int id = await _expenses.InsertId(expenses);

            SalaryExpensesBll salaryExpenses = new SalaryExpensesBll();

            salaryExpenses.idCarWashWorkers = salaryBalance.CarWashWorkersId;
            salaryExpenses.expenseId = id;

            await _salaryExpenses.Insert(salaryExpenses);

        }
    }
}
