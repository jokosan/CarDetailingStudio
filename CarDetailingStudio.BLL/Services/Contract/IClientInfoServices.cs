using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientInfoServices : IDeleteFromDatabase<ClientInfoBll>
    {
        Task<IEnumerable<ClientInfoBll>> ClientInfoAll();
        Task<ClientInfoBll> ClientInfoGetId(int? IdClient);
        Task ClientInfoEdit(ClientInfoBll editClient);
        Task<IEnumerable<ClientInfoBll>> ClienWhereId(int id);

    }
}
