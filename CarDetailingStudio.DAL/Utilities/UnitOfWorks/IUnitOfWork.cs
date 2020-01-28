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
        DbRepository<Costs> CostsUnitOfWork { get; set; }
        DbRepository<car_mark> CarMarkUnitOfWork { get; set; }
        DbRepository<car_model> CarModelUnitOfWork { get; set; }
        DbRepository<CarBody> CarBodyUnitOfWork { get; set; }
        DbRepository<Wage> WageUnitOfWork { get; set; }
        DbRepository<infoBrigadeForToday> infoBrigadeUnitOfWork { get; set; }
        DbRepository<ExchangeRates> ExchangeRatesUnitOfWork { get; set; }
        DbRepository<GroupWashServices> GroupWashServicesUnitOfWork { get; set; }
        DbRepository<Retail> RatailUnitOfWork { get; set; }
        DbRepository<ClientsGroups> ClientsGroupsUnitOfWork { get; set; }
        DbRepository<ClientInfo> ClientInfoUnitOfWork { get; set; }
        DbRepository<Credit> CreditUnitOgWork { get; set; }
        
        // SingleRepository

        OrderServicesCarWashRepository orderUnitiOfWork { get; set; }
        ClientsOfCarWashRepository ClientsUnitOfWork { get; set; }
        CarWashWorkersRepository WorkersUnitOfWork { get; set; }
        BrigadeForTodayRepository BrigadeUnitOfWork { get; set; }
        ServisesCarWashOrderRepository ServisesUnitOfWork { get; set; }
        OrderInfoViewRepository OrderInfoUnitOfWork { get; set; }
    }

}
