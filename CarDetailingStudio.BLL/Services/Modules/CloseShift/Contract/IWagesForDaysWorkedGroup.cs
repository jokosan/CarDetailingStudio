using CarDetailingStudio.BLL.Model.ModelViewBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface IWagesForDaysWorkedGroup
    {
        IEnumerable<WagesForDaysWorkedBll> DayOrderResult(int? Id);
        void PayrollForDaysWorked(List<string> day, List<string> carWashWorkers);
        void PayWagesForAllDays(int? idCarWashWorkers);
        void PartPayroll(List<string> idCureentShift);
    }
}
