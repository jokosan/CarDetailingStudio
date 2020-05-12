using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface IExpenseCategory : IGetFromDatabase<ExpenseCategoryBll>, IDatabaseOperations<ExpenseCategoryBll>
    {
    }
}
