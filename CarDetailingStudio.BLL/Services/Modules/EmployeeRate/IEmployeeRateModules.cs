using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.EmployeeRate
{
    public interface IEmployeeRateModules
    {
        Task EndDayRateCalculation(BrigadeForTodayBll brigadeForToday);
        Task ClosingShift(IEnumerable<BrigadeForTodayBll> brigadeForTodays);
    }
}
