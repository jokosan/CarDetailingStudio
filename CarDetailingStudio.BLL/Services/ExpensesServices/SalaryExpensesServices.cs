using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.ExpensesServices
{
    public class SalaryExpensesServices : ISalaryExpenses
    {
        private IUnitOfWork _unitOfWork;

        public SalaryExpensesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SalaryExpensesBll>> PayrollExpensesPerMonth(int? id, int month, int year) =>
            Mapper.Map<IEnumerable<SalaryExpensesBll>>(await _unitOfWork.salaryExpensesUnitOfWork.QueryObjectGraph(x =>
                                                        x.idCarWashWorkers == id.Value
                                                        && x.Expenses.dateExpenses.Value.Month == month
                                                        && x.Expenses.dateExpenses.Value.Year == year
                                                        , "Expenses"));

        public async Task<IEnumerable<SalaryExpensesBll>> GetTableAll() =>
            Mapper.Map<IEnumerable<SalaryExpensesBll>>(await _unitOfWork.salaryExpensesUnitOfWork.GetInclude("CarWashWorkers"));

        public async Task<SalaryExpensesBll> SelectId(int? elementId) =>
            Mapper.Map<SalaryExpensesBll>(await _unitOfWork.salaryExpensesUnitOfWork.GetById(elementId));


        public async Task Insert(IEnumerable<SalaryExpensesBll> element)
        {
            var salary = Mapper.Map<IEnumerable<SalaryExpensesBll>, IEnumerable<salaryExpenses>>(element);

            _unitOfWork.salaryExpensesUnitOfWork.Insert(salary.ToList());
            await _unitOfWork.Save();
        }

        public async Task Update(SalaryExpensesBll elementToUpdate)
        {
            _unitOfWork.salaryExpensesUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        public async Task Insert(SalaryExpensesBll element)
        {
            _unitOfWork.salaryExpensesUnitOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        public salaryExpenses TransformAnEntity(SalaryExpensesBll entity) => Mapper.Map<SalaryExpensesBll, salaryExpenses>(entity);

        public async Task<IEnumerable<SalaryExpensesBll>> Reports(DateTime datepresentDay) =>
                         Mapper.Map<IEnumerable<SalaryExpensesBll>>(await _unitOfWork.salaryExpensesUnitOfWork.QueryObjectGraph(x =>
                                 (DbFunctions.TruncateTime(x.Expenses.dateExpenses.Value) == datepresentDay.Date), "CarWashWorkers", "Expenses", "Expenses.costCategories"));

        public async Task<IEnumerable<SalaryExpensesBll>> Reports(DateTime startDate, DateTime finalDate) =>
                             Mapper.Map<IEnumerable<SalaryExpensesBll>>(await _unitOfWork.salaryExpensesUnitOfWork.QueryObjectGraph(x =>
                                   (DbFunctions.TruncateTime(x.Expenses.dateExpenses.Value) >= startDate.Date)
                                    && (DbFunctions.TruncateTime(x.Expenses.dateExpenses.Value) >= finalDate.Date), "CarWashWorkers", "Expenses", "Expenses.costCategories"));
    }
}
