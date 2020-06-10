using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.TireStorage.Contract
{
    public interface IReviwOrderModules
    {
        Task<ReviwOrderModelBll> ReviwOrder(OrderTireStorageModelBll tireStorage);
    }
}
