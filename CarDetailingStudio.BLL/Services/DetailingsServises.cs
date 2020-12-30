using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class DetailingsServises : IDetailingsServises
    {
        private IUnitOfWork _unitOfWork;
        private IApiPrivatBank _apiPrivatBank;

        public DetailingsServises(IUnitOfWork unitOfWork, IApiPrivatBank apiPrivatBank)
        {
            _unitOfWork = unitOfWork;
            _apiPrivatBank = apiPrivatBank;
        }

        public async Task<IEnumerable<DetailingsBll>> GetAll()
            => Mapper.Map<IEnumerable<DetailingsBll>>(await _unitOfWork.DetailingsUnitOfWork.GetInclude("GroupWashServices"));

        public async Task<DetailingsBll> GetId(int? id)
            => Mapper.Map<DetailingsBll>(await _unitOfWork.DetailingsUnitOfWork.GetById(id));

        public async Task<IEnumerable<DetailingsBll>> GetPriceGroupWashServices(int idGroup)
            => Mapper.Map<IEnumerable<DetailingsBll>>(await _unitOfWork.DetailingsUnitOfWork.QueryObjectGraph(x => x.IdGroupWashServices == idGroup));

        public async Task<IEnumerable<DetailingsBll>> SelectTypeService(int idTypeService)
            => Mapper.Map<IEnumerable<DetailingsBll>>(await _unitOfWork.DetailingsUnitOfWork.QueryObjectGraph(x => x.IdTypeService == idTypeService, "GroupWashServices"));

        public async Task<IEnumerable<DetailingsBll>> Converter()
        {
            var All = await GetAll();

            var usdTrue = All.Any(us => us.currency == "us");

            if (usdTrue)
            {
                // var ApiCurrency = ApiPrivatBank.exchangeRatesModel.Where(x => x.ccy == "USD").Single();

                var ApiCurrency = await SourceOfChoice();

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
                        M = ConvertCurrency(currencyUsd.M, ApiCurrency.buy.Value),
                        L = ConvertCurrency(currencyUsd.L, ApiCurrency.buy.Value),
                        XL = ConvertCurrency(currencyUsd.XL, ApiCurrency.buy.Value),
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

        public async Task<ExchangeRatesBll> SourceOfChoice()
        {
            if (ApiPrivatBank.exchangeRatesModel.Count != 0)
            {
                return ApiPrivatBank.exchangeRatesModel.Where(x => x.ccy == "USD").First();
            }
            else
            {
                await _apiPrivatBank.ApiPrivat(); // < --------------- ?????????
                var Result = Mapper.Map<IEnumerable<ExchangeRatesBll>>(await _unitOfWork.ExchangeRatesUnitOfWork.GetWhere(x => x.ccy == "USD"));

                return Result.SingleOrDefault();
            }
        }

        #region Detailings Insert Update
        public async Task AddNewServices(DetailingsBll prive)
        {
            _unitOfWork.DetailingsUnitOfWork.Insert(TransformEntity(prive));
            await _unitOfWork.Save();
        }

        public async Task UpdateServices(DetailingsBll updateServices)
        {
            _unitOfWork.DetailingsUnitOfWork.Update(TransformEntity(updateServices));
            await _unitOfWork.Save();
        }
        #endregion

        public double? ConvertCurrency(double? usd, double privat) => usd * privat;

        private Detailings TransformEntity(DetailingsBll entity) => Mapper.Map<DetailingsBll, Detailings>(entity);

    }
}
