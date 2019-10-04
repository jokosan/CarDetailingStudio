using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

    }
}
