using CarDetailingStudio.DAL.Infrastructure;
using CarDetailingStudio.DAL.Infrastructure.Contract;
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
        private DbRepository<brigadeForToday> BrigadeForTodayUW;
        private DbRepository<OrderServicesCarWash> CarWashWorkersUW;
        private DbRepository<OrderServicesCarWash> OrderServicesCarWashUW;
        private DbRepository<JobTitleTable> JobTitleTableUW;
        private DbRepository<ServisesCarWashOrder> ServisesCarWashOrderUW;
        private DbRepository<Detailings> DetailingsUW;
        private DbRepository<OrderCarWashWorkers> OrderCarWashWorkersUW;
        private DbRepository<Costs> CostsUW;

        private OrderServicesCarWashRepository orderUW;
        private ClientsOfCarWashRepository ClientsUW;
        private CarWashWorkersRepository WorkersUW;
        private BrigadeForTodayRepository BrigadeUW;
        private ServisesCarWashOrderRepository ServisesUW;
        private OrderInfoViewRepository OrderInfoUW;

        public OrderInfoViewRepository OrderInfoUnitOfWork
        {
            get { return OrderInfoUW ?? (OrderInfoUW = new OrderInfoViewRepository(_entities)); }
            set { OrderInfoUW = value; }
        }

        public DbRepository<Costs> CostsUnitOfWork
        {
            get { return CostsUW ?? (CostsUW = new DbRepository<Costs>(_entities)); }
            set { CostsUW = value; }
        }

        public ServisesCarWashOrderRepository ServisesUnitOfWork
        {
            get
            {
                if (ServisesUW == null)
                {
                    ServisesUW = new ServisesCarWashOrderRepository(_entities);
                }

                return ServisesUW;
            }

            set { ServisesUW = value; }
        }

        public BrigadeForTodayRepository BrigadeUnitOfWork
        {
            get
            {
                if (BrigadeUW == null)
                {
                    BrigadeUW = new BrigadeForTodayRepository(_entities);
                }

                return BrigadeUW;
            }

            set { BrigadeUW = value; }
        }

        public CarWashWorkersRepository WorkersUnitOfWork
        {
            get
            {
                if (WorkersUW == null)
                {
                    WorkersUW = new CarWashWorkersRepository(_entities);
                }

                return WorkersUW;
            }

            set { WorkersUW = value; }
        }

        public ClientsOfCarWashRepository ClientsUnitOfWork
        {
            get
            {
                if (ClientsUW == null)
                {
                    ClientsUW = new ClientsOfCarWashRepository(_entities);
                }

                return ClientsUW;
            }

            set { ClientsUW = value; }
        }

        public OrderServicesCarWashRepository orderUnitiOfWork
        {
            get
            {
                if (orderUW == null)
                {
                    orderUW = new OrderServicesCarWashRepository(_entities);
                }

                return orderUW;
            }
            set { orderUW = value; }
        }

        public DbRepository<OrderCarWashWorkers> OrderCarWasWorkersUnitOFWork
        {
            get
            {
                if (OrderCarWashWorkersUW == null)
                {
                    OrderCarWashWorkersUW = new DbRepository<OrderCarWashWorkers>(_entities);
                }

                return OrderCarWashWorkersUW;
            }

            set { OrderCarWashWorkersUW = value; }
        }

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

        public DbRepository<OrderServicesCarWash> CarWashWorkersUnitOfWork
        {
            get
            {
                if (CarWashWorkersUW == null)
                {
                    CarWashWorkersUW = new DbRepository<OrderServicesCarWash>(_entities);
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

