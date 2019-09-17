using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using CarDetailingStudio.DataBase.db;
using CarDetailingStudio.BLL.Services.IServices;
using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.ServiceLogic;

namespace CarDetailingStudio.Utilities.Ninject
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
            kernel.Bind<IServices<CarWashWorkers>>().To<CarWashWorkersServices>();
            kernel.Bind<CarWashWorkersServices>().To<CarWashWorkersServices>();
            kernel.Bind<BrigadeForTodayServices>().To<BrigadeForTodayServices>();
            kernel.Bind<FormationOfTheCurrentShift>().To<FormationOfTheCurrentShift>();
        }
    }
}