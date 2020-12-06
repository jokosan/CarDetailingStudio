using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract
{
    public interface IExpenses : IReports<ExpensesBll>, IDatabaseOperations<ExpensesBll>, IGetFromDatabase<ExpensesBll>
    {
        Task<IEnumerable<ExpensesBll>> GetTableAll(int idTypeExpenses);
        Task<int> InsertId(ExpensesBll element);
    }
}
