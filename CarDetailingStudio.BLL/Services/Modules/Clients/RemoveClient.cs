using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Modules.Clients.Contract;

namespace CarDetailingStudio.BLL.Services.Modules.Clients
{
    public class RemoveClient : IRemoveClient
    {
        private IUnitOfWork _unitOfWork;
        private IClientInfoServices _clientInfo;
        private IClientsOfCarWashServices _clientsOfCarWash;

        public RemoveClient(IUnitOfWork unitOfWork, IClientInfoServices  clientInfoServices, IClientsOfCarWashServices clientsOfCarWash)
        {
            _unitOfWork = unitOfWork;
            _clientInfo = clientInfoServices;
            _clientsOfCarWash = clientsOfCarWash;
        }

        public async Task RemoveCarClient(int clientId)
        {
            var clientCar = await _clientsOfCarWash.GetId(clientId);
            var clientInfo = await _clientInfo.ClienWhereId(clientCar.IdInfoClient.Value);
            int? carId = null;

            await _clientsOfCarWash.Delete(clientCar);

            foreach (var item in clientInfo)
            {
                if (carId == null)
                    carId = item.Id;

                await _clientInfo.Delete(item);
            }
        }

        public async Task RemoveClientCar(int ClientId)
        {
            await _clientInfo.Delete(Mapper.Map<ClientInfoBll>(await _unitOfWork.ClientInfoUnitOfWork.GetById(ClientId)));
            var removeCarClient = await _clientsOfCarWash.GetAll(ClientId);

            foreach (var item in removeCarClient)
            {
               await _clientsOfCarWash.Delete(item);
            }
        }
    }
}
