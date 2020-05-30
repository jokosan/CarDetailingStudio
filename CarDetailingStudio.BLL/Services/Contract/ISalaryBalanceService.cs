using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ISalaryBalanceService : IGetFromDatabase<SalaryBalanceBll>, IDatabaseOperations<SalaryBalanceBll>
    {
        IEnumerable<SalaryBalanceBll> SelectIdToDate(int? idCarWash, DateTime date);
        SalaryBalanceBll LastMonthBalance(int? id);

    }
}
