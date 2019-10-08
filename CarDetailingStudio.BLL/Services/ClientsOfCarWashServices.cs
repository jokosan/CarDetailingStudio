using CarDetailingStudio.BLL.Model;
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
    public class ClientsOfCarWashServices : IServices<ClientsOfCarWashBll>
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private ClientsOfCarWashBll _clients;

        public ClientsOfCarWashServices(UnitOfWork unitOfWork, AutomapperConfig maper, ClientsOfCarWashBll clients)
        {
            _unitOfWork = unitOfWork;
            _automapper = maper;
            _clients = clients;
        }

        public IEnumerable<ClientsOfCarWashBll> GetAll()
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(_unitOfWork.ClientsOfCarWashUnitOfWork.Get());
        }

        public ClientsOfCarWashBll GetId(int? id)
        {
            var resilt = Mapper.Map<ClientsOfCarWashBll>(_unitOfWork.ClientsOfCarWashUnitOfWork.GetById(id));
            return resilt;
        }

        public void Insert(ClientsOfCarWashBll AddCliens)
        {
            ClientsOfCarWash clientsOfCarWash = Mapper.Map<ClientsOfCarWashBll, ClientsOfCarWash>(AddCliens);
            _unitOfWork.ClientsOfCarWashUnitOfWork.Insert(clientsOfCarWash);
        }
    }
}
