using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Filters
{
    public class ShiftClosingStatus
    {
        private IUnitOfWork _unitOfWork;
        private bool statusFalse;

        public ShiftClosingStatus()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task ShiftStatus()
        {
            DateTime dateTime = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            await CurrentShift(dateTime);
        }

        public async Task CurrentShift(DateTime date)
        {
            var ordersFulfilled = await TableCalculationStatusFolse();
            var ordersFulfilledWhere = ordersFulfilled.Where(x => x.OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy") == date.ToString("dd.MM.yyyy"));

            if (DateTime.Now != date)
            {
                statusFalse = ordersFulfilledWhere.Any(x => x.CalculationStatus == false);
            }

            if (statusFalse)
            {
                var DayClose = await DayResultViewInfo();

                OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

                foreach (var itemShift in DayClose)
                {
                    var resultShift = ordersFulfilledWhere.Where(x => x.IdCarWashWorkers == itemShift.carWashWorkersId);

                    foreach (var itemOrder in resultShift)
                    {
                        orderCarWashWorkers.Id = itemOrder.Id;
                        orderCarWashWorkers.IdOrder = itemOrder.IdOrder;
                        orderCarWashWorkers.IdCarWashWorkers = itemOrder.IdCarWashWorkers;
                        orderCarWashWorkers.CalculationStatus = false;
                        orderCarWashWorkers.Payroll = itemOrder.Payroll;
                        orderCarWashWorkers.closedDayStatus = true;
                        orderCarWashWorkers.typeServicesId = itemOrder.typeServicesId;

                        OrderCarWashWorkers orderCarWash = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(orderCarWashWorkers);
                        _unitOfWork.OrderCarWasWorkersUnitOFWork.Update(orderCarWash);
                        await _unitOfWork.Save();
                    }
                }
            }
        }

        private async Task<IEnumerable<OrderCarWashWorkersBll>> TableCalculationStatusFolse()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => x.CalculationStatus == false, "OrderServicesCarWash"));
        }

        public async Task<IEnumerable<DayResultModelBll>> DayResultViewInfo()
        {
            var resultGetInclud = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.closedDayStatus == false)
                                                                                                                               && (x.CalculationStatus == false), "CarWashWorkers"));
            return resultGetInclud.GroupBy(x => x.IdCarWashWorkers)
                                  .Select(y => new DayResultModelBll
                                  {
                                      carWashWorkersId = y.First().CarWashWorkers.id,
                                      orderCount = y.Count(),
                                      surname = y.First().CarWashWorkers.Surname,
                                      name = y.First().CarWashWorkers.Name,
                                      patronymic = y.First().CarWashWorkers.Patronymic,
                                      calculationStatus = y.First().CalculationStatus,
                                      payroll = y.Sum(c => c.Payroll)
                                  });
        }
    }
}
