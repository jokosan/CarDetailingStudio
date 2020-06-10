using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IJobTitleTableServices
    {
       Task<IEnumerable<JobTitleTableBll>> SelectJobTitle();
    }
}
