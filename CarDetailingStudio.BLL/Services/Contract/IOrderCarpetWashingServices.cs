using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderCarpetWashingServices : IGetFromDatabase<OrderCarpetWashingBll>, IDatabaseOperations<OrderCarpetWashingBll>
    {
        IEnumerable<OrderCarpetWashingBll> GetIncludeWhere();
    }
}
