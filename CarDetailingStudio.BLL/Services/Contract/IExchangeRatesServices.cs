using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services
{
    public interface IExchangeRatesServices
    {
        void Insert(List<ExchangeRatesBll> exchangeRates);
        IEnumerable<ExchangeRatesBll> GetAll();
        void UpdateTable();
        bool CheckForUpdate();
        void UpdateListExchangeRates();
    }
}