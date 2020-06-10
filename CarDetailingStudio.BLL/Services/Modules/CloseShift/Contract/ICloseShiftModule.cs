using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface ICloseShiftModule
    {
        Task CurrentShift();
    }
}