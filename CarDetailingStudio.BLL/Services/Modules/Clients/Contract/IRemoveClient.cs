using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.Clients.Contract
{
    public interface IRemoveClient
    {
        Task RemoveCarClient(int clientId);
        Task RemoveClientCar(int ClientId);
    }
}
