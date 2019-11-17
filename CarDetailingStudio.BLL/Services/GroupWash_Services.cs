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
    public class GroupWash_Services : IGroupWashServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapperConfig;

        public GroupWash_Services(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig)
        {
            _unitOfWork = unitOfWork;
            _automapperConfig = automapperConfig;
        }

        public IEnumerable<GroupWashServicesBll> GetAllTable()
        {
            return Mapper.Map<IEnumerable<GroupWashServicesBll>>(_unitOfWork.GroupWashServicesUnitOfWork.Get());
        }
    }
}
