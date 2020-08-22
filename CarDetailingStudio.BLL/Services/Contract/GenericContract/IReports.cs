using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    public interface IReports<T> where T : class
    {
        Task<IEnumerable<T>> Reports(DateTime datepresentDay);
        Task<IEnumerable<T>> Reports(DateTime startDate, DateTime finalDate);
    }
}
