using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;

namespace CarDetailingStudio.BLL.Services.Expenses.ExpensesContract
{
    public interface IConsumablesTireFitting : IGetFromDatabase<ConsumablesTireFittingBll>, IDatabaseOperations<ConsumablesTireFittingBll>
    {
    }
}
