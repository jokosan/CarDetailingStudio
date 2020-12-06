using CarDetailingStudio.BLL.Services.Trade;
using CarDetailingStudio.BLL.Services.Trade.TradeContract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    public class NinjectTrade
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IGoodsSold>().To<GoodsSoldServices>();
            kernel.Bind<IListOfGoods>().To<ListOfGoodsServices>();
            kernel.Bind<IProcurement>().To<ProcurementServices>();
            kernel.Bind<IProductCategories>().To<ProductCategoriesServices>();
        }
    }
}
