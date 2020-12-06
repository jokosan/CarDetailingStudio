using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract
{
    public interface IUtilityCosts : IGetFromDatabase<UtilityCostsBll>, IDatabaseOperations<UtilityCostsBll>
    {
  
    }
}
