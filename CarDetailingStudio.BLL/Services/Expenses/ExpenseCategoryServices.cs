using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class ExpenseCategoryServices : IExpenseCategory
    {
        private IUnitOfWork _unitOfWork;

        public ExpenseCategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ExpenseCategoryBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<ExpenseCategoryBll>>(_unitOfWork.expenseCategoryUnitOfWork.Get());
        }

        public void Insert(ExpenseCategoryBll element)
        {
            expenseCategory expenseCategorys = Mapper.Map<ExpenseCategoryBll, expenseCategory>(element);

            _unitOfWork.expenseCategoryUnitOfWork.Insert(expenseCategorys);
            _unitOfWork.Save();
        }

        public ExpenseCategoryBll SelectId(int? elementId)
        {
            return Mapper.Map<ExpenseCategoryBll>(_unitOfWork.expenseCategoryUnitOfWork.GetById(elementId));
        }

        public void Update(ExpenseCategoryBll elementToUpdate)
        {
            expenseCategory expenseCategorys = Mapper.Map<ExpenseCategoryBll, expenseCategory>(elementToUpdate);

            _unitOfWork.expenseCategoryUnitOfWork.Update(expenseCategorys);
            _unitOfWork.Save();
        }
    }
}
