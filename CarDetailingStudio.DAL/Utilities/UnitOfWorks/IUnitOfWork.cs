using CarDetailingStudio.DAL.Infrastructure;
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

        DbRepository<ClientsOfCarWash> ClientsOfCarWashUnitOfWork { get; set; }
        //DbRepository<ClientsGroups> ClientsGroupsUnitOfWork { get; set; }
        DbRepository<brigadeForToday> BrigadeForTodayUnitOfWork { get; set; }
        DbRepository<CarWashWorkers> CarWashWorkersUnitOfWork { get; set; }
        DbRepository<OrderServicesCarWash> OrderServicesCarWashUnitOfWork { get; set; }
        DbRepository<JobTitleTable> JobTitleTableUnitOfWork { get; set; }
        
        //DbRepository<ServisesCarWashOrder> ServisesCarWashOrderUnitOfWork { get; set; }
        DbRepository<Detailings> DetailingsUnitOfWork { get; set; }
        DbRepository<WashServices> WashServicesUnitOfWork { get; set; }
    }

}
