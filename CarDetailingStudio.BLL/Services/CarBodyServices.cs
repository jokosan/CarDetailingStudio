using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services
{
    public class CarBodyServices : ICarBodyServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapperConfig;

        public CarBodyServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapperConfig = automapper;
        }

        public IEnumerable<CarBodyBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<CarBodyBll>>(_unitOfWork.CarBodyUnitOfWork.Get());
        }

        public CarBodyBll SelectId(int? elementId)
        {
            return Mapper.Map<CarBodyBll>(_unitOfWork.CarBodyUnitOfWork.GetById(elementId));
        }
    }
}
