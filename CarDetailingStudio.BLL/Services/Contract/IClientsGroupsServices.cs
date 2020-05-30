using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientsGroupsServices : IDatabaseOperations<ClientsGroupsBll>, IDeleteFromDatabase<ClientsGroupsBll>
    {
        IEnumerable<ClientsGroupsBll> GetClientsGroups();
        ClientsGroupsBll GetId(int? id);
    }
}
