namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    public interface IDeleteFromDatabase<T> where T : class
    {
        void Delete(T elementToDelete);
    }
}
