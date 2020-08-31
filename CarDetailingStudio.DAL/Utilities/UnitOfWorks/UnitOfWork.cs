using CarDetailingStudio.DAL.Infrastructure;
using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Utilities.UnitOfWorks
{
    public partial class UnitOfWork : IDisposable, IUnitOfWork
    {
        private carWashEntities _entities;

        public UnitOfWork()
        {
            _entities = new carWashEntities();
        }

        private DbRepository<ClientsOfCarWash> ClientsOfCarWashUW;
        private DbRepository<brigadeForToday> BrigadeForTodayUW;
        private DbRepository<CarWashWorkers> CarWashWorkersUW;
        private DbRepository<OrderServicesCarWash> OrderServicesCarWashUW;
        private DbRepository<JobTitleTable> JobTitleTableUW;
        private DbRepository<ServisesCarWashOrder> ServisesCarWashOrderUW;
        private DbRepository<Detailings> DetailingsUW;
        private DbRepository<OrderCarWashWorkers> OrderCarWashWorkersUW;
        private DbRepository<car_model> CarModelUW;
        private DbRepository<car_mark> CarMarkUW;
        private DbRepository<CarBody> CarBodyUW;
        private DbRepository<infoBrigadeForToday> InfoBrigadeUW;
        private DbRepository<ExchangeRates> ExchangeRatesUW;
        private DbRepository<GroupWashServices> GroupWashServicesUW;
        private DbRepository<ClientsGroups> ClientsGrupsUW;
        private DbRepository<ClientInfo> ClientInfoUW;
        private DbRepository<Credit> CreditUW;
        private DbRepository<StatusOrder> StatusOrderUW;
        private DbRepository<salaryBalance> SalaryBalanceUW;
        private DbRepository<orderCarpetWashing> OrderCarpetWashingUW;
        private DbRepository<bonusToSalary> bonusToSalaryUW;
        private DbRepository<PaymentState> PaymentStateUW;
        private DbRepository<Cashier> CashierUW;
        private DbRepository<utilityCostsCategory> utilityCostsCategoryUW;
        private DbRepository<costCategories> costCategoriesUW;

        private OrderServicesCarWashRepository orderUW;
        private ClientsOfCarWashRepository ClientsUW;
        private CarWashWorkersRepository WorkersUW;
        private BrigadeForTodayRepository BrigadeUW;
        private ServisesCarWashOrderRepository ServisesUW;
        private OrderInfoViewRepository OrderInfoUW;

        public DbRepository<costCategories> CostCategoriesUnionOfWork
        {
            get => costCategoriesUW ?? (costCategoriesUW = new DbRepository<costCategories>(_entities));
            set => costCategoriesUW = value;
        }

        public DbRepository<utilityCostsCategory> utilityCostsCategoryUnitOfWork
        {
            get => utilityCostsCategoryUW ?? (utilityCostsCategoryUW = new DbRepository<utilityCostsCategory>(_entities));
            set => utilityCostsCategoryUW = value;
        }

        public DbRepository<Cashier> CashierUtilOfWork
        {
            get => CashierUW ?? (CashierUW = new DbRepository<Cashier>(_entities));
            set => CashierUW = value;
        }

        public DbRepository<PaymentState> PaymentStateUnitOfWork
        {
            get => PaymentStateUW ?? (PaymentStateUW = new DbRepository<PaymentState>(_entities));
            set => PaymentStateUW = value;
        }

        public DbRepository<bonusToSalary> BonusToSalaryUnitOfWork
        {
            get => bonusToSalaryUW ?? (bonusToSalaryUW = new DbRepository<bonusToSalary>(_entities));
            set => bonusToSalaryUW = value;
        }

        public DbRepository<salaryBalance> SalaruBalanceUnitOfWork
        {
            get => SalaryBalanceUW ?? (SalaryBalanceUW = new DbRepository<salaryBalance>(_entities));
            set => SalaryBalanceUW = value;
        }

        public DbRepository<orderCarpetWashing> OrderCarpetWashingUnitOfWork
        {
            get => OrderCarpetWashingUW ?? (OrderCarpetWashingUW = new DbRepository<orderCarpetWashing>(_entities));
            set => OrderCarpetWashingUW = value;
        }
        public DbRepository<StatusOrder> StatusOrderUnitOfWork
        {
            get => StatusOrderUW ?? (StatusOrderUW = new DbRepository<StatusOrder>(_entities));
            set => StatusOrderUW = value;
        }

        public DbRepository<Credit> CreditUnitOgWork
        {
            get { return CreditUW ?? (CreditUW = new DbRepository<Credit>(_entities)); }
            set { CreditUW = value; }
        }

        public DbRepository<ClientInfo> ClientInfoUnitOfWork
        {
            get { return ClientInfoUW ?? (ClientInfoUW = new DbRepository<ClientInfo>(_entities)); }
            set { ClientInfoUW = value; }
        }

        public DbRepository<ClientsGroups> ClientsGroupsUnitOfWork
        {
            get { return ClientsGrupsUW ?? (ClientsGrupsUW = new DbRepository<ClientsGroups>(_entities)); }
            set { ClientsGrupsUW = value; }
        }

        public DbRepository<GroupWashServices> GroupWashServicesUnitOfWork
        {
            get { return GroupWashServicesUW ?? (GroupWashServicesUW = new DbRepository<GroupWashServices>(_entities)); }
            set { GroupWashServicesUW = value; }
        }

        public DbRepository<ExchangeRates> ExchangeRatesUnitOfWork
        {
            get { return ExchangeRatesUW ?? (ExchangeRatesUW = new DbRepository<ExchangeRates>(_entities)); }
            set { ExchangeRatesUW = value; }
        }

        public DbRepository<infoBrigadeForToday> infoBrigadeUnitOfWork
        {
            get { return InfoBrigadeUW ?? (InfoBrigadeUW = new DbRepository<infoBrigadeForToday>(_entities)); }
            set { InfoBrigadeUW = value; }
        }


        public DbRepository<CarBody> CarBodyUnitOfWork
        {
            get { return CarBodyUW ?? (CarBodyUW = new DbRepository<CarBody>(_entities)); }
            set { CarBodyUW = value; }
        }

        public DbRepository<car_model> CarModelUnitOfWork
        {
            get { return CarModelUW ?? (CarModelUW = new DbRepository<car_model>(_entities)); }
            set { CarModelUW = value; }
        }

        public DbRepository<car_mark> CarMarkUnitOfWork
        {
            get { return CarMarkUW ?? (CarMarkUW = new DbRepository<car_mark>(_entities)); }
            set { CarMarkUW = value; }
        }

        public OrderInfoViewRepository OrderInfoUnitOfWork
        {
            get { return OrderInfoUW ?? (OrderInfoUW = new OrderInfoViewRepository(_entities)); }
            set { OrderInfoUW = value; }
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

        public async Task Save()
        {
           await _entities.SaveChangesAsync();
        }
        #endregion
    }
}

