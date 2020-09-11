using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using DevExpress.Data.Helpers;
using DevExpress.Data.Mask;
using DevExpress.Schedule;
using DevExpress.Utils.Design;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderCarWashWorkersServices : IOrderCarWashWorkersServices
    {
        private IUnitOfWork _unitOfWork;
        private IBrigadeForTodayServices _brigade;
        private IServisesCarWashOrderServices _servisesCarWash;
        private AutomapperConfig _automapper;
        private OrderCarWashWorkersBll _orderCarBrigade;

        public OrderCarWashWorkersServices(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig,
                                           OrderCarWashWorkersBll orderCar, IBrigadeForTodayServices brigade,
                                           IServisesCarWashOrderServices servisesCarWash)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapperConfig;
            _orderCarBrigade = orderCar;
            _brigade = brigade;
            _servisesCarWash = servisesCarWash;
        }


        public async Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(DateTime dateTime)
        {
            var getResult = await TableCalculationStatusFolse();
            return getResult.Where(x => x.OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy") == dateTime.ToString("dd.MM.yyyy"));
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(int id, DateTime date)
        {
            var getResult = await TableCalculationStatusFolse();
            return getResult.Where(x => (x.IdCarWashWorkers == id) && (x.OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy") == date.ToString("dd.MM.yyyy")));
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> TableCalculationStatusFolse()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => x.CalculationStatus == false, "OrderServicesCarWash"));
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(int? IdCarWashWorkers)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.IdCarWashWorkers == IdCarWashWorkers)
                                                                                                                         && (x.CalculationStatus == false), "OrderServicesCarWash"));
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> GetTableInclud()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.closedDayStatus == true)
                                                                                                                               && (x.CalculationStatus == false), "CarWashWorkers"));
        }

        public async Task<OrderCarWashWorkersBll> Change(int? Order, int? Employee)
        {
            var getResult = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.GetWhere(x => (x.IdOrder == Order) && (x.IdCarWashWorkers == Employee)));

            return getResult.First(x => x.IdCarWashWorkers == Employee);
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> GetTableInclud(int? idCarWashWorkers)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => x.IdOrder == idCarWashWorkers, "CarWashWorkers"));
        }
        // Закрыть текущий день 
        public async Task<IEnumerable<OrderCarWashWorkersBll>> GetClosedDay()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.closedDayStatus == false)
                                                                                                                               && (x.CalculationStatus == false), "CarWashWorkers", "OrderServicesCarWash"));
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> GetClosedDay(int? id, DateTime? date)
        {
            var getResult = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.IdCarWashWorkers == id),
                                                                                                                                //   && (x.closedDayStatus == true)
                                                                                                                                //   && (x.CalculationStatus == false),
                                                                                                                                "OrderServicesCarWash"));

            return getResult.Where(d => d.OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy") == date?.ToString("dd.MM.yyyy"));
        }

        public async Task SaveOrderCarWashWorkers(OrderCarWashWorkersBll orderCarWash)
        {
            OrderCarWashWorkers orderCarWashWorkers = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(orderCarWash);

            _unitOfWork.OrderCarWasWorkersUnitOFWork.Insert(orderCarWashWorkers);
            await _unitOfWork.Save();
        }

        public async Task UpdateOrderCarWashWorkers(OrderCarWashWorkersBll orderCarWash)
        {
            OrderCarWashWorkers orderCarWashWorkers = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(orderCarWash);

            _unitOfWork.OrderCarWasWorkersUnitOFWork.Update(orderCarWashWorkers);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> DailyEmployeeOrders(int? carWashWorkersId, DateTime date)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => DbFunctions.TruncateTime(x.OrderServicesCarWash.ClosingData.Value) == date.Date
                                                                                                                                && (x.IdCarWashWorkers == carWashWorkersId),
                                                                                                                               "OrderServicesCarWash", "CarWashWorkers"));
        }

        #region Отчеты за день и месяц
        public async Task<IEnumerable<OrderCarWashWorkersBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) == datepresentDay.Date),
                                                                                                                                         "OrderServicesCarWash", "CarWashWorkers", "CarWashWorkers.brigadeForToday"));
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) >= startDate.Date
                                                                                                                                    && (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) <= finalDate.Date)),
                                                                                                                                   "OrderServicesCarWash", "CarWashWorkers", "CarWashWorkers.brigadeForToday"));
        }
        #endregion

        #region
        private DateTime final { get; set; }

        public async Task<IEnumerable<OrderCarWashWorkersDayGroupBll>> OrderCarWashWorkers(int? id, DateTime startDate, DateTime? finalDate)
        {
            if (finalDate != null)
            {
                final = finalDate.Value;
            }

            if (id != null)
            {
                if (finalDate == null)
                {
                    var result = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.IdCarWashWorkers == id) &&
                                                                                                                                        (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) == startDate.Date),
                                                                                                                                        "OrderServicesCarWash", "CarWashWorkers"));
                    return OrderCarWashWorkersDayGroup(result);
                }
                else
                {
                    var result = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.IdCarWashWorkers == id) &&
                                                                                                                                      (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) >= startDate.Date) &&
                                                                                                                                      (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) <= final.Date),
                                                                                                                                       "OrderServicesCarWash", "CarWashWorkers"));
                    return OrderCarWashWorkersDayGroup(result);
                }
            }
            else
            {
                if (finalDate == null)
                {

                    var result = await Reports(startDate);
                    return OrderCarWashWorkersDayGroup(result);
                }
                else
                {
                    var result = await Reports(startDate, finalDate.Value);
                    return OrderCarWashWorkersDayGroup(result);
                }
            }
        }

        private IEnumerable<OrderCarWashWorkersDayGroupBll> OrderCarWashWorkersDayGroup(IEnumerable<OrderCarWashWorkersBll> order)
        {
            return order.GroupBy(o => new { o.IdCarWashWorkers, o.OrderServicesCarWash.ClosingData.Value.Date })
                .Select(t1 => new OrderCarWashWorkersDayGroupBll
                {
                    Id = t1.First().Id,
                    IdOrder = t1.First().IdOrder,
                    IdCarWashWorkers = t1.First().IdCarWashWorkers,
                    Name = t1.First().CarWashWorkers.Name,
                    Surname = t1.First().CarWashWorkers.Surname,
                    Patronymic = t1.First().CarWashWorkers.Patronymic,
                    OrderDate = t1.First().OrderServicesCarWash.OrderDate,
                    CountOrder = t1.Count(),
                    ClosingData = t1.First().OrderServicesCarWash.ClosingData,
                    Payroll = t1.Sum(c => c.Payroll),
                    TotalCostOfAllServices = t1.Sum(c => c.OrderServicesCarWash.TotalCostOfAllServices),
                    DiscountPrice = t1.Sum(c => c.OrderServicesCarWash.DiscountPrice)
                });
        }

        public async Task<IEnumerable<OrderCarWashWorkersDayGroupBll>> WhereCarWashWorkers(int keyCarWashWorkers)
        {
            var result = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork
                                                                                          .QueryObjectGraph(x => x.IdCarWashWorkers == keyCarWashWorkers,
                                                                                           "OrderServicesCarWash", "CarWashWorkers"));
            return OrderCarWashWorkersDayGroup(result);
        }

        #endregion
    }
}

