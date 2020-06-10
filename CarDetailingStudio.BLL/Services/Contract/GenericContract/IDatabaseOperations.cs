using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    public interface IDatabaseOperations<T> where T : class
    {
        Task Insert(T element);
        Task Update(T elementToUpdate);
    }
}
