using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IGroupWashServices
    {
        IEnumerable<GroupWashServicesBll> GetAllTable();
        IEnumerable<GroupWashServicesBll> GetIdAll(int? id);
    }
}
