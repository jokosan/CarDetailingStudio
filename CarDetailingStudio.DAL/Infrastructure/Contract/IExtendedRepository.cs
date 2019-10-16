using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IExtendedRepository<T> where T : class
    {       
        IEnumerable<T> GetWhere(Func<T, bool> predicate);      
     
    }
}
