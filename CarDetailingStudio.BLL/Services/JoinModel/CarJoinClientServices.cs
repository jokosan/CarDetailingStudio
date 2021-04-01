using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.JoinModel.Model;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using DevExpress.Utils.Design;
using CarDetailingStudio.BLL.Services.JoinModel.Contract;
using System.Security.Cryptography;

namespace CarDetailingStudio.BLL.Services.JoinModel
{
    public class CarJoinClientServices : ICarJoinClientServices 
    {
        private readonly IClientInfoServices _clientInfo;
        private readonly IClientsOfCarWashServices _clientsOfCarWash;
        private readonly IOrderServicesCarWashServices _orderServicesCarWash;

        public CarJoinClientServices(
            IClientsOfCarWashServices clientsOfCarWash,
            IClientInfoServices clientInfo,
            IOrderServicesCarWashServices orderServicesCarWash)
        {
            _clientInfo = clientInfo;
            _clientsOfCarWash = clientsOfCarWash;
            _orderServicesCarWash = orderServicesCarWash;
        }

        public async Task<IEnumerable<CarJoinClientModelBll>> JoinServicesClientCar()
        {
            var client = Mapper.Map<IEnumerable<ClientInfoBll>>(await _clientInfo.ClientInfoAll());
            var car = Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(await _clientsOfCarWash.GetAll());

            var resultJoin = (from t1 in client
                              join t2 in car on t1.Id equals t2.IdInfoClient
                              where t2.arxiv == true
                              select new CarJoinClientModelBll()
                              {
                                  idCar = t2.id,
                                  idClien = t1.Id,
                                  IdClientsGroups = t2.ClientsGroups.Name,
                                  Surname = t1.Surname,
                                  Name = t1.Name,
                                  PatronymicName = t1.PatronymicName,
                                  phone = t1.Phone,
                                  DateRegistration = t1.DateRegistration,
                                  Email = t1.Email,
                                  discont = t2.discont,
                                  carMarkName = t2.car_mark.name,
                                  carModelName = t2.car_model.name,
                                  carBodyName = t2.CarBody.Name,
                                  NumberCar = t2.NumberCar
                              });
         
            return resultJoin.AsEnumerable();
        }
    }
}
