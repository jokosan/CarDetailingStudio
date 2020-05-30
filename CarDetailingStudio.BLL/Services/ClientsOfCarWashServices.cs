using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services
{
    public class ClientsOfCarWashServices : IClientsOfCarWashServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private IClientInfoServices _clientInfo;

        public ClientsOfCarWashServices(IUnitOfWork unitOfWork, AutomapperConfig maper, IClientInfoServices clientInfo)
        {
            _unitOfWork = unitOfWork;
            _automapper = maper;
            _clientInfo = clientInfo;
        }

        public IEnumerable<ClientsOfCarWashBll> GetAll(string search)
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(_unitOfWork.ClientsOfCarWashUnitOfWork.GetWhere(x => x.NumberCar.Contains(search)));
        }

        public IEnumerable<ClientsOfCarWashBll> GetAll(int? id)
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(_unitOfWork.ClientsUnitOfWork.GetId(id));
        }



        public ClientsOfCarWashBll GetId(int? id)
        {
            return Mapper.Map<ClientsOfCarWashBll>(_unitOfWork.ClientsUnitOfWork.GetById(id));
        }

        public int Insert(ClientsOfCarWashBll AddCliens)
        {
            ClientsOfCarWash clientsOfCarWash = Mapper.Map<ClientsOfCarWashBll, ClientsOfCarWash>(AddCliens);
            _unitOfWork.ClientsOfCarWashUnitOfWork.Insert(clientsOfCarWash);

            _unitOfWork.Save();

            return clientsOfCarWash.id;
        }

        public void Delete(ClientsOfCarWashBll elementToDelete)
        {
            //ClientsOfCarWash clients = Mapper.Map<ClientsOfCarWashBll, ClientsOfCarWash>(elementToDelete);
            _unitOfWork.ClientsOfCarWashUnitOfWork.Delete(elementToDelete.id);
            _unitOfWork.Save();
        }

        public void ClientCarUpdate(ClientsOfCarWashBll updateClientCar)
        {
            ClientsOfCarWash clients = Mapper.Map<ClientsOfCarWashBll, ClientsOfCarWash>(updateClientCar);
            _unitOfWork.ClientsOfCarWashUnitOfWork.Update(clients);

            _unitOfWork.Save();
        }

        public IEnumerable<ClientsOfCarWashBll> GetAll()
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(_unitOfWork.ClientsUnitOfWork.Get());
        }

        public void ClientCarArxiv(int carId, bool status)
        {
            var clientCar = GetId(carId);
            clientCar.arxiv = status;

            ClientCarUpdate(clientCar);
        }

        public void RemoveClient(int clientId)
        {
            var clientCar = GetId(clientId);
            var clientInfo = _clientInfo.ClienWhereId(clientCar.IdInfoClient.Value);
            int? carId = null;

            Delete(clientCar);

            foreach (var item in clientInfo)
            {
                if (carId == null)
                    carId = item.Id;

                _clientInfo.Delete(item);
            }

            //var delClient = _clientInfo.ClientInfoGetId(carId);

        }
    }
}
