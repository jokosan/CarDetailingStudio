using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class JobTitleTableServices : IJobTitleTableServices
    {
        public IUnitOfWork _unitOfWork;
        public AutomapperConfig _automapper;

        public JobTitleTableServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public async Task<IEnumerable<JobTitleTableBll>> SelectJobTitle()
        {
            return Mapper.Map<IEnumerable<JobTitleTableBll>>(await _unitOfWork.JobTitleTableUnitOfWork.Get());
        }
    }
}
