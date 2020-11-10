using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.Checkout;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    public class NinjectOrder
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IOrderServicesCarWashServices>().To<OrderServicesCarWashServices>();
            kernel.Bind<IServisesCarWashOrderServices>().To<ServisesCarWashOrderServices>();
            kernel.Bind<IOrderCarWashWorkersServices>().To<OrderCarWashWorkersServices>();
            kernel.Bind<IOrderServices>().To<OrderServices>();
            kernel.Bind<IOrder>().To<Order>();
        }
    }
}
