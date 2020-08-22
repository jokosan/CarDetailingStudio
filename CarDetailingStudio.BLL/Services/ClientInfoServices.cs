using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class ClientInfoServices : IClientInfoServices
    {
        private IUnitOfWork _unitOfWork;

        public ClientInfoServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ClientInfoBll>> ClientInfoAll()
        {
            return Mapper.Map<IEnumerable<ClientInfoBll>>(await _unitOfWork.ClientInfoUnitOfWork.Get());
        }

        public async Task<IEnumerable<ClientInfoBll>> ClienWhereId(int id)
        {
            return Mapper.Map<IEnumerable<ClientInfoBll>>(await _unitOfWork.ClientInfoUnitOfWork.GetWhere(x => x.Id == id));
        }

        public async Task<ClientInfoBll> ClientInfoGetId(int? IdClient)
        {
            return Mapper.Map<ClientInfoBll>(await _unitOfWork.ClientInfoUnitOfWork.GetById(IdClient));
        }

        public async Task ClientInfoEdit(ClientInfoBll editClient)
        {
            ClientInfo clientInfo = Mapper.Map<ClientInfoBll, ClientInfo>(editClient);
            _unitOfWork.ClientInfoUnitOfWork.Update(clientInfo);
            await _unitOfWork.Save();
        }

        public async Task Delete(ClientInfoBll elementToDelete)
        {
            _unitOfWork.ClientInfoUnitOfWork.Delete(elementToDelete.Id);
            await _unitOfWork.Save();
        }

        public async Task AddClient(ClientInfoBll client)
        {
            ClientInfo clientInfoBll  = Mapper.Map<ClientInfoBll, ClientInfo>(client);
            _unitOfWork.ClientInfoUnitOfWork.Insert(clientInfoBll);
            await _unitOfWork.Save();
        }

       
    }
}
