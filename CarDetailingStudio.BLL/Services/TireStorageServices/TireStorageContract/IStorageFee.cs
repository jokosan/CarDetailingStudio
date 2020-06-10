using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract
{
    public interface IStorageFee : IGetFromDatabase<StorageFeeBll>, IDatabaseOperations<StorageFeeBll>
    {
        Task<int> InsertVoidInt(StorageFeeBll element);
    }
}
