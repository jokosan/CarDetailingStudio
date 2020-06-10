using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    public interface IGetFromDatabase<T> where T : class
    {
        Task<IEnumerable<T>> GetTableAll();
        Task<T> SelectId(int? elementId);
    }
}
