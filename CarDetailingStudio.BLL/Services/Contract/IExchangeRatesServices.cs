using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public interface IExchangeRatesServices
    {
        Task Insert(List<ExchangeRatesBll> exchangeRates);
        Task<IEnumerable<ExchangeRatesBll>> GetAll();
        Task UpdateTable();
        Task<bool> CheckForUpdate();
        Task UpdateListExchangeRates();
    }
}