using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class DetailingsServises
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private ApiPrivatBank _apiPrivatBank;

        public DetailingsServises()
        {
            _unitOfWork = new UnitOfWork();
            _automapper = new AutomapperConfig();
            _apiPrivatBank = new ApiPrivatBank();
        }

        public IEnumerable<DetailingsBll> GetAll()
        { 
            return Mapper.Map<IEnumerable<DetailingsBll>>(_unitOfWork.DetailingsUnitOfWork.Get());
        }

        public IEnumerable<DetailingsBll> Converter()
        {

            var All = GetAll();

            var usdTrue = All.Any(us => us.currency == "us");

            if (usdTrue)
            {
                _apiPrivatBank.ApiPrivat();
                var ApiCurrency = ExchangeRates.ExchangeRatesList.Where(x => x.ccy == "USD").Single();
              
                List<DetailingsBll> detailings = new List<DetailingsBll>();

                foreach (var currencyUsd in All.Where(us => us.currency == "us"))
                {                   
                    detailings.Add(new DetailingsBll { 
                        Id = currencyUsd.Id,
                        services_list = currencyUsd.services_list,
                        validity = currencyUsd.validity,
                        note = currencyUsd.note,
                        S = ConvertCurrency(currencyUsd.S, ApiCurrency.buy),
                        M = ConvertCurrency(currencyUsd.S, ApiCurrency.buy),
                        L = ConvertCurrency(currencyUsd.S, ApiCurrency.buy),
                        XL = ConvertCurrency(currencyUsd.S, ApiCurrency.buy),
                        group = currencyUsd.group,
                        status = currencyUsd.status,
                        currency = currencyUsd.currency,
                        mark = currencyUsd.mark,
                        IdGroupWashServices = currencyUsd.IdGroupWashServices                        
                    });                   
                }

                var result = All.Where(ua => ua.currency == "ua").Concat(detailings);

                return result;
            }
            else
            {
                return Mapper.Map<IEnumerable<DetailingsBll>>(_unitOfWork.DetailingsUnitOfWork.Get());
            }          
        }

        public double? ConvertCurrency(double? usd, double privat) => usd * privat;
       
    }
}
