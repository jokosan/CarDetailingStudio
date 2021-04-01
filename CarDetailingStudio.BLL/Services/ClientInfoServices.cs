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

        public async Task<IEnumerable<ClientInfoBll>> ClientInfoAll() =>
            Mapper.Map<IEnumerable<ClientInfoBll>>(await _unitOfWork.ClientInfoUnitOfWork.Get());
        
        public async Task<IEnumerable<ClientInfoBll>> ClienWhereId(int id) => 
            Mapper.Map<IEnumerable<ClientInfoBll>>(await _unitOfWork.ClientInfoUnitOfWork.GetWhere(x => x.Id == id));

        public async Task<ClientInfoBll> ClientInfoGetId(int? IdClient) =>
            Mapper.Map<ClientInfoBll>(await _unitOfWork.ClientInfoUnitOfWork.GetById(IdClient));

        public async Task Delete(ClientInfoBll elementToDelete)
        {
            _unitOfWork.ClientInfoUnitOfWork.Delete(elementToDelete.Id);
            await _unitOfWork.Save();
        }

        public async Task Insert(ClientInfoBll element)
        {
            _unitOfWork.ClientInfoUnitOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        public async Task Update(ClientInfoBll elementToUpdate)
        {
            _unitOfWork.ClientInfoUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        ClientInfo TransformAnEntity(ClientInfoBll entity) =>
            Mapper.Map<ClientInfoBll, ClientInfo>(entity);
    }
}
