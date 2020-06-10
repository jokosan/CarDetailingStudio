using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IGetInclude<T> where T : class
    {
        Task<IEnumerable<T>> QueryObjectGraph(Expression<Func<T, bool>> filter, string children);
        Task<IEnumerable<T>> GetInclude(string children);
        Task<T> IdInclude(int id);
    }
}
