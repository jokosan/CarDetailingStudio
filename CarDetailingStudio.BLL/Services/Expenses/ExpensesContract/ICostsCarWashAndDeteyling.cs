using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface ICostsCarWashAndDeteyling : IGetFromDatabase<CostsCarWashAndDeteylingBll>, IDatabaseOperations<CostsCarWashAndDeteylingBll>
    {
        IEnumerable<CostsCarWashAndDeteylingBll> MonthlyReport(DateTime date);
    }
}
