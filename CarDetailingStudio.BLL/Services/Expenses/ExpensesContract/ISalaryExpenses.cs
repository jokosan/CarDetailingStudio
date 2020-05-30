using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface ISalaryExpenses : IGetFromDatabase<SalaryExpensesBll>, IDatabaseOperations<SalaryExpensesBll>
    {
        void Insert(IEnumerable<SalaryExpensesBll> element);
    }
}
