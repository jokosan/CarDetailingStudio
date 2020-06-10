using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface IDayResult
    {
        Task<IEnumerable<DayResultModelBll>> DayResultViewInfo();
        Task<IEnumerable<DayResultModelBll>> TotalForEachEmployee();
    }
}