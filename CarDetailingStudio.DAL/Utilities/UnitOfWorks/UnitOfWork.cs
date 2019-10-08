using CarDetailingStudio.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Utilities.UnitOfWorks
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private carWashEntities _entities;

        public UnitOfWork()
        {
            _entities = new carWashEntities();
        }

        private DbRepository<ClientsOfCarWash> ClientsOfCarWashUW;
        private DbRepository<ClientsGroups> ClientsGroupsUW;
        private DbRepository<brigadeForToday> BrigadeForTodayUW;
        private DbRepository<CarWashWorkers> CarWashWorkersUW;
        private DbRepository<OrderServicesCarWash> OrderServicesCarWashUW;
        private DbRepository<JobTitleTable> JobTitleTableUW;
        private DbRepository<ServisesCarWashOrder> ServisesCarWashOrderUW;
        private DbRepository<Detailings> DetailingsUW;

        public DbRepository<ServisesCarWashOrder> ServisesCarWashOrderUnitOfWork
        {
            get
            {
                if (ServisesCarWashOrderUW == null)
                {
                    ServisesCarWashOrderUW = new DbRepository<ServisesCarWashOrder>(_entities);                
                }

                return ServisesCarWashOrderUW;
            }

            set { ServisesCarWashOrderUW = value; }
        }

        public DbRepository<OrderServicesCarWash> OrderServicesCarWashUnitOfWork
        {
            get
            {
                if (OrderServicesCarWashUW == null)
                {
                    OrderServicesCarWashUW = new DbRepository<OrderServicesCarWash>(_entities);
                }

                return OrderServicesCarWashUW;
            }

            set { OrderServicesCarWashUW = value; }

        }

        public DbRepository<CarWashWorkers> CarWashWorkersUnitOfWork
        {
            get
            {
                if (CarWashWorkersUW == null)
                {
                    CarWashWorkersUW = new DbRepository<CarWashWorkers>(_entities);
                }

                return CarWashWorkersUW;
            }

            set { CarWashWorkersUW = value; }
        }

        public DbRepository<brigadeForToday> BrigadeForTodayUnitOfWork
        {
            get
            {
                if (BrigadeForTodayUW == null)
                {
                    BrigadeForTodayUW = new DbRepository<brigadeForToday>(_entities);
                }

                return BrigadeForTodayUW;
            }

            set { BrigadeForTodayUW = value; }
        }

        public DbRepository<JobTitleTable> JobTitleTableUnitOfWork
        {
            get
            {
                if (JobTitleTableUW == null)
                {
                    JobTitleTableUW = new DbRepository<JobTitleTable>(_entities);
                }

                return JobTitleTableUW;
            }

            set { JobTitleTableUW = value; }
        }

        public DbRepository<ClientsOfCarWash> ClientsOfCarWashUnitOfWork
        {
            get
            {
                if (ClientsOfCarWashUW == null)
                {
                    ClientsOfCarWashUW = new DbRepository<ClientsOfCarWash>(_entities);
                }

                return ClientsOfCarWashUW;
            }

            set { ClientsOfCarWashUW = value; }
        }

        public DbRepository<Detailings> DetailingsUnitOfWork
        {
            get
            {
                if (DetailingsUW == null)
                {
                    DetailingsUW = new DbRepository<Detailings>(_entities);
                }

                return DetailingsUW;
            }

            set { DetailingsUnitOfWork = value; }
        }

        #region Dispose
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Save()
        {
            _entities.SaveChanges();
        }
        #endregion
    }
}

