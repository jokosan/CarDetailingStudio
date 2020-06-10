using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderCarpetWashingServices : IGetFromDatabase<OrderCarpetWashingBll>, IDatabaseOperations<OrderCarpetWashingBll>
    {
        Task<IEnumerable<OrderCarpetWashingBll>> GetIncludeWhere();
    }
}
