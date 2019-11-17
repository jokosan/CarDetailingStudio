using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class ClientInfoServices : IClientInfoServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public ClientInfoServices(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapperConfig;
        }

        public IEnumerable<ClientInfoBll> ClientInfoAll()
        {
            return Mapper.Map<IEnumerable<ClientInfoBll>>(_unitOfWork.ClientInfoUnitOfWork.Get());
        }
        
        public ClientInfoBll ClientInfoGetId(int? IdClient)
        {
            return Mapper.Map<ClientInfoBll>(_unitOfWork.ClientInfoUnitOfWork.GetById(IdClient));
        }

        public void ClientInfoEdit(ClientInfoBll editClient)
        {
            ClientInfo clientInfo = Mapper.Map<ClientInfoBll, ClientInfo>(editClient);
            _unitOfWork.ClientInfoUnitOfWork.Update(clientInfo);
            _unitOfWork.Save();
        }
    }
}
