using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules
{
    interface IAdditionalIncome
    {
        Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsPerDay(DateTime date);
        Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final);

        Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsPerDay(DateTime date, int paymentState);
        Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final, int paymentState);
        AdditionalIncomeModels AnalyticsFormationAdditionalIncome(IEnumerable<AdditionalIncomeBll> additionalIncomes);
    }
}
