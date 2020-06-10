using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IBrigadeForTodayServices
    {
        Task<IEnumerable<BrigadeForTodayBll>> GetDateTimeNow();
        Task<BrigadeForTodayBll> GetId(int id);
        Task<IEnumerable<BrigadeForTodayBll>> Info(int? id);
        Task RemoveFromBrigade(int id);
    }
}