using CarDetailingStudio.DAL.Infrastructure;

namespace CarDetailingStudio.DAL.Utilities.UnitOfWorks
{
    public partial class UnitOfWork
    {
        DbRepository<TireStorage> tireStorageUW;
        DbRepository<storageFee> storageFeeUW;
        DbRepository<TireStorageServices> tireStorageServicesUW;

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
    }
}
