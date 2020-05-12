using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientsGroupsServices : IDatabaseOperations<ClientsGroupsBll>, IDeleteFromDatabase<ClientsGroupsBll>
    {
        IEnumerable<ClientsGroupsBll> GetClientsGroups();
        ClientsGroupsBll GetId(int? id);
    }
}
