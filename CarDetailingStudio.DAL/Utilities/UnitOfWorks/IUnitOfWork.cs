using CarDetailingStudio.DAL.Infrastructure;
using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Utilities.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Dispose();

        void Save();

        //  GenericRepository

        DbRepository<ClientsOfCarWash> ClientsOfCarWashUnitOfWork { get; set; }
        DbRepository<brigadeForToday> BrigadeForTodayUnitOfWork { get; set; }
        DbRepository<CarWashWorkers> CarWashWorkersUnitOfWork { get; set; }
        DbRepository<OrderServicesCarWash> OrderServicesCarWashUnitOfWork { get; set; }
        DbRepository<JobTitleTable> JobTitleTableUnitOfWork { get; set; }
        DbRepository<ServisesCarWashOrder> ServisesCarWashOrderUnitOfWork { get; set; }
        DbRepository<Detailings> DetailingsUnitOfWork { get; set; }
        DbRepository<OrderCarWashWorkers> OrderCarWasWorkersUnitOFWork { get; set; }
        DbRepository<car_mark> CarMarkUnitOfWork { get; set; }
        DbRepository<car_model> CarModelUnitOfWork { get; set; }
        DbRepository<CarBody> CarBodyUnitOfWork { get; set; }
        DbRepository<infoBrigadeForToday> infoBrigadeUnitOfWork { get; set; }
        DbRepository<ExchangeRates> ExchangeRatesUnitOfWork { get; set; }
        DbRepository<GroupWashServices> GroupWashServicesUnitOfWork { get; set; }
        DbRepository<ClientsGroups> ClientsGroupsUnitOfWork { get; set; }
        DbRepository<ClientInfo> ClientInfoUnitOfWork { get; set; }
        DbRepository<Credit> CreditUnitOgWork { get; set; }
        DbRepository<StatusOrder> StatusOrderUnitOfWork { get; set; }
        DbRepository<salaryBalance> SalaruBalanceUnitOfWork { get; set; }
        DbRepository<orderCarpetWashing> OrderCarpetWashingUnitOfWork { get; set; }

        // expenses - затраты

        DbRepository<expenseCategory> expenseCategoryUnitOfWork { get; set; }
        DbRepository<salaryExpenses> salaryExpensesUnitOfWork { get; set; }
        DbRepository<utilityCosts> utilityCostsUnitOfWork { get; set; }
        DbRepository<otherExpenses> otherExpensesUnitOfWork { get; set; }
        DbRepository<costsCarWashAndDeteyling> costsCarWashAndDeteylingUnitOfWork { get; set; }
        DbRepository<consumablesTireFitting> consumablesTireFittingUnitOfWork { get; set; }


        // TireStorage - хранение шин

        DbRepository<TireStorage> tireStorageUnitOfWork { get; set; }
        DbRepository<storageFee> storageFeeUnitOfWork { get; set; }
        DbRepository<TireStorageServices> tireStorageServicesUnitOfWork { get; set; }

        // SingleRepository

        OrderServicesCarWashRepository orderUnitiOfWork { get; set; }
        ClientsOfCarWashRepository ClientsUnitOfWork { get; set; }
        CarWashWorkersRepository WorkersUnitOfWork { get; set; }
        BrigadeForTodayRepository BrigadeUnitOfWork { get; set; }
        ServisesCarWashOrderRepository ServisesUnitOfWork { get; set; }
        OrderInfoViewRepository OrderInfoUnitOfWork { get; set; }
    }

}
