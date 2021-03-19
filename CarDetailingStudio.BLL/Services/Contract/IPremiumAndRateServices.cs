using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IPremiumAndRateServices : IGetFromDatabase<PremiumAndRateBll>, IDatabaseOperations<PremiumAndRateBll>
    {
        Task<IEnumerable<PremiumAndRateBll>> SelectPosition(int carWashWorkersId);
        Task CreatePremiumAndRateServices(int position, int carWashWorkersId);
        Task<IEnumerable<PremiumAndRateBll>> AllCurrentEmployees();
    }
}
