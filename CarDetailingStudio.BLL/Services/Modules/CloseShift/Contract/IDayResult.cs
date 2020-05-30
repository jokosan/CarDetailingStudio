using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface IDayResult
    {
        IEnumerable<DayResultModelBll> DayResultViewInfo();
        IEnumerable<DayResultModelBll> TotalForEachEmployee();
    }
}