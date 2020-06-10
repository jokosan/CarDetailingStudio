using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IExtendedRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
    }
}
