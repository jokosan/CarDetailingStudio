using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class OtherExpensesServises : IOtherExpenses
    {
        private IUnitOfWork _unitOfWork;

        public OtherExpensesServises(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OtherExpensesBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<OtherExpensesBll>>(await _unitOfWork.otherExpensesUnitOfWork.GetInclude("expenseCategory"));
        }

        public async Task Insert(OtherExpensesBll element)
        {
            otherExpenses otherExpenses = Mapper.Map<OtherExpensesBll, otherExpenses>(element);

            _unitOfWork.otherExpensesUnitOfWork.Insert(otherExpenses);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<OtherExpensesBll>> MonthlyReport(DateTime date)
        {
            return Mapper.Map<IEnumerable<OtherExpensesBll>>(await _unitOfWork.otherExpensesUnitOfWork.GetWhere(x => x.dateExpenses.Value.Month == date.Month));
        }

        public async Task<OtherExpensesBll> SelectId(int? elementId)
        {
            return Mapper.Map<OtherExpensesBll>(await _unitOfWork.otherExpensesUnitOfWork.GetById(elementId));
        }

        public async Task Update(OtherExpensesBll elementToUpdate)
        {
            otherExpenses otherExpenses = Mapper.Map<OtherExpensesBll, otherExpenses>(elementToUpdate);

            _unitOfWork.otherExpensesUnitOfWork.Update(otherExpenses);
            await _unitOfWork.Save();
        }
    }
}
