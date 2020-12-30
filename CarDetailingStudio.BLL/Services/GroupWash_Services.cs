using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CarDetailingStudio.DAL;

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

        public async Task<IEnumerable<GroupWashServicesBll>> GetAllTable()
        {
            return Mapper.Map<IEnumerable<GroupWashServicesBll>>(await _unitOfWork.GroupWashServicesUnitOfWork.Get());
        }

        public async Task <IEnumerable<GroupWashServicesBll>> GetIdAll(int? id)
        {
            if (id == 2)
            {
                return Mapper.Map<IEnumerable<GroupWashServicesBll>>(await _unitOfWork.GroupWashServicesUnitOfWork.GetWhere(x => x.Id <= 2));
            }
            else if (id == 1)
            {
                return Mapper.Map<IEnumerable<GroupWashServicesBll>>(await _unitOfWork.GroupWashServicesUnitOfWork.GetWhere(x => x.Id != 2));
            }
            else
            {
                return await GetAllTable();
            }
        }

        public async Task<IEnumerable<GroupWashServicesBll>> SelectGroupWashServices(List<int> idServices)
        {
            var resultGroup = await GetAllTable();
            return resultGroup.Where(x => idServices.Contains(x.Id));
        }

        public async Task Insert(GroupWashServicesBll element)
        {
            _unitOfWork.GroupWashServicesUnitOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        public async Task Update(GroupWashServicesBll elementToUpdate)
        {
            _unitOfWork.GroupWashServicesUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        private GroupWashServices TransformAnEntity(GroupWashServicesBll entity) => Mapper.Map<GroupWashServicesBll, GroupWashServices>(entity);
    }
}
