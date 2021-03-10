using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.AnalyticsModules.Models.CompletedOrders;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractExpenses
{
    class Expenses : IAbstractExpenses
    {
        private readonly IExpenses _expenses;
     
        public Expenses(IExpenses expenses)
        {
            _expenses = expenses;
        }

        public async Task<IEnumerable<ExpensesBll>> ExpensesForTheSelectedPeriod(DateTime start, DateTime final) => await _expenses.Reports(start, final);
        
        public async Task<IEnumerable<ExpensesBll>> ExpensesForTheSelectedPeriod(DateTime start, DateTime final, int paymentState)
        {
            var expensesResult = await _expenses.Reports(start, final);
            return expensesResult.Where(x => x.paymentType == paymentState);
        }

        public async Task<IEnumerable<ExpensesBll>> ExpensesPerDay(DateTime date) => await _expenses.Reports(date);

        public async Task<IEnumerable<ExpensesBll>> ExpensesPerDay(DateTime date, int paymentState)
        {
            var expensesResult = await _expenses.Reports(date);
            return expensesResult.Where(x => x.paymentType == paymentState);
        }

        public IEnumerable<ExpensesClassModels> GroupExpenseCategory(IEnumerable<ExpensesBll> expenses)
        {
            return  expenses.GroupBy(x => new { x.expenseCategoryId})
                .Select(y => new ExpensesClassModels
                {
                    expenseCategoryId = y.First().expenseCategoryId,
                    expenseCategoryName = y.First().expenseCategory.name,
                    Amount = y.Sum(s => s.Amount.Value)
                }).OrderBy(o => o.expenseCategoryId);
        }

        public IEnumerable<ExpensesClassModels> GroupCostCategories(IEnumerable<ExpensesBll> expenses)
        {           
            return expenses.GroupBy(x => new { x.idCostCategories, x.dateExpenses.Value.Date })
              .Select(y => new ExpensesClassModels
              {
                  expenseCategoryId = y.First().expenseCategoryId,
                  expenseCategoryName = y.First().expenseCategory.name,
                  dateExpenses = y.First().dateExpenses.Value.Date,
                  Amount = y.Sum(s => s.Amount.Value),
                  CountExpenses = y.Count(),
                  paymentType = y.First().paymentType,
                  idCostCategories = y.First().idCostCategories
              });
        }        
    }
}
