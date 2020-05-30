using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;

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

        public IEnumerable<ClientInfoBll> ClienWhereId(int id)
        {
            return Mapper.Map<IEnumerable<ClientInfoBll>>(_unitOfWork.ClientInfoUnitOfWork.GetWhere(x => x.Id == id));
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

        public void Delete(ClientInfoBll elementToDelete)
        {
            // ClientInfo clients = Mapper.Map<ClientInfoBll, ClientInfo>(elementToDelete);
            _unitOfWork.ClientInfoUnitOfWork.Delete(elementToDelete.Id);
            _unitOfWork.Save();
        }

    }
}
