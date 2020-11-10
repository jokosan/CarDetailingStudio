using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract
{
    public interface ITireStorage : IGetFromDatabase<TireStorageBll>, IDatabaseOperations<TireStorageBll>
    {
        Task<TireStorageBll> GetOrderId(int idOrder);
    }
}
