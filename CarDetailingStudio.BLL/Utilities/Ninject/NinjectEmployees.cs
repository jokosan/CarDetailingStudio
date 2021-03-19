using CarDetailingStudio.BLL.EmployeesModules;
using CarDetailingStudio.BLL.EmployeesModules.Contract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    public class NinjectEmployees
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IEmployeesFacade>().To<EmployeesFacade>();
            kernel.Bind<IStaffShift>().To<StaffShift>();
            kernel.Bind<IAccrualOfPremium>().To<AccrualOfPremiumFacade>();
        }
    }
}
