using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class UtilityCostsCategoryServices : IUtilityCostsCategory
    {
        private IUnitOfWork _unitOfWork;

        public UtilityCostsCategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;        
        }

        public async Task<IEnumerable<UtilityCostsCategoryBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<UtilityCostsCategoryBll>>(await _unitOfWork.utilityCostsCategoryUnitOfWork.Get());
        }

        public async Task<UtilityCostsCategoryBll> SelectId(int? elementId)
        {
            return Mapper.Map<UtilityCostsCategoryBll>(await _unitOfWork.utilityCostsCategoryUnitOfWork.GetById(elementId));
        }
    }
}
