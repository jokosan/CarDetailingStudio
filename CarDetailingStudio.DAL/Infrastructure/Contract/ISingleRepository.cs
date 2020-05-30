using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface ISingleRepository<T> where T : class
    {
        IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter);
    }
}
