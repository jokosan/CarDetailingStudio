using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<IEnumerable<WagesForDaysWorkedBll>> MonthOrderResult(int? id, int month, int year) =>
            GroupOrderCarWashWorkers(await _orderCarWash.SelectMonth(id, month, year));

        public async Task<IEnumerable<WagesForDaysWorkedBll>> DayOrderResult(int? Id) =>
             GroupOrderCarWashWorkers(await _orderCarWash.SampleForPayroll(Id));

        private IEnumerable<WagesForDaysWorkedBll> GroupOrderCarWashWorkers(IEnumerable<OrderCarWashWorkersBll> orderCarWashes) =>
             orderCarWashes.GroupBy(x => x.OrderServicesCarWash.OrderDate?.ToString("dd.MM.yyyy"))
                                  .Select(y => new WagesForDaysWorkedBll
                                  {
                                      carWashWorkersId = y.First().IdCarWashWorkers,
                                      ClosingData = y.First().OrderServicesCarWash.ClosingData,
                                      DiscountPrice = y.Sum(s => s.OrderServicesCarWash.DiscountPrice),
                                      orderCount = y.Count(),
                                      calculationStatus = y.First().CalculationStatus,
                                      payroll = y.Sum(s => s.Payroll)
                                  });

        public async Task<IEnumerable<SalaryBalanceBll>> SalaryBalanceGroup(int? Id)
        {
            var salaryExpenses = await _salaryBalance.GetTableAll();

            return salaryExpenses.GroupBy(x => x.dateOfPayment?.ToString("dd.MM.yyyy"))
                                                   .Select(y => new SalaryBalanceBll
                                                   {
                                                       idSalaryBalance = y.First().idSalaryBalance,
                                                       CarWashWorkersId = y.First().CarWashWorkersId,
                                                       dateOfPayment = y.First().dateOfPayment,
                                                       payoutAmount = y.Sum(s => s.payoutAmount),
                                                       currentMonthStatus = y.First().currentMonthStatus
                                                   });
        }

        public async Task PaymentOfPartOfTheSalary(int? employeeId, double payoutAmount, double totalPayable, double SalaryCurrentMonth, double Prize, double BalancLastMonth, double PaidMonth  )
        {
            var lostMonthBalance = await _salaryBalance.LastMonthBalance(employeeId);
            SalaryBalanceBll salaryBalanceBll = new SalaryBalanceBll();

            if (BalancLastMonth != 0)
            { 
                
            }


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
                }
            }

            salaryBalanceBll.CarWashWorkersId = employeeId;
            salaryBalanceBll.dateOfPayment = DateTime.Now;
            salaryBalanceBll.payoutAmount = payoutAmount;
            salaryBalanceBll.accountBalance = totalPayable;

            await _salaryBalance.Insert(salaryBalanceBll);
            await PayrollExpenses(salaryBalanceBll);
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
