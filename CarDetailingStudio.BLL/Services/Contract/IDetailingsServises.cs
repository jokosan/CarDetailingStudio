using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IDetailingsServises
    {
        double? ConvertCurrency(double? usd, double privat);
        Task<IEnumerable<DetailingsBll>> Converter();
        Task<IEnumerable<DetailingsBll>> GetAll();
        Task<IEnumerable<DetailingsBll>> SelectTypeService(int idTypeService);
        Task AddNewServices(DetailingsBll prive);
        Task UpdateServices(DetailingsBll updateServices);
        Task<DetailingsBll> GetId(int? id);
        Task<IEnumerable<DetailingsBll>> GetPriceGroupWashServices(int idGroup);
    }
}