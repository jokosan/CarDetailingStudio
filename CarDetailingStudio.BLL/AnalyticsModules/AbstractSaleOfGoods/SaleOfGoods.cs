using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Trade.TradeContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractSaleOfGoods
{
    class SaleOfGoods : ISaleOfGoods
    {
        private readonly IGoodsSold _goodsSold;

        public SaleOfGoods(IGoodsSold goodsSold)
        {
            _goodsSold = goodsSold;
        }

        public async Task<IEnumerable<GoodsSoldBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final) 
            => await _goodsSold.Reports(date, final);       

        public async Task<IEnumerable<GoodsSoldBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final, int paymentState)
        {
            var selectGoodsSold = await _goodsSold.Reports(date, final);
            return selectGoodsSold.Where(x => x.PaymentState == paymentState);
        }

        public async Task<IEnumerable<GoodsSoldBll>> SaleOfGoodsPerDay(DateTime date)
            => await _goodsSold.Reports(date);

        public async Task<IEnumerable<GoodsSoldBll>> SaleOfGoodsPerDay(DateTime date, int paymentState)
        {
            var selectGoodsSold = await _goodsSold.Reports(date);
            return selectGoodsSold.Where(x => x.PaymentState == paymentState);
        }

        public SaleOfGoodsModels AnalyticsFormationSaleOfGoods(IEnumerable<GoodsSoldBll> saleOfGoods)
        {
            SaleOfGoodsModels models = new SaleOfGoodsModels();

            models.CauntGoodsSold = saleOfGoods.Sum(s => s.amount).Value;
            models.SumGoodsSold = saleOfGoods.Sum(s => s.orderPrice).Value;
            models.SumWagesAdminstrator = saleOfGoods.Sum(s => s.percentageOfSale).Value;

            return models;
        }

        public IEnumerable<GoodsSoldBll> GoodsSoldGroup(IEnumerable<GoodsSoldBll> goodsSolds)
        {
            return goodsSolds.GroupBy(x => new { x.listOfGoodsId.Value, x.Date.Value.Date })
                             .Select(y => new GoodsSoldBll
                             { 
                                listOfGoods = y.First().listOfGoods,
                                Date = y.First().Date.Value.Date,
                                amount = y.Sum(s => s.amount),
                                orderPrice = y.Sum(s => s.orderPrice),
                                PaymentState = y.First().PaymentState
                             });
        }

        public List<IncomeModel> GoodsSoldIncome(IEnumerable<GoodsSoldBll> goodsSolds)
        {
            var financServises = new List<IncomeModel>();
            financServises.Add(new IncomeModel
            {
                ServicesOfInvome = "Товары",
                DateOfIncome = DateTime.Now.Date,
                SumOfIncome = Math.Round(goodsSolds.Sum(s => s.orderPrice).Value, 1),
                SumNoCash = Math.Round(goodsSolds.Where(x => x.PaymentState == (int)PaymentMethod.nonСash).Sum(s => s.orderPrice).Value, 1),
                SumCash = Math.Round(goodsSolds.Where(x => x.PaymentState == (int)PaymentMethod.cash).Sum(s => s.orderPrice).Value, 1)
            });

            return financServises;
        }
            
    }
}
