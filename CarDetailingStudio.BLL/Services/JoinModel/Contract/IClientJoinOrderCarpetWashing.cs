using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.JoinModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.JoinModel.Contract
{
    public interface IClientJoinOrderCarpetWashing
    {
        Task<IEnumerable<ClientJoinCarpetWashingModel>> ActiveOrdersWashingCarpets();
        Task<IEnumerable<ClientJoinCarpetWashingModel>> ActiveOrdersCarpets();
    }
}
