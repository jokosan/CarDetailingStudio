using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class ExchangeRatesServices : IExchangeRatesServices
    {
        private IUnitOfWork _unitOfWork;
        //  private AutomapperConfig _automapper;

        public ExchangeRatesServices()
        {
            _unitOfWork = new UnitOfWork();
            // _automapper = new AutomapperConfig();
        }

        public async Task Insert(List<ExchangeRatesBll> exchangeRates)
        {
            var Add = Mapper.Map<IEnumerable<ExchangeRatesBll>, IEnumerable<ExchangeRates>>(exchangeRates);
            _unitOfWork.ExchangeRatesUnitOfWork.Insert(Add.ToList());
            await _unitOfWork.Save();
        }

        public async Task UpdateTable()
        {
            var ResultCount = await GetAll();

            if (ResultCount.Count() > 0)
            {
                int z = 0;

                foreach (var x in ResultCount)
                {
                    x.ccy = ApiPrivatBank.exchangeRatesModel[z].ccy;
                    x.base_ccy = ApiPrivatBank.exchangeRatesModel[z].base_ccy;
                    x.buy = ApiPrivatBank.exchangeRatesModel[z].buy;
                    x.sale = ApiPrivatBank.exchangeRatesModel[z].sale;
                    x.Date = ApiPrivatBank.exchangeRatesModel[z].Date;

                    ExchangeRates exchangeRates = Mapper.Map<ExchangeRatesBll, ExchangeRates>(x);
                    _unitOfWork.ExchangeRatesUnitOfWork.Update(exchangeRates);
                    await _unitOfWork.Save();

                    z++;
                }
            }
            else
            {
                await Insert(ApiPrivatBank.exchangeRatesModel);
            }
        }

        public async Task<IEnumerable<ExchangeRatesBll>> GetAll()
        {
            return Mapper.Map<IEnumerable<ExchangeRatesBll>>(await _unitOfWork.ExchangeRatesUnitOfWork.Get());
        }

        public async Task<bool> CheckForUpdate()
        {
            var check = await GetAll();
            return check.Any(x => (x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")));
        }

        public async Task UpdateListExchangeRates()
        {
            var GetExchangeRates = await GetAll();

            ApiPrivatBank.exchangeRatesModel.Clear();
            ApiPrivatBank.exchangeRatesModel.AddRange(GetExchangeRates);
        }
    }
}
