using CarDetailingStudio.BLL.Model.ModelViewBll;

namespace CarDetailingStudio.BLL.Services.Modules.TireStorage.Contract
{
    public interface IReviwOrderModules
    {
        ReviwOrderModelBll ReviwOrder(OrderTireStorageModelBll tireStorage);
    }
}
