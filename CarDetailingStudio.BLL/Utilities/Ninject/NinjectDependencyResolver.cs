using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Infrastructure;
using CarDetailingStudio.DAL.Infrastructure.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    class NinjectDependencyResolver : IDependencyResolver
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
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IGenericRepository<brigadeForToday>>().To<DbRepository<brigadeForToday>>();
          
        }
    }
}
