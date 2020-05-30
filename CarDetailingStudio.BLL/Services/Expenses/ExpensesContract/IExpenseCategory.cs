using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface IExpenseCategory : IGetFromDatabase<ExpenseCategoryBll>, IDatabaseOperations<ExpenseCategoryBll>
    {
    }
}
