using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

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

        public ExpenseCategoryBll SelectId(int? elementId)
        {
            return Mapper.Map<ExpenseCategoryBll>(_unitOfWork.expenseCategoryUnitOfWork.GetById(elementId));
        }
    }
}
