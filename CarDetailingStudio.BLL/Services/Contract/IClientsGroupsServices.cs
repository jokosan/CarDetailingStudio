using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientsGroupsServices : IDatabaseOperations<ClientsGroupsBll>, IDeleteFromDatabase<ClientsGroupsBll>
    {
        Task<IEnumerable<ClientsGroupsBll>> GetClientsGroups();
        Task<ClientsGroupsBll> GetId(int? id);
    }
}
