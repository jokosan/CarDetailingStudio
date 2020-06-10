using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface ISalaryExpenses : IGetFromDatabase<SalaryExpensesBll>, IDatabaseOperations<SalaryExpensesBll>
    {
        Task Insert(IEnumerable<SalaryExpensesBll> element);
    }
}
