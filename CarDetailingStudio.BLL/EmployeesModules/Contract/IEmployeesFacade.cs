using CarDetailingStudio.BLL.EmployeesModules.Model;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.EmployeesModules.Contract
{
    public interface IEmployeesFacade
    {
        Task<ChangeOfDay> SelectionOfEmployeesToShift();
        Task<ChangeOfDay> SelectionOfEmployeesToShift(DateTime date);
        Task<IEnumerable<CarWashWorkersBll>> ListOfEmployeesForService(int ServisesId);
        Task TestBonus1(int id);
    }
}
