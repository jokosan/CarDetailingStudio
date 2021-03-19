using CarDetailingStudio.BLL.EmployeesModules.Model;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.EmployeesModules.Contract
{
    public interface IStaffShift
    {
        Task<ChangeOfDay> NewShifts();
        Task<ChangeOfDay> NewShifts(DateTime dateTime);
        Task<IEnumerable<CarWashWorkersBll>> EmployeeToPerformServices(int idServises, DateTime? date = null);
    }
}
