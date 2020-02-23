using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;

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

        public  void ShiftStatus()
        {
            DateTime dateTime = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            CurrentShift(dateTime);
        }

        public void CurrentShift(DateTime date)
        {
            var ordersFulfilled = TableCalculationStatusFolse().Where(x => x.OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy") == date.ToString("dd.MM.yyyy"));
            int test = ordersFulfilled.Count();

            if (DateTime.Now != date)
            {
                statusFalse = ordersFulfilled.Any(x => x.CalculationStatus == false);
            }

            if (statusFalse)
            {
                var DayClose = DayResultViewInfo();

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

                        OrderCarWashWorkers orderCarWash = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(orderCarWashWorkers);
                        _unitOfWork.OrderCarWasWorkersUnitOFWork.Update(orderCarWash);
                        _unitOfWork.Save();
                    }
                }
            }
        }

        public IEnumerable<DayResultModelBll> DayResultViewInfo()
        {
            var resultGetInclud = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => x.CalculationStatus == false, "CarWashWorkers"));
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

        private IEnumerable<OrderCarWashWorkersBll> TableCalculationStatusFolse()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => x.CalculationStatus == false, "OrderServicesCarWash"));
        }
    }
}
