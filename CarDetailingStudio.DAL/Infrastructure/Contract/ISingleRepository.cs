using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface ISingleRepository<T> where T : class
    {
        Task<IEnumerable<T>> QueryObjectGraph(Expression<Func<T, bool>> filter);
    }
}
