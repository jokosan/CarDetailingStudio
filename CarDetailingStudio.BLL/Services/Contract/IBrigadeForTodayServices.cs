using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IBrigadeForTodayServices
    {
        IEnumerable<BrigadeForTodayBll> GetDateTimeNow();
        BrigadeForTodayBll GetId(int id);
        IEnumerable<BrigadeForTodayBll> Info(int? id);
        void RemoveFromBrigade(int id);
    }
}