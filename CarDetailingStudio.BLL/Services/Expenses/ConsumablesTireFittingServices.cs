using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class ConsumablesTireFittingServices : IConsumablesTireFitting
    {
        private IUnitOfWork _unitOfWork;

        public ConsumablesTireFittingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ConsumablesTireFittingBll> SelectId(int? elementId)
        {
            return Mapper.Map<ConsumablesTireFittingBll>(await _unitOfWork.consumablesTireFittingUnitOfWork.GetInclude("expenseCategory"));
        }

        public async Task<IEnumerable<ConsumablesTireFittingBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<ConsumablesTireFittingBll>>(await _unitOfWork.consumablesTireFittingUnitOfWork.Get());
        }

        public async Task Insert(ConsumablesTireFittingBll element)
        {
            consumablesTireFitting consumablesTireFittings = Mapper.Map<ConsumablesTireFittingBll, consumablesTireFitting>(element);

            _unitOfWork.consumablesTireFittingUnitOfWork.Insert(consumablesTireFittings);
            await _unitOfWork.Save();
        }

        public async Task Update(ConsumablesTireFittingBll elementToUpdate)
        {
            consumablesTireFitting consumablesTireFittings = Mapper.Map<ConsumablesTireFittingBll, consumablesTireFitting>(elementToUpdate);

            _unitOfWork.consumablesTireFittingUnitOfWork.Update(consumablesTireFittings);
            await _unitOfWork.Save();
        }
    }
}
