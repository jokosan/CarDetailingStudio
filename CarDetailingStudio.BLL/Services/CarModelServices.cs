using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class CarModelServices : ICarModelServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public CarModelServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public async Task<IEnumerable<CarModelBll>> GetWhere(int id)
        {
            return Mapper.Map<IEnumerable<CarModelBll>>(await _unitOfWork.CarModelUnitOfWork.GetWhere(x => x.id_car_mark == id));
        }
    }
}
