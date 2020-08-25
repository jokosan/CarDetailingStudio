using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class SalaryExpensesServices : ISalaryExpenses
    {
        private IUnitOfWork _unitOfWork;

        public SalaryExpensesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SalaryExpensesBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<SalaryExpensesBll>>(await _unitOfWork.salaryExpensesUnitOfWork.GetInclude("CarWashWorkers"));
        }

        public async Task<SalaryExpensesBll> SelectId(int? elementId)
        {
            return Mapper.Map<SalaryExpensesBll>(await _unitOfWork.salaryExpensesUnitOfWork.GetById(elementId));
        }

        public async Task Insert(IEnumerable<SalaryExpensesBll> element)
        {
            var salary = Mapper.Map<IEnumerable<SalaryExpensesBll>, IEnumerable<salaryExpenses>>(element);

            _unitOfWork.salaryExpensesUnitOfWork.Insert(salary.ToList());
            await _unitOfWork.Save();
        }

        public async Task Insert(SalaryExpensesBll element)
        {
            salaryExpenses salaryExpenses = Mapper.Map<SalaryExpensesBll, salaryExpenses>(element);

            _unitOfWork.salaryExpensesUnitOfWork.Insert(salaryExpenses);
            await _unitOfWork.Save();
        }

        public async Task Update(SalaryExpensesBll elementToUpdate)
        {
            salaryExpenses salaryExpenses = Mapper.Map<SalaryExpensesBll, salaryExpenses>(elementToUpdate);

            _unitOfWork.salaryExpensesUnitOfWork.Update(salaryExpenses);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<SalaryExpensesBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<SalaryExpensesBll>>(await _unitOfWork.salaryExpensesUnitOfWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.dateExpenses.Value) == datepresentDay.Date), "expenseCategory"));
        }

        public async Task<IEnumerable<SalaryExpensesBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<SalaryExpensesBll>>(await _unitOfWork.salaryExpensesUnitOfWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.dateExpenses.Value) >= startDate.Date
                                                                                                                        && (DbFunctions.TruncateTime(x.dateExpenses.Value) <= finalDate.Date)), "expenseCategory"));
        }
    }
}
