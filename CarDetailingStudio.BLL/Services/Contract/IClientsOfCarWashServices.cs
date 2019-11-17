using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientsOfCarWashServices
    {
        IEnumerable<ClientsOfCarWashBll> GetAll();
        IEnumerable<ClientsOfCarWashBll> GetAll(int? id);
        ClientsOfCarWashBll GetId(int? id);
        int Insert(ClientsOfCarWashBll AddCliens);
        void ClientCarUpdate(ClientsOfCarWashBll updateClientCar);
    }
}