using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<CarMarkBll>> Get()
        {
            return Mapper.Map<IEnumerable<CarMarkBll>>(await _unitOfWork.CarMarkUnitOfWork.Get());
        }

        public async Task<IEnumerable<CarMarkBll>> GetWhere(string id)
        {
            return Mapper.Map<IEnumerable<CarMarkBll>>(await _unitOfWork.CarMarkUnitOfWork.GetWhere(x => x.name.Contains(id)));
        }

        public async Task<IEnumerable<CarMarkBll>> GetInclude()
        {
            return Mapper.Map<IEnumerable<CarMarkBll>>(await _unitOfWork.CarMarkUnitOfWork.GetInclude("car_model"));
        }
    }
}
