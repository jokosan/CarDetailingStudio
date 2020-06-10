using CarDetailingStudio.BLL.Model;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IClientModules
    {
        Task<int> Distribute(ClientViewsBll client);
    }
}
