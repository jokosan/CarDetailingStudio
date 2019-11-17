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
    public class JobTitleTableServices : IJobTitleTableServices
    {
        public IUnitOfWork _unitOfWork;
        public AutomapperConfig _automapper;

        public JobTitleTableServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public IEnumerable<JobTitleTableBll> SelectJobTitle()
        {
            return Mapper.Map<IEnumerable<JobTitleTableBll>>(_unitOfWork.JobTitleTableUnitOfWork.Get()); 
        }
    }
}
