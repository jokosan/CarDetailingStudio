using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IGetRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int? id);
    }
}
