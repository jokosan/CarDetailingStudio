using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Utilities.Map
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<OrderServicesCarWashBll, OrderServicesCarWash>();
            CreateMap<OrderServicesCarWash, OrderServicesCarWashBll>();

            CreateMap<CarWashWorkersBll, OrderServicesCarWash>();
            CreateMap<OrderServicesCarWash, CarWashWorkersBll>();

            CreateMap<ClientsOfCarWashBll, ClientsOfCarWash>();
            CreateMap<ClientsOfCarWash, ClientsOfCarWashBll>();

            CreateMap<JobTitleTableBll, JobTitleTable>();
            CreateMap<JobTitleTable, JobTitleTableBll>();

            CreateMap<CarBodyBll, CarBody>();
            CreateMap<CarBody, CarBodyBll>();

            CreateMap<ClientsGroupsBll, ClientsGroups>();
            CreateMap<ClientsGroups, ClientsGroupsBll>();

            CreateMap<BrigadeForTodayBll, brigadeForToday>();
            CreateMap<brigadeForToday, BrigadeForTodayBll>();

            CreateMap<CarGenerationBll, car_generation>();
            CreateMap<car_generation, CarGenerationBll>();

            CreateMap<CarMarkBll, car_mark>();
            CreateMap<car_mark, CarMarkBll>();

            CreateMap<CarModelBll, car_model>();
            CreateMap<car_model, CarModelBll>();

            CreateMap<CarModificationBll, car_modification>();
            CreateMap<car_modification, CarModificationBll>();

            CreateMap<CarSerieBll, car_serie>();
            CreateMap<car_serie, CarSerieBll>();

            CreateMap<StatusOrderBll, StatusOrder>();
            CreateMap<StatusOrder, StatusOrderBll>();

            CreateMap<PaymentStateBll, PaymentState>();
            CreateMap<PaymentState, PaymentStateBll>();

            CreateMap<ServisesCarWashOrderBll, ServisesCarWashOrder>();
            CreateMap<ServisesCarWashOrder, ServisesCarWashOrderBll>();

            CreateMap<OrderCarWashWorkersBll, OrderCarWashWorkers>();
            CreateMap<OrderCarWashWorkers, OrderCarWashWorkersBll>();

            CreateMap<CostsBll, Costs>();
            CreateMap<Costs, CostsBll>();

            CreateMap<TypeOfCostsBll, TypeOfCosts>();
            CreateMap<TypeOfCosts, TypeOfCostsBll>();

            CreateMap<WageBll, Wage>();
            CreateMap<Wage, WageBll>();

            CreateMap<OrderInfoViewBll, ItogOrderView>();
            CreateMap<ItogOrderView, OrderInfoViewBll>();

            CreateMap<CarBodyBll, CarBody>();
            CreateMap<CarBody, CarBodyBll>();

            CreateMap<RetailBll, Retail>();
            CreateMap<Retail, RetailBll>();

            CreateMap<PurchaseCostsBll, PurchaseCosts>();
            CreateMap<PurchaseCosts, PurchaseCostsBll>();

            CreateMap<TireStorageBll, TireStorage>();
            CreateMap<TireStorage, TireStorageBll>();

            CreateMap<TireStorageServicesBll, TireStorageServices>();
            CreateMap<TireStorageServices, TireStorageServicesBll>();

            CreateMap<ExchangeRatesBll, ExchangeRates>();
            CreateMap<ExchangeRates, ExchangeRatesBll>();

            CreateMap<InfoBrigadeForTodayBll, infoBrigadeForToday>();
            CreateMap<infoBrigadeForToday, InfoBrigadeForTodayBll>();

            CreateMap<ClientInfoBll, ClientInfo>();
            CreateMap<ClientInfo, ClientInfoBll>();

            CreateMap<GroupWashServicesBll, GroupWashServices>();
            CreateMap<GroupWashServices, GroupWashServicesBll>();

            CreateMap<RetailBll, Retail>();
            CreateMap<Retail, RetailBll>();


            







        }
    }
}
