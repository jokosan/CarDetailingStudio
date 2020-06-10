using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    public interface IDeleteFromDatabase<T> where T : class
    {
        Task Delete(T elementToDelete);
    }
}
