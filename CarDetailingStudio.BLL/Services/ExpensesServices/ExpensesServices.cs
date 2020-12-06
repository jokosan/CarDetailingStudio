using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services.ExpensesServices
{
    public class ExpensesServices : IExpenses
    {
        private IUnitOfWork _unitOfWork;

        public ExpensesServices(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ExpensesBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<ExpensesBll>>(await _unitOfWork.ExpensesUnitOfWork.GetInclude("expenseCategory", "costCategories"));
        }

        public async Task<IEnumerable<ExpensesBll>> GetTableAll(int idTypeExpenses)
        {
            return Mapper.Map<IEnumerable<ExpensesBll>>(await _unitOfWork.ExpensesUnitOfWork.QueryObjectGraph(x => x.expenseCategoryId == idTypeExpenses,
                                                        "expenseCategory", "costCategories"));
        }

        public async Task<ExpensesBll> SelectId(int? elementId)
        {
            return Mapper.Map<ExpensesBll>(await _unitOfWork.ExpensesUnitOfWork.GetById(elementId));
        }

        public async Task Update(ExpensesBll elementToUpdate)
        {
            _unitOfWork.ExpensesUnitOfWork.Update(TransformEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        public async Task Insert(ExpensesBll element)
        {
            _unitOfWork.ExpensesUnitOfWork.Insert(TransformEntity(element));
            await _unitOfWork.Save();
        }

        public async Task<int> InsertId(ExpensesBll element)
        {
            var result = TransformEntity(element);
            _unitOfWork.ExpensesUnitOfWork.Insert(result);
            await _unitOfWork.Save();

            return result.idExpenses;
        }

        private Expenses TransformEntity(ExpensesBll entity) => Mapper.Map<ExpensesBll, Expenses>(entity);

        public async Task<IEnumerable<ExpensesBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<ExpensesBll>>(await _unitOfWork.ExpensesUnitOfWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.dateExpenses.Value) == datepresentDay.Date), "expenseCategory"));
        }

        public async Task<IEnumerable<ExpensesBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<ExpensesBll>>(await _unitOfWork.ExpensesUnitOfWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.dateExpenses.Value) >= startDate.Date
                                                                                                                       && (DbFunctions.TruncateTime(x.dateExpenses.Value) <= finalDate.Date)), "expenseCategory"));
        }
    }
}
