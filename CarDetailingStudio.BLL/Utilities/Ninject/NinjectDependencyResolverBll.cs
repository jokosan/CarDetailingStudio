using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.Checkout;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Expenses;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.BLL.Services.JoinModel;
using CarDetailingStudio.BLL.Services.JoinModel.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.BLL.Services.Modules.CloseShift;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using CarDetailingStudio.BLL.Services.Modules.TireStorage;
using CarDetailingStudio.BLL.Services.Modules.TireStorage.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
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

            // Order

            kernel.Bind<IOrderServicesCarWashServices>().To<OrderServicesCarWashServices>();
            kernel.Bind<IServisesCarWashOrderServices>().To<ServisesCarWashOrderServices>();
            kernel.Bind<IOrderCarWashWorkersServices>().To<OrderCarWashWorkersServices>();
            kernel.Bind<IOrderServices>().To<OrderServices>();
            kernel.Bind<IOrder>().To<Order>();

            // expenses - затраты

            kernel.Bind<IConsumablesTireFitting>().To<ConsumablesTireFittingServices>();
            kernel.Bind<ICostsCarWashAndDeteyling>().To<CostsCarWashAndDeteylingServices>();
            kernel.Bind<IOtherExpenses>().To<OtherExpensesServises>();
            kernel.Bind<ISalaryExpenses>().To<SalaryExpensesServices>();
            kernel.Bind<IUtilityCosts>().To<UtilityCostsServices>();
            kernel.Bind<IExpenseCategory>().To<ExpenseCategoryServices>();

            // TireStorage - хранение шин

            kernel.Bind<ITireStorage>().To<TireStorageServices>();
            kernel.Bind<ITireStorageServices>().To<TireStorageSerServices>();
            kernel.Bind<IStorageFee>().To<StorageFeeServices>();
            kernel.Bind<IReviwOrderModules>().To<ReviwOrderModules>();

            // Join
            kernel.Bind<IClientJoinOrderCarpetWashing>().To<ClientJoinOrderCarpetWashing>();

            NinjectDependencyResolverDAL.Initialize(kernel);
        }
    }
}
