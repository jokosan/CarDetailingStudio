using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.ExpensesServices
{
    public class ExpenseCategoryServices : IExpenseCategory
    {
        private IUnitOfWork _unitOfWork;

        public ExpenseCategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ExpenseCategoryBll>> GetTableAll() =>
            Mapper.Map<IEnumerable<ExpenseCategoryBll>>(await _unitOfWork.expenseCategoryUnitOfWork.Get());


        public async Task<IEnumerable<ExpenseCategoryBll>> GetTableAll(int id) =>
            Mapper.Map<IEnumerable<ExpenseCategoryBll>>(await _unitOfWork.expenseCategoryUnitOfWork.GetWhere(x => x.idExpenseCategory == id));

        public async Task<ExpenseCategoryBll> SelectId(int? elementId) =>
            Mapper.Map<ExpenseCategoryBll>(await _unitOfWork.expenseCategoryUnitOfWork.GetById(elementId));


        public async Task Insert(ExpenseCategoryBll element)
        {
            _unitOfWork.expenseCategoryUnitOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        public async Task Update(ExpenseCategoryBll elementToUpdate)
        {
            _unitOfWork.expenseCategoryUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        public expenseCategory TransformAnEntity(ExpenseCategoryBll entity) => Mapper.Map<ExpenseCategoryBll, expenseCategory>(entity);
    }
}
