using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarMarkServices
    {
        IEnumerable<CarMarkBll> Get();
        IEnumerable<CarMarkBll> GetWhere(string id);
        IEnumerable<CarMarkBll> GetInclude();
    }
}