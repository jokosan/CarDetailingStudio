using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class CarMarkServices : ICarMarkServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public CarMarkServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public IEnumerable<CarMarkBll> Get()
        {
            return Mapper.Map<IEnumerable<CarMarkBll>>(_unitOfWork.CarMarkUnitOfWork.Get());
        }

        public IEnumerable<CarMarkBll> GetWhere(string id)
        {
            return Mapper.Map<IEnumerable<CarMarkBll>>(_unitOfWork.CarMarkUnitOfWork.GetWhere(x => x.name.Contains(id)));
        }

        public IEnumerable<CarMarkBll> GetInclude()
        {
            return Mapper.Map<IEnumerable<CarMarkBll>>(_unitOfWork.CarMarkUnitOfWork.GetInclude("car_model"));
        }
    }
}
