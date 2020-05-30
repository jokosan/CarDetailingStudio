using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface IUtilityCosts : IGetFromDatabase<UtilityCostsBll>, IDatabaseOperations<UtilityCostsBll>
    {
        IEnumerable<UtilityCostsBll> MonthlyReport(DateTime date);
    }
}
