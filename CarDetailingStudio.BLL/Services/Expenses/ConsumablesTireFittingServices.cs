using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;

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

        public void Insert(ConsumablesTireFittingBll element)
        {
            consumablesTireFitting consumablesTireFittings = Mapper.Map<ConsumablesTireFittingBll, consumablesTireFitting>(element);

            _unitOfWork.consumablesTireFittingUnitOfWork.Insert(consumablesTireFittings);
            _unitOfWork.Save();
        }

        public void Update(ConsumablesTireFittingBll elementToUpdate)
        {
            consumablesTireFitting consumablesTireFittings = Mapper.Map<ConsumablesTireFittingBll, consumablesTireFitting>(elementToUpdate);

            _unitOfWork.consumablesTireFittingUnitOfWork.Update(consumablesTireFittings);
            _unitOfWork.Save();
        }
    }
}
