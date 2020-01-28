using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.BLL.Services.Modules.EmployeeSalary;
using CarDetailingStudio.DAL.Utilities.Ninject;
using Ninject;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    public static class NinjectDependencyResolverBll
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IOrderServicesCarWashServices>().To<OrderServicesCarWashServices>();
            kernel.Bind<IServisesCarWashOrderServices>().To<ServisesCarWashOrderServices>();
            kernel.Bind<IBrigadeForTodayServices>().To<BrigadeForTodayServices>();
            kernel.Bind<IDetailingsServises>().To<DetailingsServises>();
            kernel.Bind<ICarWashWorkersServices>().To<CarWashWorkersServices>();
            kernel.Bind<IClientsOfCarWashServices>().To<ClientsOfCarWashServices>();
            kernel.Bind<IOrderCarWashWorkersServices>().To<OrderCarWashWorkersServices>();
            kernel.Bind<IOrderServices>().To<OrderServices>();
            kernel.Bind<ICarModelServices>().To<CarModelServices>();
            kernel.Bind<ICarMarkServices>().To<CarMarkServices>();
            kernel.Bind<ICarBodyServices>().To<CarBodyServices>();
            kernel.Bind<IExchangeRatesServices>().To<ExchangeRatesServices>();
            kernel.Bind<IApiPrivatBank>().To<ApiPrivatBank>();
            kernel.Bind<IJobTitleTableServices>().To<JobTitleTableServices>();
            kernel.Bind<IGroupWashServices>().To<GroupWash_Services>();
            kernel.Bind<IClientsGroupsServices>().To<ClientsGroupsServices>();
            kernel.Bind<IClientModules>().To<ClientModules>();
            kernel.Bind<IClientInfoServices>().To<ClientInfoServices>();
            kernel.Bind<IOrderInfoViewServices>().To<OrderInfoViewServices>();
            kernel.Bind<ISalaryModules>().To<SalaryModules>();
            kernel.Bind<ICostServices>().To<CostServices>();
            kernel.Bind<ICreditServices>().To<CreditServices>();

            NinjectDependencyResolverDAL.Initialize(kernel);
        }
    }
}
