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
        DbRepository<OrderServicesCarWash> CarWashWorkersUnitOfWork { get; set; }
        DbRepository<OrderServicesCarWash> OrderServicesCarWashUnitOfWork { get; set; }
        DbRepository<JobTitleTable> JobTitleTableUnitOfWork { get; set; }
        DbRepository<ServisesCarWashOrder> ServisesCarWashOrderUnitOfWork { get; set; }
        DbRepository<Detailings> DetailingsUnitOfWork { get; set; }
        DbRepository<OrderCarWashWorkers> OrderCarWasWorkersUnitOFWork { get; set; }
        DbRepository<Costs> CostsUnitOfWork { get; set; }

        // SingleRepository

        OrderServicesCarWashRepository orderUnitiOfWork { get; set; }
        ClientsOfCarWashRepository ClientsUnitOfWork { get; set; }
        CarWashWorkersRepository WorkersUnitOfWork { get; set; }
        BrigadeForTodayRepository BrigadeUnitOfWork { get; set; }
        ServisesCarWashOrderRepository ServisesUnitOfWork { get; set; }
        OrderInfoViewRepository OrderInfoUnitOfWork { get; set; }
    }

}
