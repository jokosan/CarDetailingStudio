using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL;
using CarDetailingStudio.BLL.Services.Modules;

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

        public void Insert(List<ExchangeRatesBll> exchangeRates)
        {
            var Add = Mapper.Map<IEnumerable<ExchangeRatesBll>, IEnumerable<ExchangeRates>>(exchangeRates);
            _unitOfWork.ExchangeRatesUnitOfWork.Insert(Add.ToList());
            _unitOfWork.Save();
        }

        public void UpdateTable()
        {
            var ResultCount = GetAll();

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
                    _unitOfWork.Save();

                    z++;
                }
            }
            else
            {
                Insert(ApiPrivatBank.exchangeRatesModel);
            }
        }

        public IEnumerable<ExchangeRatesBll> GetAll()
        {
            return Mapper.Map<IEnumerable<ExchangeRatesBll>>(_unitOfWork.ExchangeRatesUnitOfWork.Get());
        }

        public bool CheckForUpdate()
        {
            var check = GetAll();
            return check.Any(x => (x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")));
        }

        public void UpdateListExchangeRates()
        {
            var GetExchangeRates = GetAll();

            ApiPrivatBank.exchangeRatesModel.Clear();
            ApiPrivatBank.exchangeRatesModel.AddRange(GetExchangeRates);
        }
    }
}
