using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.EmployeeRate;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    public class NinjectEmployeeRate
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IEmployeeRateModules>().To<EmployeeRateModules>();
            kernel.Bind<IEmployeeRate>().To<EmployeeRateServises>();
        }
    }
}
