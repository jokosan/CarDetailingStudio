using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CarDetailingStudio.BLL.Services
{
    public class ServisesCarWashOrderServices : IServices<ServisesCarWashOrderBll>
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public ServisesCarWashOrderServices(UnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public IEnumerable<ServisesCarWashOrderBll> GetAll()
        {
            return Mapper.Map<IEnumerable<ServisesCarWashOrderBll>>(_unitOfWork.ServisesCarWashOrderUnitOfWork.Get());
        }

    }
}
