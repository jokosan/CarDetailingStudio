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

            CreateMap<OrderInfoViewBll, ItogOrderView>();
            CreateMap<ItogOrderView, OrderInfoViewBll>();

            CreateMap<CarBodyBll, CarBody>();
            CreateMap<CarBody, CarBodyBll>();

            CreateMap<ExchangeRatesBll, ExchangeRates>();
            CreateMap<ExchangeRates, ExchangeRatesBll>();

            CreateMap<InfoBrigadeForTodayBll, infoBrigadeForToday>();
            CreateMap<infoBrigadeForToday, InfoBrigadeForTodayBll>();

            CreateMap<ClientInfoBll, ClientInfo>();
            CreateMap<ClientInfo, ClientInfoBll>();

            CreateMap<GroupWashServicesBll, GroupWashServices>();
            CreateMap<GroupWashServices, GroupWashServicesBll>();

            CreateMap<CreditBll, Credit>();
            CreateMap<Credit, CreditBll>();

            CreateMap<SalaryBalanceBll, salaryBalance>();
            CreateMap<salaryBalance, SalaryBalanceBll>();

            CreateMap<OrderCarpetWashingBll, orderCarpetWashing>();
            CreateMap<orderCarpetWashing, OrderCarpetWashingBll>();


            // expenses - затраты

            CreateMap<ExpenseCategoryBll, expenseCategory>();
            CreateMap<expenseCategory, ExpenseCategoryBll>();

            CreateMap<SalaryExpensesBll, salaryExpenses>();
            CreateMap<salaryExpenses, SalaryExpensesBll>();

            CreateMap<UtilityCostsBll, utilityCosts>();
            CreateMap<utilityCosts, UtilityCostsBll>();

            CreateMap<ExpensesBll, Expenses>();
            CreateMap<Expenses, ExpensesBll>();

            // TireStorage - хранение шин

            CreateMap<TireStorageBll, TireStorage>();
            CreateMap<TireStorage, TireStorageBll>();

            CreateMap<TireStorageServicesBll, TireStorageServices>();
            CreateMap<TireStorageServices, TireStorageServicesBll>();

            CreateMap<StorageFeeBll, storageFee>();
            CreateMap<storageFee, StorageFeeBll>();

            CreateMap<SalaryBalanceBll, salaryBalance>();
            CreateMap<salaryBalance, SalaryBalanceBll>();


            CreateMap<UtilityCostsCategoryBll, utilityCostsCategory>();
            CreateMap<utilityCostsCategory, UtilityCostsCategoryBll>();

            CreateMap<CostCategoriesBll, costCategories>();
            CreateMap<costCategories, CostCategoriesBll>();

            //  шиномонтаж
            CreateMap<PriceListTireFittingAdditionalServicesBll, PriceListTireFittingAdditionalServices>();
            CreateMap<PriceListTireFittingAdditionalServices, PriceListTireFittingAdditionalServicesBll>();

            CreateMap<TireServiceBll, tireService>();
            CreateMap<tireService, TireServiceBll>();

            CreateMap<PriceListTireFittingBll, PriceListTireFitting>();
            CreateMap<PriceListTireFitting, PriceListTireFittingBll>();

            CreateMap<TireRadiusBll, TireRadius>();
            CreateMap<TireRadius, TireRadiusBll>();

            CreateMap<TypeOfCarsBll, TypeOfCars>();
            CreateMap<TypeOfCars, TypeOfCarsBll>();

            CreateMap<TireChangeServiceBll, tireChangeService>();
            CreateMap<tireChangeService, TireChangeServiceBll>();

            CreateMap<GoodsSoldBll, goodsSold>().ReverseMap();
            CreateMap<ListOfGoodsBll, listOfGoods>().ReverseMap();
            CreateMap<ProductCategoriesBll, ProductCategories>().ReverseMap();
            CreateMap<ProcurementBll, procurement>().ReverseMap();

            CreateMap<EmployeeRateBll, EmployeeRate>().ReverseMap();
        }
    }
}
