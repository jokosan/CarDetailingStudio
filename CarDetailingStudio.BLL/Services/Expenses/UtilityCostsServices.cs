using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class UtilityCostsServices : IUtilityCosts
    {
        private IUnitOfWork _unitOfWork;

        public UtilityCostsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UtilityCostsBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<UtilityCostsBll>>(_unitOfWork.utilityCostsUnitOfWork.GetInclude("expenseCategory"));
        }

        public UtilityCostsBll SelectId(int? elementId)
        {
            return Mapper.Map<UtilityCostsBll>(_unitOfWork.utilityCostsUnitOfWork.GetById(elementId));
        }

        public void Insert(UtilityCostsBll element)
        {
            utilityCosts utilityCosts = Mapper.Map<UtilityCostsBll, utilityCosts>(element);

            _unitOfWork.utilityCostsUnitOfWork.Insert(utilityCosts);
            _unitOfWork.Save();
        }


        public void Update(UtilityCostsBll elementToUpdate)
        {
            utilityCosts utilityCosts = Mapper.Map<UtilityCostsBll, utilityCosts>(elementToUpdate);

            _unitOfWork.utilityCostsUnitOfWork.Update(utilityCosts);
            _unitOfWork.Save();
        }

        public IEnumerable<UtilityCostsBll> MonthlyReport(DateTime date)
        {
            return Mapper.Map<IEnumerable<UtilityCostsBll>>(_unitOfWork.utilityCostsUnitOfWork.GetWhere(x => x.dateExpenses?.Month == date.Month, "expenseCategory"));
        }
    }
}
