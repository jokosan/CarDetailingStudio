using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarMarkServices
    {
        IEnumerable<CarMarkBll> Get();
        IEnumerable<CarMarkBll> GetWhere(string id);
        IEnumerable<CarMarkBll> GetInclude();
    }
}