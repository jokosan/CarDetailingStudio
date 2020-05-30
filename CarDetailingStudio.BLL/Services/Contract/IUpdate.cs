namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IUpdate<T> where T : class
    {
        void UpdateState(T entry);
    }
}
