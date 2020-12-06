using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract
{
    public interface IAllExpenses
    {
        Task SaveExpenses(AllExpensesBll allExpenses);
    }
}
