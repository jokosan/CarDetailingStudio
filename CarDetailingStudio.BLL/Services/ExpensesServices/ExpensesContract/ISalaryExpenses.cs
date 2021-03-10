using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract
{
    public interface ISalaryExpenses : IGetFromDatabase<SalaryExpensesBll>, IDatabaseOperations<SalaryExpensesBll>, IReports<SalaryExpensesBll>
    {
        Task<IEnumerable<SalaryExpensesBll>> PayrollExpensesPerMonth(int? id, int month, int year);
        Task Insert(IEnumerable<SalaryExpensesBll> element);
    }
}
