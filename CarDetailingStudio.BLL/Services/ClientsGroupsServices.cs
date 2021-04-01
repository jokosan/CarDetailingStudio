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
    public class ClientsGroupsServices : IClientsGroupsServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapperConfig;

        public ClientsGroupsServices(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig)
        {
            _unitOfWork = unitOfWork;
            _automapperConfig = automapperConfig;
        }

        public async Task<IEnumerable<ClientsGroupsBll>> GetClientsGroups() =>
            Mapper.Map<IEnumerable<ClientsGroupsBll>>(await _unitOfWork.ClientsGroupsUnitOfWork.Get());

        public async Task<ClientsGroupsBll> GetId(int? id) =>
            Mapper.Map<ClientsGroupsBll>(await _unitOfWork.ClientsGroupsUnitOfWork.GetById(id));

        public async Task Insert(ClientsGroupsBll element)
        {
            ClientsGroups clientsGroups = Mapper.Map<ClientsGroupsBll, ClientsGroups>(element);

            _unitOfWork.ClientsGroupsUnitOfWork.Insert(clientsGroups);
            await _unitOfWork.Save();
        }

        public async Task Update(ClientsGroupsBll elementToUpdate)
        {
            ClientsGroups clientsGroups = Mapper.Map<ClientsGroupsBll, ClientsGroups>(elementToUpdate);

            _unitOfWork.ClientsGroupsUnitOfWork.Update(clientsGroups);
            await _unitOfWork.Save();
        }

        public async Task Delete(ClientsGroupsBll elementToDelete)
        {
            _unitOfWork.ClientInfoUnitOfWork.Delete(elementToDelete.Id);
            await _unitOfWork.Save();
        }
    }

}
