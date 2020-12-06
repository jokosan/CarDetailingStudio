using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract
{
    public interface IPriceListTireFitting : IGetFromDatabase<PriceListTireFittingBll>, IDatabaseOperations<PriceListTireFittingBll>
    {
        Task<IEnumerable<PriceListTireFittingBll>> SelectValueFromThePriceList(List<int> id);
        Task<IEnumerable<PriceListTireFittingBll>> SelectRadius(List<int> idRadius, int typyCar);
        Task<IEnumerable<PriceListTireFittingBll>> SelectId(int? id, int? typeCar);
    }
}
