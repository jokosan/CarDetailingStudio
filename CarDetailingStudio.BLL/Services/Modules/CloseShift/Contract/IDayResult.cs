using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface IDayResult
    {
        IEnumerable<DayResultModelBll> DayResultViewInfo();
        IEnumerable<DayResultModelBll> TotalForEachEmployee();
    }
}