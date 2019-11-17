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
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services
{
    public class DetailingsServises : IDetailingsServises
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private IApiPrivatBank _apiPrivatBank;

        public DetailingsServises(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig, IApiPrivatBank apiPrivatBank)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapperConfig;
            _apiPrivatBank = apiPrivatBank;
        }

        public IEnumerable<DetailingsBll> GetAll()
        {
            return Mapper.Map<IEnumerable<DetailingsBll>>(_unitOfWork.DetailingsUnitOfWork.Get());
        }

        public DetailingsBll GetId(int? id)
        {
            return Mapper.Map<DetailingsBll>(_unitOfWork.DetailingsUnitOfWork.GetById(id));
        }

        public void AddNewServices(DetailingsBll prive)
        {
            Detailings detailings = Mapper.Map<DetailingsBll, Detailings>(prive);
            _unitOfWork.DetailingsUnitOfWork.Insert(detailings);
            _unitOfWork.Save();
        }

        public void UpdateServices(DetailingsBll updateServices)
        {
            Detailings detailings = Mapper.Map<DetailingsBll, Detailings>(updateServices);
            _unitOfWork.DetailingsUnitOfWork.Update(detailings);
            _unitOfWork.Save();
        }

        public IEnumerable<DetailingsBll> Converter()
        {
            var All = GetAll();

            var usdTrue = All.Any(us => us.currency == "us");

            if (usdTrue)
            {
                // var ApiCurrency = ApiPrivatBank.exchangeRatesModel.Where(x => x.ccy == "USD").Single();

                var ApiCurrency = SourceOfChoice();

                List<DetailingsBll> detailings = new List<DetailingsBll>();

                foreach (var currencyUsd in All.Where(us => us.currency == "us"))
                {
                    detailings.Add(new DetailingsBll
                    {
                        Id = currencyUsd.Id,
                        services_list = currencyUsd.services_list,
                        validity = currencyUsd.validity,
                        note = currencyUsd.note,

                        S = ConvertCurrency(currencyUsd.S, ApiCurrency.buy.Value),
                        M = ConvertCurrency(currencyUsd.S, ApiCurrency.buy.Value),
                        L = ConvertCurrency(currencyUsd.S, ApiCurrency.buy.Value),
                        XL = ConvertCurrency(currencyUsd.S, ApiCurrency.buy.Value),
                        group = currencyUsd.group,
                        status = currencyUsd.status,
                        currency = currencyUsd.currency,
                        mark = currencyUsd.mark,
                        IdGroupWashServices = currencyUsd.IdGroupWashServices,
                        IdTypeService = currencyUsd.IdTypeService
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

        public ExchangeRatesBll SourceOfChoice()
        {        

            if (ApiPrivatBank.exchangeRatesModel.Count != 0)
            {
                return ApiPrivatBank.exchangeRatesModel.Where(x => x.ccy == "USD").Single();
            }
            else
            {
                var Result = Mapper.Map<IEnumerable<ExchangeRatesBll>>(_unitOfWork.ExchangeRatesUnitOfWork.GetWhere(x => x.ccy == "USD"));

                return Result.Single();
            }         
            
        }

        public double? ConvertCurrency(double? usd, double privat) => usd * privat;

    }
}
