using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IBrigadeForTodayServices : IReports<BrigadeForTodayBll>
    {
        Task<BrigadeForTodayBll> CurrentShift(DateTime date, int id);
        Task<IEnumerable<BrigadeForTodayBll>> GetDateTimeNow();
        Task<bool> AdminTrue(DateTime date, int ststus);
        Task<BrigadeForTodayBll> GetId(int id);
        Task RemoveFromBrigade(int id);
    }
}