using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IDefaultRepository<T> where T : class
    {      
        void Insert(T entity);
        void Insert(List<T> entity);
        void Update(T entityToUpdate);
        void Delete(object id);
        void Delete(T entityToDelete);
        void AttachStubs(object[] stub);       
     
    }
}
