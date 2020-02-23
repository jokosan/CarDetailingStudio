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
    public class ConsumablesTireFittingServices : IConsumablesTireFitting
    {
        private IUnitOfWork _unitOfWork;

        public ConsumablesTireFittingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ConsumablesTireFittingBll SelectId(int? elementId)
        {
            return Mapper.Map<ConsumablesTireFittingBll>(_unitOfWork.consumablesTireFittingUnitOfWork.GetInclude("expenseCategory"));
        }

        public IEnumerable<ConsumablesTireFittingBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<ConsumablesTireFittingBll>>(_unitOfWork.consumablesTireFittingUnitOfWork.Get());
        }
    }
}
