using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift
{
    public class DayResult : IDayResult
    {
        private IOrderCarWashWorkersServices _orderCarWashWorkersServices;
        private IOrderServicesCarWashServices _orderServicesCarWash;

        public DayResult(IOrderCarWashWorkersServices orderCarWashWorkersServices, IOrderServicesCarWashServices orderServicesCarWash)
        {
            _orderCarWashWorkersServices = orderCarWashWorkersServices;
            _orderServicesCarWash = orderServicesCarWash;
        }

        public IEnumerable<DayResultModelBll> DayResultViewInfo()
        {
            var resultGetInclud = _orderCarWashWorkersServices.GetClosedDay();
            return GrupDayResult(resultGetInclud);
        }

        public IEnumerable<DayResultModelBll> TotalForEachEmployee()
        {
            var result = _orderCarWashWorkersServices.GetTableInclud();
            return GrupDayResult(result);
        }

        private IEnumerable<DayResultModelBll> GrupDayResult(IEnumerable<OrderCarWashWorkersBll> getResult)
        {
            return getResult.GroupBy(x => x.IdCarWashWorkers)
                                  .Select(y => new DayResultModelBll
                                  {
                                      carWashWorkersId = y.First().CarWashWorkers.id,
                                      orderCount = y.Count(),
                                      surname = y.First().CarWashWorkers.Surname,
                                      name = y.First().CarWashWorkers.Name,
                                      patronymic = y.First().CarWashWorkers.Patronymic,
                                      calculationStatus = y.First().CalculationStatus,
                                      payroll = y.Sum(c => c.Payroll),
                                      salaryDate = test(y.First().salaryDate)

                                  });
        }

        public DateTime test(DateTime? date) => Convert.ToDateTime(date?.ToString("dd.MM.yyyy"));

    }
}
