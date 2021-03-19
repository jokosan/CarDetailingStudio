using CarDetailingStudio.BLL.EmployeesModules.Contract;
using Quartz;
using System.Threading.Tasks;

namespace CarDetailingStudio.Utilities.Quartz
{
    public class AutomaticShiftClose : IJob
    {
        private readonly IEmployeesFacade _employeesFacade;

        public AutomaticShiftClose(IEmployeesFacade employeesFacade)
        {
            _employeesFacade = employeesFacade;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _employeesFacade.TestBonus1(2);
        }
    }
}