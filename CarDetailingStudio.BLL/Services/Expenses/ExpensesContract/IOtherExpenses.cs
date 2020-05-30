using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface IOtherExpenses : IGetFromDatabase<OtherExpensesBll>, IDatabaseOperations<OtherExpensesBll>
    {
        IEnumerable<OtherExpensesBll> MonthlyReport(DateTime date);
    }
}
