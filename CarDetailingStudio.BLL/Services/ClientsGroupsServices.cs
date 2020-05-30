using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;

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

        public IEnumerable<ClientsGroupsBll> GetClientsGroups()
        {
            return Mapper.Map<IEnumerable<ClientsGroupsBll>>(_unitOfWork.ClientsGroupsUnitOfWork.Get());
        }

        public ClientsGroupsBll GetId(int? id)
        {
            return Mapper.Map<ClientsGroupsBll>(_unitOfWork.ClientsGroupsUnitOfWork.GetById(id));
        }

        public void Insert(ClientsGroupsBll element)
        {
            ClientsGroups clientsGroups = Mapper.Map<ClientsGroupsBll, ClientsGroups>(element);

            _unitOfWork.ClientsGroupsUnitOfWork.Insert(clientsGroups);
            _unitOfWork.Save();
        }

        public void Update(ClientsGroupsBll elementToUpdate)
        {
            ClientsGroups clientsGroups = Mapper.Map<ClientsGroupsBll, ClientsGroups>(elementToUpdate);

            _unitOfWork.ClientsGroupsUnitOfWork.Update(clientsGroups);
            _unitOfWork.Save();
        }

        public void Delete(ClientsGroupsBll elementToDelete)
        {
            _unitOfWork.ClientInfoUnitOfWork.Delete(elementToDelete.Id);
            _unitOfWork.Save();
        }
    }

}
