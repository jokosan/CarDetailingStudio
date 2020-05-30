using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class OtherExpensesServises : IOtherExpenses
    {
        private IUnitOfWork _unitOfWork;

        public OtherExpensesServises(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<OtherExpensesBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<OtherExpensesBll>>(_unitOfWork.otherExpensesUnitOfWork.GetInclude("expenseCategory"));
        }

        public void Insert(OtherExpensesBll element)
        {
            otherExpenses otherExpenses = Mapper.Map<OtherExpensesBll, otherExpenses>(element);

            _unitOfWork.otherExpensesUnitOfWork.Insert(otherExpenses);
            _unitOfWork.Save();
        }

        public IEnumerable<OtherExpensesBll> MonthlyReport(DateTime date)
        {
            return Mapper.Map<IEnumerable<OtherExpensesBll>>(_unitOfWork.otherExpensesUnitOfWork.GetWhere(x => x.dateExpenses?.Month == date.Month));
        }

        public OtherExpensesBll SelectId(int? elementId)
        {
            return Mapper.Map<OtherExpensesBll>(_unitOfWork.otherExpensesUnitOfWork.GetById(elementId));
        }

        public void Update(OtherExpensesBll elementToUpdate)
        {
            otherExpenses otherExpenses = Mapper.Map<OtherExpensesBll, otherExpenses>(elementToUpdate);

            _unitOfWork.otherExpensesUnitOfWork.Update(otherExpenses);
            _unitOfWork.Save();
        }
    }
}
