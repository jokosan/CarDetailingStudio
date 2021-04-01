using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class CarBodyServices : ICarBodyServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapperConfig;

        public CarBodyServices(
            IUnitOfWork unitOfWork,
            AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapperConfig = automapper;
        }

        public async Task<IEnumerable<CarBodyBll>> GetTableAll() =>
            Mapper.Map<IEnumerable<CarBodyBll>>(await _unitOfWork.CarBodyUnitOfWork.Get());

        public async Task<CarBodyBll> SelectId(int? elementId) =>
            Mapper.Map<CarBodyBll>(await _unitOfWork.CarBodyUnitOfWork.GetById(elementId));
    }
}
