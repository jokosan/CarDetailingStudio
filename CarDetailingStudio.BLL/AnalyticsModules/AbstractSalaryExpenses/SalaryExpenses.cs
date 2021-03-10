using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractSalaryExpenses
{
    public class SalaryExpenses : IAbstractSalaryExpenses
    {
        private readonly ISalaryExpenses _salaryExpenses;

        public SalaryExpenses(ISalaryExpenses salaryExpenses)
        {
            _salaryExpenses = salaryExpenses;
        }

        public async Task<IEnumerable<SalaryExpensesBll>> SalaryExpensesInfo(DateTime date, int paymentState = 0)
        {
            var resultSalaryExpenses = await _salaryExpenses.Reports(date);

            if (paymentState != 0)
                return resultSalaryExpenses.Where(x => x.Expenses.paymentType == paymentState);

            return resultSalaryExpenses;
        }

        public async Task<IEnumerable<SalaryExpensesBll>> SalaryExpensesInfo(DateTime start, DateTime finalint, int paymentState = 0)
        {
            var resultSalaryExpenses = await _salaryExpenses.Reports(start, finalint);

            if (paymentState != 0)
                return resultSalaryExpenses.Where(x => x.Expenses.paymentType == paymentState);

            return resultSalaryExpenses;
        }

        public IEnumerable<ExpensesBll> ExpenseCategoryWages(IEnumerable<SalaryExpensesBll> expenses)
        {
            List<ExpensesBll> expensesBll = new List<ExpensesBll>();

            foreach (var item in expenses)
            {
                expensesBll.Add(new ExpensesBll
                {
                    idExpenses = item.expenseId.Value,
                    dateExpenses = item.Expenses.dateExpenses.Value.Date,
                   //expenseCategory = new ExpenseCategoryBll() { name = item.Expenses.costCategories.Name},
                    Amount = item.Expenses.Amount.Value,
                    paymentType = item.Expenses.paymentType,
                    note = $"{item.CarWashWorkers.Surname} {item.CarWashWorkers.Name} {item.CarWashWorkers.Patronymic}"
                });
            }

            return expensesBll.AsEnumerable();
        }
    }
}
