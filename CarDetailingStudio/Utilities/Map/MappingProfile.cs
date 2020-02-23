using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.Models;
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

            CreateMap<CarModelView, CarModelBll>();
            CreateMap<CarModelBll, CarModelView>();

            CreateMap<CarModificationView, CarModificationBll>();
            CreateMap<CarModificationBll, CarModificationView>();

            CreateMap<CarSerieView, CarSerieBll>();
            CreateMap<CarSerieBll, CarSerieView>();

            CreateMap<StatusOrderView, StatusOrderBll>();
            CreateMap<StatusOrderBll, StatusOrderView>();

            CreateMap<PaymentStateView, PaymentStateBll>();
            CreateMap<PaymentStateBll, PaymentStateView>();

            CreateMap<OrderInfoViewModel, OrderInfoViewBll>();
            CreateMap<OrderInfoViewBll, OrderInfoViewModel>();

            CreateMap<OrderCarWashWorkersView, OrderCarWashWorkersBll>();
            CreateMap<OrderCarWashWorkersBll, OrderCarWashWorkersView>();

            CreateMap<OrderInfoViewModel, OrderInfoViewBll>();
            CreateMap<OrderInfoViewBll, OrderInfoViewModel>();

            CreateMap<CarBodyView, CarBodyBll>();
            CreateMap<CarBodyBll, CarBodyView>();

            CreateMap<InfoBrigadeForTodayView, InfoBrigadeForTodayBll>();
            CreateMap<InfoBrigadeForTodayBll, InfoBrigadeForTodayView>();

            CreateMap<ClientInfoView, ClientInfoBll>();
            CreateMap<ClientInfoBll, ClientInfoView>();

            CreateMap<GroupWashServicesView, GroupWashServicesBll>();
            CreateMap<GroupWashServicesBll, ClientsGroupsView>();
           
            CreateMap<ClientView, ClientViewsBll>();
            CreateMap<ClientViewsBll, ClientView>();

            CreateMap<CreditView, CreditBll>();
            CreateMap<CreditBll, CreditView>();

            CreateMap<WagesForDaysWorkedView, WagesForDaysWorkedBll>();
            CreateMap<WagesForDaysWorkedBll, WagesForDaysWorkedView>();

            CreateMap<DayResultModelView, DayResultModelBll>();
            CreateMap<DayResultModelBll, DayResultModelView>();


            // expenses - затраты

            CreateMap<ExpenseCategoryView, ExpenseCategoryBll>();
            CreateMap<ExpenseCategoryBll, ExpenseCategoryView>();

            CreateMap<SalaryExpensesView, SalaryExpensesBll>();
            CreateMap<SalaryExpensesBll, SalaryExpensesView>();

            CreateMap<UtilityCostsView, UtilityCostsBll>();
            CreateMap<UtilityCostsBll, UtilityCostsView>();

            CreateMap<OtherExpensesView, OtherExpensesBll>();
            CreateMap<OtherExpensesBll, OtherExpensesView>();

            CreateMap<CostsCarWashAndDeteylingView, CostsCarWashAndDeteylingBll>();
            CreateMap<CostsCarWashAndDeteylingBll, CostsCarWashAndDeteylingView>();

            CreateMap<ConsumablesTireFittingView, ConsumablesTireFittingBll>();
            CreateMap<ConsumablesTireFittingBll, ConsumablesTireFittingView>();


            // Tire Storaage - Хранение шин

            CreateMap<TireStorageView, TireStorageBll>();
            CreateMap<TireStorageBll, TireStorageView>();

            CreateMap<TireStorageServicesView, TireStorageServicesBll>();
            CreateMap<TireStorageServicesBll, TireStorageServicesView>();

            CreateMap<StorageFeeView, StorageFeeBll>();
            CreateMap<StorageFeeBll, StorageFeeView>();

            CreateMap<OrderTireStorageModelView, OrderTireStorageModelBll>();
            CreateMap<OrderTireStorageModelBll, OrderTireStorageModelView>();

            CreateMap<ReviwOrderModelView, ReviwOrderModelBll>();
            CreateMap<ReviwOrderModelBll, ReviwOrderModelView>();

        }
    }
}