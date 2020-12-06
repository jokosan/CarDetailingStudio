using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract
{
    public interface ITireStorageServices : IGetFromDatabase<TireStorageServicesBll>, IDatabaseOperations<TireStorageServicesBll>
    {
 
    }
}
