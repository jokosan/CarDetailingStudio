using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarMarkServices
    {
        Task<IEnumerable<CarMarkBll>> Get();
        Task<IEnumerable<CarMarkBll>> GetWhere(string id);
    }
}