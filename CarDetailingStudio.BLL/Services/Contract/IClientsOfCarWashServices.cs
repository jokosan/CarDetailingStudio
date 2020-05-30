using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientsOfCarWashServices : IDeleteFromDatabase<ClientsOfCarWashBll>
    {
        IEnumerable<ClientsOfCarWashBll> GetAll();
        IEnumerable<ClientsOfCarWashBll> GetAll(string search);
        IEnumerable<ClientsOfCarWashBll> GetAll(int? id);
        ClientsOfCarWashBll GetId(int? id);
        int Insert(ClientsOfCarWashBll AddCliens);
        void ClientCarUpdate(ClientsOfCarWashBll updateClientCar);
        void ClientCarArxiv(int carId, bool status);
        void RemoveClient(int clientId);
    }
}