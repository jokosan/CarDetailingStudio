using System.Collections.Generic;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IGetRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T GetById(int? id);
    }
}
