using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CarDetailingStudio.BLL.Services.TireFitting
{
    public class TypeOfCarsServices : ITypeOfCars
    {
        private IUnitOfWork _unitOfWork;

        public TypeOfCarsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TypeOfCarsBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<TypeOfCarsBll>>(await _unitOfWork.TypeOfCarsUnitOfWork.Get());
        }

        public async Task<TypeOfCarsBll> SelectId(int? elementId)
        {
            return Mapper.Map<TypeOfCarsBll>(await _unitOfWork.TypeOfCarsUnitOfWork.GetById(elementId));
        }
    }
}
