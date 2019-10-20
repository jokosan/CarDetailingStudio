﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.Models.ModelViews;

namespace CarDetailingStudio.Utilities.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderServicesCarWashView, OrderServicesCarWashBll>();
            CreateMap<OrderServicesCarWashBll, OrderServicesCarWashView>();

            CreateMap<CarWashWorkersView, CarWashWorkersBll>();
            CreateMap<CarWashWorkersBll, CarWashWorkersView>();

            CreateMap<ClientsOfCarWashView, ClientsOfCarWashBll>();
            CreateMap<ClientsOfCarWashBll, ClientsOfCarWashView>();

            CreateMap<JobTitleTableView, JobTitleTableBll>();
            CreateMap<JobTitleTableBll, JobTitleTableView>();

            CreateMap<CarBodyView, CarBodyBll>();
            CreateMap<CarBodyBll, CarBodyView>();

            CreateMap<ClientsGroupsView, ClientsGroupsBll>();
            CreateMap<ClientsGroupsBll, ClientsGroupsView>();

            CreateMap<BrigadeForTodayView, BrigadeForTodayBll>();
            CreateMap<BrigadeForTodayBll, BrigadeForTodayView>();

            CreateMap<CarGenerationView, CarGenerationBll>();
            CreateMap<CarGenerationBll, CarGenerationView>();

            CreateMap<CarMarkView, CarMarkBll>();
            CreateMap<CarMarkBll, CarMarkView>();

            CreateMap<CarModelView, CarModelBll>()
                .ForMember(t => t.car_generation, opt => opt.Ignore());
            CreateMap<CarModelBll, CarModelView>()
                .ForMember(t => t.car_generation, opt => opt.Ignore());

            CreateMap<CarModificationView, CarModificationBll>();
            CreateMap<CarModificationBll, CarModificationView>();

            CreateMap<CarSerieView, CarSerieBll>();
            CreateMap<CarSerieBll, CarSerieView>();

            CreateMap<StatusOrderView, StatusOrderBll>();
            CreateMap<StatusOrderBll, StatusOrderView>();

            CreateMap<PaymentStateView, PaymentStateBll>();
            CreateMap<PaymentStateBll, PaymentStateView>();

            CreateMap<ServisesCarWashOrderView, ServisesCarWashOrderBll>();
            CreateMap<ServisesCarWashOrderBll, ServisesCarWashOrderView>();

            CreateMap<OrderInfoViewModel, OrderInfoViewBll>();
            CreateMap<OrderInfoViewBll, OrderInfoViewModel>();

            CreateMap<OrderCarWashWorkersView, OrderCarWashWorkersBll>();
            CreateMap<OrderCarWashWorkersBll, OrderCarWashWorkersView>();

            CreateMap<CostsView, CostsBll>();
            CreateMap<CostsBll, CostsView>();

            CreateMap<TypeOfCostsView, TypeOfCostsBll>();
            CreateMap<TypeOfCostsBll, TypeOfCostsView>();

            CreateMap<WageView, WageBll>();
            CreateMap<WageBll, WageView>();

            CreateMap<OrderInfoViewModel, OrderInfoViewBll>();
            CreateMap<OrderInfoViewBll, OrderInfoViewModel>();

        }
    }
}