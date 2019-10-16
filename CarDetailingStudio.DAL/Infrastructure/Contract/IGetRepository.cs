using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IGetRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T GetById(int? id);
    }
}
