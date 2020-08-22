using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientsOfCarWashServices : IDeleteFromDatabase<ClientsOfCarWashBll>
    {
        Task<IEnumerable<ClientsOfCarWashBll>> GetAll();
        Task<IEnumerable<ClientsOfCarWashBll>> GetAll(string search);
        Task<IEnumerable<ClientsOfCarWashBll>> GetAll(int? id);
        Task<ClientsOfCarWashBll> GetId(int? id);
        Task<int> Insert(ClientsOfCarWashBll AddCliens);
        Task ClientCarUpdate(ClientsOfCarWashBll updateClientCar);
        Task ClientCarArxiv(int carId, bool status);
        Task<ClientsOfCarWashBll> ClientWhereToInfoClient(int idInfoClient);
    }
}