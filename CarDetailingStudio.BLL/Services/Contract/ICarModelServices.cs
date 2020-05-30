using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarModelServices
    {
        IEnumerable<CarModelBll> GetWhere(int id);
    }
}