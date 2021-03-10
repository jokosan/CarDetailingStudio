using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift
{
    public class CloseShiftModule : ICloseShiftModule
    {
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private IDayResult _dayResult;
        private IBonusModules _bonusModules;
        private ICarWashWorkersServices _carWashWorkers;

        public CloseShiftModule(
            IOrderCarWashWorkersServices orderCarWashWorkers,
            IDayResult dayResult,
            IBonusModules bonusModules,
            ICarWashWorkersServices carWashWorkers)
        {
            _orderCarWashWorkers = orderCarWashWorkers;
            _dayResult = dayResult;
            _bonusModules = bonusModules;
            _carWashWorkers = carWashWorkers;
        }

        public async Task CurrentShift()
        {
            var ordersFulfilled = await _orderCarWashWorkers.SampleForPayroll(DateTime.Now);
            var DayClose = await _dayResult.DayResultViewInfo();

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            foreach (var itemShift in DayClose)
            {
                var resultShift = ordersFulfilled.Where(x => x.IdCarWashWorkers == itemShift.carWashWorkersId);

                foreach (var itemOrder in resultShift)
                {
                    orderCarWashWorkers.Id = itemOrder.Id;
                    orderCarWashWorkers.IdOrder = itemOrder.IdOrder;
                    orderCarWashWorkers.IdCarWashWorkers = itemOrder.IdCarWashWorkers;
                    orderCarWashWorkers.CalculationStatus = false;
                    orderCarWashWorkers.Payroll = itemOrder.Payroll;
                    orderCarWashWorkers.closedDayStatus = true;
                    orderCarWashWorkers.typeServicesId = itemOrder.typeServicesId;

                    await _orderCarWashWorkers.UpdateOrderCarWashWorkers(orderCarWashWorkers);
                }

                var resultCarWashWorkers = await _carWashWorkers.CarWashWorkersId(itemShift.carWashWorkersId);
                var InterestRate = (double)resultCarWashWorkers.InterestRate.Value / 100;

                if (resultCarWashWorkers.IdPosition >= 3)
                {
                    await _bonusModules.PremiumAccrual(itemShift.carWashWorkersId, itemShift.payroll.Value / InterestRate);
                }
                else
                {
                    var bonusAdmin = await _orderCarWashWorkers.SampleForPayroll(itemShift.carWashWorkersId, DateTime.Now);
                   
                    var test1 = bonusAdmin.Where(x => x.typeServicesId == 4).Sum(s => s.Payroll);
                    var test2 = bonusAdmin.Where(x => x.typeServicesId == 6).Sum(s => s.Payroll);
                    var test3 = bonusAdmin.Where(x => x.typeServicesId == 8).Sum(s => s.Payroll);
                    var test4 = bonusAdmin.Where(x => x.typeServicesId == 10).Sum(s => s.Payroll);

                    var result = test1 + test2 + test3 + test4;

                    // await _bonusModules.PremiumAccrual(itemShift.carWashWorkersId, itemShift.payroll.Value / InterestRate);
                    await _bonusModules.PremiumAccrual(itemShift.carWashWorkersId, result.Value / InterestRate);
                }
            }
        }

        private async Task BonusAdd(int carWashWorkers, double payroll) => await _bonusModules.PremiumAccrual(carWashWorkers, payroll);
    }
}
