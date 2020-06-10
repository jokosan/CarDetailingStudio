using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface IUtilityCosts : IGetFromDatabase<UtilityCostsBll>, IDatabaseOperations<UtilityCostsBll>
    {
        Task<IEnumerable<UtilityCostsBll>> MonthlyReport(DateTime date);
    }
}
