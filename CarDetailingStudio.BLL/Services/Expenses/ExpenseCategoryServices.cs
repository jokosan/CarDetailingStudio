using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class ExpenseCategoryServices : IExpenseCategory
    {
        private IUnitOfWork _unitOfWork;

        public ExpenseCategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ExpenseCategoryBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<ExpenseCategoryBll>>(await _unitOfWork.expenseCategoryUnitOfWork.Get());
        }

        public async Task Insert(ExpenseCategoryBll element)
        {
            expenseCategory expenseCategorys = Mapper.Map<ExpenseCategoryBll, expenseCategory>(element);

            _unitOfWork.expenseCategoryUnitOfWork.Insert(expenseCategorys);
            await _unitOfWork.Save();
        }

        public async Task<ExpenseCategoryBll> SelectId(int? elementId)
        {
            return Mapper.Map<ExpenseCategoryBll>(await _unitOfWork.expenseCategoryUnitOfWork.GetById(elementId));
        }

        public async Task Update(ExpenseCategoryBll elementToUpdate)
        {
            expenseCategory expenseCategorys = Mapper.Map<ExpenseCategoryBll, expenseCategory>(elementToUpdate);

            _unitOfWork.expenseCategoryUnitOfWork.Update(expenseCategorys);
            await _unitOfWork.Save();
        }
    }
}
