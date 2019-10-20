using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientsOfCarWashServices
    {
        IEnumerable<ClientsOfCarWashBll> GetAll();
        ClientsOfCarWashBll GetId(int? id);
        void Insert(ClientsOfCarWashBll AddCliens);
    }
}