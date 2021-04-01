using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services
{
    public class CostCategoriesServices : ICostCategories
    {
        private IUnitOfWork _unitOfWork;

        public CostCategoriesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CostCategoriesBll>> GetTableAll() =>
            Mapper.Map<IEnumerable<CostCategoriesBll>>(await _unitOfWork.CostCategoriesUnionOfWork.Get());

        public async Task<CostCategoriesBll> SelectId(int? elementId) =>
            Mapper.Map<CostCategoriesBll>(await _unitOfWork.CostCategoriesUnionOfWork.GetById(elementId));

        public async Task<IEnumerable<CostCategoriesBll>> GetWhere(int elementId) =>
            Mapper.Map<IEnumerable<CostCategoriesBll>>(await _unitOfWork.CostCategoriesUnionOfWork.GetWhere(x => x.typeOfExpenses == elementId));

        public async Task Insert(CostCategoriesBll element)
        {
            _unitOfWork.CostCategoriesUnionOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();   
        }

        public async Task Update(CostCategoriesBll elementToUpdate)
        {
            _unitOfWork.CostCategoriesUnionOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        public costCategories TransformAnEntity(CostCategoriesBll entity) =>
            Mapper.Map<CostCategoriesBll, costCategories>(entity);
    }
}
