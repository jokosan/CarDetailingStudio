using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.Wage.Contract
{
    public interface ITotalMonthlySalaryModules
    {
        Task CheckForPastMonths();
        Task CheckMonthlyPaymentsEmployee(int? id);
    }
}
