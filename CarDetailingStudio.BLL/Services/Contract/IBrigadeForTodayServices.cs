using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IBrigadeForTodayServices : IReports<BrigadeForTodayBll>
    {
        Task<IEnumerable<BrigadeForTodayBll>> GetDateTimeNow();
        Task<BrigadeForTodayBll> GetId(int id);
        Task RemoveFromBrigade(int id);
    }
}