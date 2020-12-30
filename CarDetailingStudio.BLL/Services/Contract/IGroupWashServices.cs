using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IGroupWashServices : IDatabaseOperations<GroupWashServicesBll>
    {
        Task<IEnumerable<GroupWashServicesBll>> GetAllTable();
        Task<IEnumerable<GroupWashServicesBll>> GetIdAll(int? id);
        Task<IEnumerable<GroupWashServicesBll>> SelectGroupWashServices(List<int> idServices);
    }
}
