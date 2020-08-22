using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using Quartz;
using System.Threading.Tasks;

namespace CarDetailingStudio.Utilities.Quartz
{
    public class AutomaticShiftClose : IJob
    {
        private ICloseShiftModule _closeShiftModule;

        public AutomaticShiftClose(ICloseShiftModule closeShiftModule)
        {
            _closeShiftModule = closeShiftModule;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _closeShiftModule.CurrentShift();
        }
    }
}