﻿using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services
{
    public class ClientsOfCarWashServices : IClientsOfCarWashServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private ClientsOfCarWashBll _clients;

        public ClientsOfCarWashServices(IUnitOfWork unitOfWork, AutomapperConfig maper, ClientsOfCarWashBll clients)
        {
            _unitOfWork = unitOfWork;
            _automapper = maper;
            _clients = clients;
        }

        public IEnumerable<ClientsOfCarWashBll> GetAll(string search)
        {          
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(_unitOfWork.ClientsOfCarWashUnitOfWork.GetWhere(x => x.NumberCar.Contains(search)));
        }

        public IEnumerable<ClientsOfCarWashBll> GetAll(int? id)
        { 
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(_unitOfWork.ClientsUnitOfWork.GetWhere(x => x.IdInfoClient == id));
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
    }
}
