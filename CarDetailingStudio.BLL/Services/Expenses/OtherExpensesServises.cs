using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        #region  Отчеты за день и месяц
        public async Task<IEnumerable<OtherExpensesBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<OtherExpensesBll>>(await _unitOfWork.otherExpensesUnitOfWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.dateExpenses.Value) == datepresentDay.Date), "expenseCategory"));
        }

        public async Task<IEnumerable<OtherExpensesBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<OtherExpensesBll>>(await _unitOfWork.otherExpensesUnitOfWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.dateExpenses.Value) >= startDate.Date
                                                                                                                        && (DbFunctions.TruncateTime(x.dateExpenses.Value) <= finalDate.Date)), "expenseCategory"));
        }
        #endregion

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
