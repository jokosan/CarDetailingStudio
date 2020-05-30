using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientInfoServices : IDeleteFromDatabase<ClientInfoBll>
    {
        IEnumerable<ClientInfoBll> ClientInfoAll();
        ClientInfoBll ClientInfoGetId(int? IdClient);
        void ClientInfoEdit(ClientInfoBll editClient);
        IEnumerable<ClientInfoBll> ClienWhereId(int id);

    }
}
