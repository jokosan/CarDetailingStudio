using System;
using System.Collections.Generic;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        void Delete(object id);
        void Delete(T entityToDelete);
        IEnumerable<T> Get();
        T GetById(int? id);
        void Insert(T entity);
        void Update(T entityToUpdate);
        void AttachStubs(object[] stub);
        IEnumerable<T> GetWhere(Func<T, bool> predicate);
        void Insert(List<T> entity);
        void Update(List<T> entityUpdate);

        // IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string children);


    }
}
