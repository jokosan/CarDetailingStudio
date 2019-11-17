using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IDetailingsServises
    {
        double? ConvertCurrency(double? usd, double privat);
        IEnumerable<DetailingsBll> Converter();
        IEnumerable<DetailingsBll> GetAll();
        void AddNewServices(DetailingsBll prive);
        void UpdateServices(DetailingsBll updateServices);
        DetailingsBll GetId(int? id);
    }
}