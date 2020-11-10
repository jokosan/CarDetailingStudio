using CarDetailingStudio.DAL.Infrastructure;

namespace CarDetailingStudio.DAL.Utilities.UnitOfWorks
{
    public partial class UnitOfWork
    {
        // Хранение шин 

        #region
        private DbRepository<TireStorage> tireStorageUW;
        private DbRepository<storageFee> storageFeeUW;
        private DbRepository<TireStorageServices> tireStorageServicesUW;

        public DbRepository<TireStorage> tireStorageUnitOfWork
        {
            get => tireStorageUW ?? (tireStorageUW = new DbRepository<TireStorage>(_entities));
            set => tireStorageUW = value;
        }

        public DbRepository<storageFee> storageFeeUnitOfWork
        {
            get => storageFeeUW ?? (storageFeeUW = new DbRepository<storageFee>(_entities));
            set => storageFeeUW = value;
        }

        public DbRepository<TireStorageServices> tireStorageServicesUnitOfWork
        {
            get => tireStorageServicesUW ?? (tireStorageServicesUW = new DbRepository<TireStorageServices>(_entities));
            set => tireStorageServicesUW = value;
        }
        #endregion

        // Шиномонтаж
        #region
        private DbRepository<PriceListTireFittingAdditionalServices> PriceListTireFittingAdditionalServicesUW { get; set; }
        private DbRepository<tireService> TireServiceUW { get; set;}  
        private DbRepository<PriceListTireFitting> PriceListTireFittingUW { get; set; }
        private DbRepository<TireRadius> TireRadiusUW { get; set; }
        private DbRepository<TypeOfCars> TypeOfCarsUW { get; set; }
        private DbRepository<tireChangeService> TireChangeServiceUW { get; set; }

        public DbRepository<tireChangeService> TireChangeServiceUnitOfWork
        {
            get => TireChangeServiceUW ?? (TireChangeServiceUW = new DbRepository<tireChangeService>(_entities));
            set => TireChangeServiceUW = value;
        }

        public DbRepository<PriceListTireFittingAdditionalServices> PriceListTireFittingAdditionalServicesUnitOfWork
        {
            get => PriceListTireFittingAdditionalServicesUW ?? (PriceListTireFittingAdditionalServicesUW = new DbRepository<PriceListTireFittingAdditionalServices>(_entities));
            set => PriceListTireFittingAdditionalServicesUW = value;
        }

        public DbRepository<tireService> TireServiceUnitOfWork
        {
            get => TireServiceUW ?? (TireServiceUW = new DbRepository<tireService>(_entities));
            set => TireServiceUW = value;
        }

        public DbRepository<PriceListTireFitting> PriceListTireFittingUnitOfWork
        {
            get => PriceListTireFittingUW ?? (PriceListTireFittingUW = new DbRepository<PriceListTireFitting>(_entities));
            set => PriceListTireFittingUW = value;
        }
        
        public DbRepository<TireRadius> TireRadiusUnitOfWork
        {
            get => TireRadiusUW ?? (TireRadiusUW = new DbRepository<TireRadius>(_entities));
            set => TireRadiusUW = value;
        }

        public DbRepository<TypeOfCars> TypeOfCarsUnitOfWork
        {
            get => TypeOfCarsUW ?? (TypeOfCarsUW = new DbRepository<TypeOfCars>(_entities));
            set => TypeOfCarsUW = value;
        }
        #endregion
    }
}
