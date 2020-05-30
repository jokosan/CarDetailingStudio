using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;

namespace CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract
{
    public interface IStorageFee : IGetFromDatabase<StorageFeeBll>, IDatabaseOperations<StorageFeeBll>
    {
        int InsertVoidInt(StorageFeeBll element);
    }
}
