using CarDetailingStudio.BLL.Utilities.Ninject;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
using CarDetailingStudio.Filters;
using Ninject.Web.WebApi.FilterBindingSyntax;

namespace CarDetailingStudio.Utilities.Ninjects
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernalParam)
        {
            kernel = kernalParam;
            AddBindings();
            InitializeBindings(kernel);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public static void InitializeBindings(IKernel kernel)
        {
            NinjectDependencyResolverBll.Initialize(kernel);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
         
        }
    }
}