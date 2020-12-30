using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ISalaryArchive : IDatabaseOperations<SalaryArchiveBll>, IGetFromDatabase<SalaryArchiveBll>
    {
        Task<IEnumerable<SalaryArchiveBll>> MonthlySalary(int idCarWashWorkers, int month, int year);
        Task<IEnumerable<SalaryArchiveBll>> MonthlySalary(int month, int year);
        Task<IEnumerable<SalaryArchiveBll>> SelectCarWashWorkers(int idCarWashWorkers);
        Task<SalaryArchiveBll> LastMonth(int idCarWash);
    }
}
