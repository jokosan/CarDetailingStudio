using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.JoinModel.Model;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;

namespace CarDetailingStudio.Utilities.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderServicesCarWashView, OrderServicesCarWashBll>().ReverseMap();

            CreateMap<CarWashWorkersView, CarWashWorkersBll>().ReverseMap();

            CreateMap<ClientsOfCarWashView, ClientsOfCarWashBll>().ReverseMap();

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

            CreateMap<SalaryBalanceView, SalaryBalanceBll>();
            CreateMap<SalaryBalanceBll, SalaryBalanceView>();

            CreateMap<OrderCarpetWashingView, OrderCarpetWashingBll>();
            CreateMap<OrderCarpetWashingBll, OrderCarpetWashingView>();

            // expenses - затраты

            CreateMap<ExpenseCategoryView, ExpenseCategoryBll>();
            CreateMap<ExpenseCategoryBll, ExpenseCategoryView>();

            CreateMap<SalaryExpensesView, SalaryExpensesBll>();
            CreateMap<SalaryExpensesBll, SalaryExpensesView>();

            CreateMap<UtilityCostsView, UtilityCostsBll>();
            CreateMap<UtilityCostsBll, UtilityCostsView>();

            CreateMap<ExpensesView, ExpensesBll>();
            CreateMap<ExpensesBll, ExpensesView>();

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

            CreateMap<CarJoinClientModel, CarJoinClientModelBll>();
            CreateMap<CarJoinClientModelBll, CarJoinClientModel>();

            CreateMap<ClientJoinCarpetWashingModelView, ClientJoinCarpetWashingModel>();

            CreateMap<SalaryBalanceView, SalaryBalanceBll>();
            CreateMap<SalaryBalanceBll, SalaryBalanceView>();

            CreateMap<OrderCarWashWorkersDayGroupView, OrderCarWashWorkersDayGroupBll>();
            CreateMap<OrderCarWashWorkersDayGroupBll, OrderCarWashWorkersDayGroupView>();


            CreateMap<UtilityCostsCategoryView, UtilityCostsCategoryBll>();
            CreateMap<UtilityCostsCategoryBll, UtilityCostsCategoryView>();


            CreateMap<CostCategoriesView, CostCategoriesBll>();
            CreateMap<CostCategoriesBll, CostCategoriesView>();

            //  шиномонтаж
            CreateMap<PriceListTireFittingAdditionalServicesView, PriceListTireFittingAdditionalServicesBll>();
            CreateMap<PriceListTireFittingAdditionalServicesBll, PriceListTireFittingAdditionalServicesView>();

            CreateMap<TireServiceView, TireServiceBll>();
            CreateMap<TireServiceBll, TireServiceView>();

            CreateMap<PriceListTireFittingView, PriceListTireFittingBll>();
            CreateMap<PriceListTireFittingBll, PriceListTireFittingView>();

            CreateMap<TireRadiusView, TireRadiusBll>();
            CreateMap<TireRadiusBll, TireRadiusView>();

            CreateMap<TypeOfCarsView, TypeOfCarsBll>();
            CreateMap<TypeOfCarsBll, TypeOfCarsView>();

            CreateMap<TireChangeServiceView, TireChangeServiceBll>();
            CreateMap<TireChangeServiceBll, TireChangeServiceView>();

            CreateMap<GoodsSoldView, ListOfGoodsBll>().ReverseMap();
            CreateMap<ListOfGoodsView, ListOfGoodsBll>().ReverseMap();
            CreateMap<ProcurementView, ProcurementBll>().ReverseMap();
            CreateMap<ProductCategoriesView, ProductCategoriesBll>().ReverseMap();
            CreateMap<EmployeeRateView, EmployeeRateBll>().ReverseMap();

            CreateMap<PremiumAndRateView, PremiumAndRateBll>().ReverseMap();
            CreateMap<PositionView, PositionBll>().ReverseMap();
        }
    }
}