using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.BLL.Utilities.Map;

namespace CarDetailingStudio.BLL.Services
{
    public class ClientsOfCarWashServices : IServices<ClientsOfCarWashBll>
    {
        private UnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public ClientsOfCarWashServices(UnitOfWork unitOfWork, AutomapperConfig maper)
        {
            _unitOfWork = unitOfWork;
            _automapper = maper;
        }

        public IEnumerable<ClientsOfCarWashBll> GetAll()
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashBll>>(_unitOfWork.ClientsOfCarWashUnitOfWork.Get());
        }

        public ClientsOfCarWashBll CustomerOrders(int? id)
        {
            var resilt = Mapper.Map<ClientsOfCarWashBll>(_unitOfWork.ClientsOfCarWashUnitOfWork.GetById(id));
            return resilt;
        }
    }
}
