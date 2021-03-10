using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AdditionalIncome
{
    class AdditionalIncome : IAdditionalIncome
    {
        private readonly Services.Contract.IAdditionalIncome _additionalIncome;

        public AdditionalIncome(
           Services.Contract.IAdditionalIncome additionalIncome)
        {
            _additionalIncome = additionalIncome;
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final) => await _additionalIncome.Reports(date, final);
        

        public async Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final, int paymentState)
        {
            var selectAdditionalIncome = await _additionalIncome.Reports(date, final);
            return selectAdditionalIncome.Where(x => x.PaymentState == paymentState);
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsPerDay(DateTime date) => await _additionalIncome.Reports(date);

        public async Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsPerDay(DateTime date, int paymentState)
        {
            var selectAdditionalIncome = await _additionalIncome.Reports(date);
            return selectAdditionalIncome.Where(x => x.PaymentState == paymentState);
        }

        public AdditionalIncomeModels AnalyticsFormationAdditionalIncome(IEnumerable<AdditionalIncomeBll> additionalIncomes)
        {
            AdditionalIncomeModels model = new AdditionalIncomeModels();

            model.CashierAvtomir = additionalIncomes.Where(x => x.IncomeCategory == "Касса автомир").Sum(s => s.Amount).Value;
            model.CashierCaravan = additionalIncomes.Where(x => x.IncomeCategory == "Касса автодом").Sum(s => s.Amount).Value;
            model.CashierDryCleaningKohler = additionalIncomes.Where(x => x.IncomeCategory == "Касса химчистка Колер").Sum(s => s.Amount).Value;

            return model;
        }
    }
}
