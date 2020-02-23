using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    public interface IGetFromDatabase<T> where T : class
    {
        IEnumerable<T> GetTableAll();
        T SelectId(int? elementId);
    }
}
