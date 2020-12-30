using CarDetailingStudio.BLL.Services.ExpensesServices;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.Wage;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    public class NinjectExpenses
    {
        // expenses - затраты
        public static void Initialize(IKernel kernel)
        {
            //kernel.Bind<IConsumablesTireFitting>().To<ConsumablesTireFittingServices>();
            //kernel.Bind<ICostsCarWashAndDeteyling>().To<CostsCarWashAndDeteylingServices>();
            //kernel.Bind<IOtherExpenses>().To<OtherExpensesServises>();
            kernel.Bind<IExpenses>().To<ExpensesServices>();
            kernel.Bind<ISalaryExpenses>().To<SalaryExpensesServices>();
            kernel.Bind<IUtilityCosts>().To<UtilityCostsServices>();
            kernel.Bind<IExpenseCategory>().To<ExpenseCategoryServices>();
            kernel.Bind<ITotalMonthlySalaryModules>().To<TotalMonthlySalaryModules>();
        }
    }
}
