using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.Contract;
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
        private readonly ITypeOfOrderServices _typeOfOrderServices;

        public AdditionalIncome(
           Services.Contract.IAdditionalIncome additionalIncome,
           ITypeOfOrderServices typeOfOrderServices)
        {
            _additionalIncome = additionalIncome;
            _typeOfOrderServices = typeOfOrderServices;
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final) => await _additionalIncome.Reports(date, final);
        

        public async Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsForTheSelectedPeriod(DateTime date, DateTime final, int paymentState)
        {
            var selectAdditionalIncome = await _additionalIncome.Reports(date, final);
            return selectAdditionalIncome.Where(x => x.PaymentState == paymentState);
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsPerDay(DateTime date)
            => await _additionalIncome.Reports(date);

        public async Task<IEnumerable<AdditionalIncomeBll>> SaleOfGoodsPerDay(DateTime date, int paymentState)
        {
            var selectAdditionalIncome = await _additionalIncome.Reports(date);
            return selectAdditionalIncome.Where(x => x.PaymentState == paymentState);
        }

        public AdditionalIncomeModels AnalyticsFormationAdditionalIncome(IEnumerable<AdditionalIncomeBll> additionalIncomes)
        {
            AdditionalIncomeModels model = new AdditionalIncomeModels();

            model.CashierAvtomir = Math.Round(additionalIncomes.Where(x => x.IncomeCategory == "Касса автомир").Sum(s => s.Amount).Value, 1);
            model.CashierCaravan = Math.Round(additionalIncomes.Where(x => x.IncomeCategory == "Касса автодом").Sum(s => s.Amount).Value, 1);
            model.CashierDryCleaningKohler = Math.Round(additionalIncomes.Where(x => x.IncomeCategory == "Касса химчистка Колер").Sum(s => s.Amount).Value, 1);

            return model;
        }

        public List<IncomeModel> FormationAdditionalIncome(IEnumerable<AdditionalIncomeBll> additionalIncomes)
        {
            string[] IncomeCategory = new string[] { "Касса автомир ", "Касса автодом", "Касса химчистка Колер" };           

            var financServises = new List<IncomeModel>();

            foreach (var item in IncomeCategory)
            {
                financServises.Add(new IncomeModel
                {
                    ServicesOfInvome = item,
                    IdIncome = TypeServices(item),
                    DateOfIncome = DateTime.Now.Date,
                    CountServices = additionalIncomes.Where(x => x.IncomeCategory == item).Count(),
                    SumOfIncome = Math.Round(additionalIncomes.Where(x => x.IncomeCategory == item).Sum(s => s.Amount).Value, 1),
                    SumNoCash = Math.Round(additionalIncomes.Where(x => x.PaymentState == (int)PaymentMethod.nonСash && x.IncomeCategory == item).Sum(s => s.Amount).Value, 1),
                    SumCash = Math.Round(additionalIncomes.Where(x => x.PaymentState == (int)PaymentMethod.cash && x.IncomeCategory == item).Sum(s => s.Amount).Value, 1)
                });
            }

            return financServises;
        }

        private int TypeServices(string name)
        {
            var typeServices = Task.Run<IEnumerable<TypeOfOrderBll>>(async () => await _typeOfOrderServices.GetTableAll());
            var typeServiesResult = typeServices.Result;
            var idTypeServises =  typeServiesResult.FirstOrDefault(x => x.nameOrder.Contains(name.Trim()));
            return idTypeServises.IdTypeOfOrder; 
        }
    }
}
