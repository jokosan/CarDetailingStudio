using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class SalaryExpensesServices : ISalaryExpenses
    {
        private IUnitOfWork _unitOfWork;

        public SalaryExpensesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SalaryExpensesBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<SalaryExpensesBll>>(_unitOfWork.salaryExpensesUnitOfWork.GetInclude("CarWashWorkers"));
        }

        public SalaryExpensesBll SelectId(int? elementId)
        {
            return Mapper.Map<SalaryExpensesBll>(_unitOfWork.salaryExpensesUnitOfWork.GetById(elementId));
        }

        public void Insert(IEnumerable<SalaryExpensesBll> element)
        {
            var salary = Mapper.Map<IEnumerable<SalaryExpensesBll>, IEnumerable<salaryExpenses>>(element);

            _unitOfWork.salaryExpensesUnitOfWork.Insert(salary.ToList());
            _unitOfWork.Save();
        }

        public void Insert(SalaryExpensesBll element)
        {
            throw new NotImplementedException();
        }


        public void Update(SalaryExpensesBll elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
