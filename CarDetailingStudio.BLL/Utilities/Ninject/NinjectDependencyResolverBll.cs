using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules;
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

            NinjectDependencyResolverDAL.Initialize(kernel);
        }
    }
}
