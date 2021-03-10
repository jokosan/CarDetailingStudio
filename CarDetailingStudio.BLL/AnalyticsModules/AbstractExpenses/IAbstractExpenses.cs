using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractExpenses
{
    interface IAbstractExpenses
    {
        Task<IEnumerable<ExpensesBll>> ExpensesPerDay(DateTime date);
        Task<IEnumerable<ExpensesBll>> ExpensesPerDay(DateTime date, int paymentState);
        Task<IEnumerable<ExpensesBll>> ExpensesForTheSelectedPeriod(DateTime start, DateTime final);
        Task<IEnumerable<ExpensesBll>> ExpensesForTheSelectedPeriod(DateTime start, DateTime finalint, int paymentState);
        IEnumerable<ExpensesClassModels> GroupExpenseCategory(IEnumerable<ExpensesBll> expenses);
        IEnumerable<ExpensesClassModels> GroupCostCategories(IEnumerable<ExpensesBll> expenses);
    }
}
