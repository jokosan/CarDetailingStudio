using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarModelServices
    {
        Task<IEnumerable<CarModelBll>> GetWhere(int id);
    }
}