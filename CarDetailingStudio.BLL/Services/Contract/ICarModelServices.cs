using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarModelServices
    {
        IEnumerable<CarModelBll> GetWhere(int id);
    }
}