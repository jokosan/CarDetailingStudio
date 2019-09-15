using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.IServices
{
    public interface IServices<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T GetId(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
