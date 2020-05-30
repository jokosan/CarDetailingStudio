using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IGetInclude<T> where T : class
    {
        IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string children);
        IEnumerable<T> GetInclude(string children);
        T IdInclude(int id);
    }
}
