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

            CreateMap<CarWashWorkersBll, CarWashWorkers>();
            CreateMap<CarWashWorkers, CarWashWorkersBll>();

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

            CreateMap<CarModelBll, car_model>()
                .ForMember(t => t.car_generation, opt => opt.Ignore());
            CreateMap<car_model, CarModelBll>()
                .ForMember(t => t.car_generation, opt => opt.Ignore());

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
        }
    }
}
