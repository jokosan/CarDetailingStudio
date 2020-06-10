using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IGroupWashServices
    {
        Task<IEnumerable<GroupWashServicesBll>> GetAllTable();
        Task<IEnumerable<GroupWashServicesBll>> GetIdAll(int? id);
    }
}
