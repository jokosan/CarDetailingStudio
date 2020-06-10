using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IServisesCarWashOrderServices
    {
        Task<IEnumerable<ServisesCarWashOrderBll>> GetAll();
        Task<IEnumerable<ServisesCarWashOrderBll>> GetAllId(int? id);
        Task ServicesDelete(int id, string NameClass);
        void ServisesInsert(List<ServisesCarWashOrderBll> idServeces, int idOrder, int idClient);

    }
}