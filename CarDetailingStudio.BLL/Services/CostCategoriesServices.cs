using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services
{
    public class CostCategoriesServices : ICostCategories
    {
        private IUnitOfWork _unitOfWork;

        public CostCategoriesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CostCategoriesBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<CostCategoriesBll>>(await _unitOfWork.CostCategoriesUnionOfWork.Get());
        }

        public async Task<CostCategoriesBll> SelectId(int? elementId)
        {
            return Mapper.Map<CostCategoriesBll>(await _unitOfWork.CostCategoriesUnionOfWork.GetById(elementId));
        }

        public async Task<IEnumerable<CostCategoriesBll>> GetWhere(int elementId)
        {
            return Mapper.Map<IEnumerable<CostCategoriesBll>>(await _unitOfWork.CostCategoriesUnionOfWork.GetWhere(x => x.typeOfExpenses == elementId));
        }
    }
}
