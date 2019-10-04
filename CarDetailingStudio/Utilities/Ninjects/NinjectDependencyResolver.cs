using CarDetailingStudio.BLL.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDetailingStudio.Utilities.Ninjects
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernalParam)
        {
            kernel = kernalParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<OrderServicesCarWashServices>().To<OrderServicesCarWashServices>();
            kernel.Bind<CarWashWorkersServices>().To<CarWashWorkersServices>();
            kernel.Bind<ClientsOfCarWashServices>().To<ClientsOfCarWashServices>();
            kernel.Bind<BrigadeForTodayServices>().To<BrigadeForTodayServices>();
        }
    }
}