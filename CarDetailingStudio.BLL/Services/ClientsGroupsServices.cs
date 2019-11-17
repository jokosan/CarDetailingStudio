using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;

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
    }

}
