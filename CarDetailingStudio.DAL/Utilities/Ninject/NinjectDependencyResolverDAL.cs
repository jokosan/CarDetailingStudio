using CarDetailingStudio.DAL.Infrastructure;
using CarDetailingStudio.DAL.Infrastructure.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using Ninject;

namespace CarDetailingStudio.DAL.Utilities.Ninject
{
    public static class NinjectDependencyResolverDAL
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IServisesCarWashOrderRepository>().To<ServisesCarWashOrderRepository>();
            kernel.Bind<IOrderServicesCarWashRepository>().To<OrderServicesCarWashRepository>();
        }
    }
}
