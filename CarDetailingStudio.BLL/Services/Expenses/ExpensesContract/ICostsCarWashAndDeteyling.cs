using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface ICostsCarWashAndDeteyling : IGetFromDatabase<CostsCarWashAndDeteylingBll>, IDatabaseOperations<CostsCarWashAndDeteylingBll>, IReports<CostsCarWashAndDeteylingBll>
    {
   
    }
}
