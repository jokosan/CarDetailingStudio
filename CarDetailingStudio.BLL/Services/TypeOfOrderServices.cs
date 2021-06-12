using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CarDetailingStudio.BLL.Services
{
    public class TypeOfOrderServices : ITypeOfOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeOfOrderServices(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TypeOfOrderBll>> GetTableAll()
             => Mapper.Map<IEnumerable<TypeOfOrderBll>>(await _unitOfWork.TypeOfOrderUnitOfWork.Get());

        public async Task<TypeOfOrderBll> SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }
    }
}
