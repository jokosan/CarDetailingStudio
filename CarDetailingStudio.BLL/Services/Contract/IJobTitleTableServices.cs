using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IJobTitleTableServices
    {
        IEnumerable<JobTitleTableBll> SelectJobTitle();
    }
}
