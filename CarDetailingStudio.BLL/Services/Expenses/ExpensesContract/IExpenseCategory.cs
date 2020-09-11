using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface IExpenseCategory : IGetFromDatabase<ExpenseCategoryBll>, IDatabaseOperations<ExpenseCategoryBll>
    {
        Task<IEnumerable<ExpenseCategoryBll>> GetTableAll(int id);
    }
}
