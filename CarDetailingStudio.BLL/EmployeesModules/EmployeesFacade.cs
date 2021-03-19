using CarDetailingStudio.BLL.EmployeesModules.Contract;
using CarDetailingStudio.BLL.EmployeesModules.Model;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.EmployeesModules
{
    public class EmployeesFacade : IEmployeesFacade
    {
        private readonly IStaffShift _staffShift;
        private readonly IAccrualOfPremium _accrualOfPremium;

        public EmployeesFacade(
            IStaffShift staffShift,
            IAccrualOfPremium accrualOfPremium)
        {
            _staffShift = staffShift;
            _accrualOfPremium = accrualOfPremium;
        }

        public async Task<ChangeOfDay> SelectionOfEmployeesToShift() => 
            await _staffShift.NewShifts();

        public async Task<ChangeOfDay> SelectionOfEmployeesToShift(DateTime date) => 
            await _staffShift.NewShifts(date);

        public async Task<IEnumerable<CarWashWorkersBll>> ListOfEmployeesForService(int ServisesId) =>
            await _staffShift.EmployeeToPerformServices(ServisesId);

        public async Task TestBonus1(int id) =>
            await _accrualOfPremium.CloseShift(id);
            
    }
}
