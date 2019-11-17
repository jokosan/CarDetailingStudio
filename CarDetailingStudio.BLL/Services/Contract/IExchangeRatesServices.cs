using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services
{
    public interface IExchangeRatesServices
    {      
        void Insert(List<ExchangeRatesBll> exchangeRates);
        IEnumerable<ExchangeRatesBll> GetAll();
    }
}