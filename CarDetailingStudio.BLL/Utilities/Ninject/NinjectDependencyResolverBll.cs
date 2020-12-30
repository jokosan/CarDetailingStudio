using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.Checkout;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.ExpensesServices;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.BLL.Services.JoinModel;
using CarDetailingStudio.BLL.Services.JoinModel.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.BLL.Services.Modules.Clients;
using CarDetailingStudio.BLL.Services.Modules.Clients.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using CarDetailingStudio.BLL.Services.Modules.Contract;
using CarDetailingStudio.BLL.Services.Modules.EmployeeRate;
using CarDetailingStudio.BLL.Services.Modules.TireStorage;
using CarDetailingStudio.BLL.Services.Modules.TireStorage.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using CarDetailingStudio.BLL.Services.TireFitting;
using CarDetailingStudio.BLL.Services.TireFitting.Module;
using CarDetailingStudio.BLL.Services.TireFitting.Module.Contract;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using CarDetailingStudio.BLL.Services.TireStorageServices;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.DAL.Utilities.Ninject;
using Ninject;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    public static class NinjectDependencyResolverBll
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IBrigadeForTodayServices>().To<BrigadeForTodayServices>();
            kernel.Bind<IDetailingsServises>().To<DetailingsServises>();
            kernel.Bind<ICarWashWorkersServices>().To<CarWashWorkersServices>();
            kernel.Bind<IClientsOfCarWashServices>().To<ClientsOfCarWashServices>();
            kernel.Bind<ICarModelServices>().To<CarModelServices>();
            kernel.Bind<ICarMarkServices>().To<CarMarkServices>();
            kernel.Bind<ICarBodyServices>().To<CarBodyServices>();
            kernel.Bind<IExchangeRatesServices>().To<ExchangeRatesServices>();
            kernel.Bind<IApiPrivatBank>().To<ApiPrivatBank>();
            kernel.Bind<IJobTitleTableServices>().To<JobTitleTableServices>();
            kernel.Bind<IGroupWashServices>().To<GroupWash_Services>();
            kernel.Bind<IClientsGroupsServices>().To<ClientsGroupsServices>();
            kernel.Bind<IClientModules>().To<ClientModules>();
            kernel.Bind<IClientInfoServices>().To<ClientInfoServices>();
            kernel.Bind<ICreditServices>().To<CreditServices>();
            kernel.Bind<IWageModules>().To<WageModules>();
            kernel.Bind<ICloseShiftModule>().To<CloseShiftModule>();
            kernel.Bind<IDayResult>().To<DayResult>();
            kernel.Bind<IWagesForDaysWorkedGroup>().To<WagesForDaysWorkedGroup>();
            kernel.Bind<IStatusOrder>().To<StatusOrderServices>();
            kernel.Bind<IOrderCarpetWashingServices>().To<OrderCarpetWashingServices>();
            kernel.Bind<ISalaryBalanceService>().To<SalaryBalanceService>();
            kernel.Bind<IPaymentState>().To<PaymentStateServices>();

            // TireStorage - хранение шин
            kernel.Bind<ITireStorage>().To<TireStorageServices>();
            kernel.Bind<ITireStorageServices>().To<TireStorageSerServices>();
            kernel.Bind<IStorageFee>().To<StorageFeeServices>();
            kernel.Bind<IReviwOrderModules>().To<ReviwOrderModules>();
            kernel.Bind<IAdditionalTireStorageServices>().To<AdditionalTireStorageServices>();

            // Join
            kernel.Bind<IClientJoinOrderCarpetWashing>().To<ClientJoinOrderCarpetWashing>();
            kernel.Bind<ICarJoinClientServices>().To<CarJoinClientServices>();

            //bonus
            kernel.Bind<IBonusToSalary>().To<BonusToSalaryServices>();
            kernel.Bind<IBonusModules>().To<BonusModules>();

            kernel.Bind<IRemoveClient>().To<RemoveClient>();
            kernel.Bind<IAllExpenses>().To<AllExpensesModulescs>();
            kernel.Bind<IUtilityCostsCategory>().To<UtilityCostsCategoryServices>();
            kernel.Bind<ICashier>().To<CashierServices>();
            kernel.Bind<IIncomeForTheCurrentDay>().To<IncomeForTheCurrentDay>();
            kernel.Bind<ICostCategories>().To<CostCategoriesServices>();
            kernel.Bind<ITireService>().To<TireService>();
            kernel.Bind<ITireChangeService>().To<TireChangeService>();
            kernel.Bind<IPriceListTireFitting>().To<PriceListTireFittingServices>();
            kernel.Bind<IPriceTireFittingAdditionalServices>().To<PriceTireFittingAdditionalServices>();
            kernel.Bind<ITireRadius>().To<TireRadiusServices>();
            kernel.Bind<ITypeOfCars>().To<TypeOfCarsServices>();

            kernel.Bind<ICreateOrderModule>().To<CreateOrderModule>();
            kernel.Bind<ISalaryArchive>().To<SalaryArchiveServices>();

            // Временное решение 
            kernel.Bind<IAdditionalIncome>().To<AdditionalIncomeServices>();

            NinjectEmployeeRate.Initialize(kernel);
            NinjectExpenses.Initialize(kernel);
            NinjectOrder.Initialize(kernel);
            NinjectDependencyResolverDAL.Initialize(kernel);
            NinjectTrade.Initialize(kernel);
        }
    }
}
