using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ISalaryBalanceService : IGetFromDatabase<SalaryBalanceBll>, IDatabaseOperations<SalaryBalanceBll>
    {
        Task<IEnumerable<SalaryBalanceBll>> SelectIdToDate(int? idCarWash, int month, int year);
        Task<IEnumerable<SalaryBalanceBll>> SelectIdToDate(int? idCarWash, DateTime date);
        Task<SalaryBalanceBll> LastMonthBalance(int? id);

    }
}
