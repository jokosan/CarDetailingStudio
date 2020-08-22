﻿using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class ClientsOfCarWashServices : IClientsOfCarWashServices
    {
        private IUnitOfWork _unitOfWork;

        public ClientsOfCarWashServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ClientsOfCarWashBll>> GetAll(string search)
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(await _unitOfWork.ClientsOfCarWashUnitOfWork.GetWhere(x => x.NumberCar.Contains(search)));
        }

        public async Task<IEnumerable<ClientsOfCarWashBll>> GetAll(int? id)
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(await _unitOfWork.ClientsUnitOfWork.GetId(id));
        }

        public async Task<ClientsOfCarWashBll> ClientWhereToInfoClient(int idInfoClient)
        {
            var clientWhere = Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(await _unitOfWork.ClientsOfCarWashUnitOfWork.GetWhere(x => x.IdInfoClient == idInfoClient));
            return clientWhere.FirstOrDefault(x => x.IdInfoClient == idInfoClient);
        }

        public async Task<ClientsOfCarWashBll> GetId(int? id)
        {
            return Mapper.Map<ClientsOfCarWashBll>(await _unitOfWork.ClientsUnitOfWork.GetById(id));
        }

        public async Task<int> Insert(ClientsOfCarWashBll AddCliens)
        {
            ClientsOfCarWash clientsOfCarWash = Mapper.Map<ClientsOfCarWashBll, ClientsOfCarWash>(AddCliens);
            _unitOfWork.ClientsOfCarWashUnitOfWork.Insert(clientsOfCarWash);

            await _unitOfWork.Save();

            return clientsOfCarWash.id;
        }

        public async Task Delete(ClientsOfCarWashBll elementToDelete)
        {
            _unitOfWork.ClientsOfCarWashUnitOfWork.Delete(elementToDelete.id);
            await _unitOfWork.Save();
        }

        public async Task ClientCarUpdate(ClientsOfCarWashBll updateClientCar)
        {
            ClientsOfCarWash clients = Mapper.Map<ClientsOfCarWashBll, ClientsOfCarWash>(updateClientCar);
            _unitOfWork.ClientsOfCarWashUnitOfWork.Update(clients);

            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<ClientsOfCarWashBll>> GetAll()
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(await _unitOfWork.ClientsUnitOfWork.Get());
        }

        public async Task ClientCarArxiv(int carId, bool status)
        {
            var clientCar = await GetId(carId);
            clientCar.arxiv = status;

            await ClientCarUpdate(clientCar);
        }

      
    }
}
