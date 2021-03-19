using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    public interface IReportsId<T> where T : class
    {
        Task<IEnumerable<T>> Reports(int id, DateTime datepresentDay);
        Task<IEnumerable<T>> Reports(int id, DateTime startDate, DateTime finalDate);
    }
}
