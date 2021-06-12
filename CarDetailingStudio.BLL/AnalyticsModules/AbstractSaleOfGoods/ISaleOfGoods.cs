using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractSaleOfGoods
{
    interface ISaleOfGoods
    {
        Task<IEnumerable<GoodsSoldBll>> SaleOfGoodsPerDay(DateTime date);
        Task<IEnumerable<GoodsSoldBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final);

        Task<IEnumerable<GoodsSoldBll>> SaleOfGoodsPerDay(DateTime date, int paymentState);
        Task<IEnumerable<GoodsSoldBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final, int paymentState);
        SaleOfGoodsModels AnalyticsFormationSaleOfGoods(IEnumerable<GoodsSoldBll> saleOfGoods);
        IEnumerable<GoodsSoldBll> GoodsSoldGroup(IEnumerable<GoodsSoldBll> goodsSolds);
        List<IncomeModel> GoodsSoldIncome(IEnumerable<GoodsSoldBll> goodsSolds);
    }
}
