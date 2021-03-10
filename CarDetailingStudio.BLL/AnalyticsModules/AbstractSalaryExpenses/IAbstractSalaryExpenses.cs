using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractSalaryExpenses
{
    public interface IAbstractSalaryExpenses
    {
        Task<IEnumerable<SalaryExpensesBll>> SalaryExpensesInfo(DateTime date, int paymentState = 0);
        Task<IEnumerable<SalaryExpensesBll>> SalaryExpensesInfo(DateTime start, DateTime finalint, int paymentState = 0);
        IEnumerable<ExpensesBll> ExpenseCategoryWages(IEnumerable<SalaryExpensesBll> expenses);
    }
}
