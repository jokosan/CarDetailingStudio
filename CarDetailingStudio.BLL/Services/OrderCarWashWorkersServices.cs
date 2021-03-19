using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
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

        public OrderCarWashWorkersServices(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> SelectMonth(int? id, int month, int year) =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork
                      .QueryObjectGraph(x => (x.IdCarWashWorkers == id)
                       && (x.OrderServicesCarWash.OrderDate.Value.Month == month)
                       && (x.OrderServicesCarWash.OrderDate.Value.Year == year), "OrderServicesCarWash", "CarWashWorkers"));

        // Пересмотреть
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

        public async Task<IEnumerable<OrderCarWashWorkersBll>> TableCalculationStatusFolse() =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                x.CalculationStatus == false, "OrderServicesCarWash", "OrderServicesCarWash.ClientsOfCarWash"));

        public async Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(int? IdCarWashWorkers) =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                (x.IdCarWashWorkers == IdCarWashWorkers)
                && (x.CalculationStatus == false),
                "OrderServicesCarWash"));

        public async Task<IEnumerable<OrderCarWashWorkersBll>> GetTableInclud(int month, int year) =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                                                           // (x.closedDayStatus == true) &&
                                                           (x.CalculationStatus == false) &&
                                                           (x.CarWashWorkers.status == true) &&
                                                           (x.OrderServicesCarWash.OrderDate.Value.Month == month) &&
                                                           (x.OrderServicesCarWash.OrderDate.Value.Year == year),
                                                           "OrderServicesCarWash", "CarWashWorkers"));

        public async Task<OrderCarWashWorkersBll> Change(int? Order, int? Employee)
        {
            var getResult = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.GetWhere(x =>
                                                                                (x.IdOrder == Order) && (x.IdCarWashWorkers == Employee)));

            return getResult.First(x => x.IdCarWashWorkers == Employee);
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> GetTableInclud(int? idCarWashWorkers)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                                                                                x.IdOrder == idCarWashWorkers, "CarWashWorkers"));
        }

        public async Task<IEnumerable<OrderCarWashWorkersBll>> DailyEmployeeOrders(int? carWashWorkersId, DateTime date) =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                DbFunctions.TruncateTime(x.OrderServicesCarWash.ClosingData.Value) == date.Date
                && (x.IdCarWashWorkers == carWashWorkersId)
                , "OrderServicesCarWash", "CarWashWorkers"));

        #region Отчеты за день и месяц
        public async Task<IEnumerable<OrderCarWashWorkersBll>> Reports(DateTime datepresentDay) =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                    (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) == datepresentDay.Date)
                    , "OrderServicesCarWash", "CarWashWorkers", "OrderServicesCarWash.ClientsOfCarWash"));

        public async Task<IEnumerable<OrderCarWashWorkersBll>> Reports(DateTime startDate, DateTime finalDate) =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                    (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) >= startDate.Date
                    && (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) <= finalDate.Date)),
                    "OrderServicesCarWash", "CarWashWorkers", "OrderServicesCarWash.ClientsOfCarWash"));

        public async Task<IEnumerable<OrderCarWashWorkersBll>> Reports(int id, DateTime datepresentDay) =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                    (x.IdCarWashWorkers == id)
                    && (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) == datepresentDay.Date)
                    , "OrderServicesCarWash", "CarWashWorkers", "OrderServicesCarWash.ClientsOfCarWash"));

        public async Task<IEnumerable<OrderCarWashWorkersBll>> Reports(int id, DateTime startDate, DateTime finalDate) =>
            Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x =>
                      (x.IdCarWashWorkers == id)
                      && (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) >= startDate.Date
                      && (DbFunctions.TruncateTime(x.OrderServicesCarWash.OrderDate.Value) <= finalDate.Date))
                      , "OrderServicesCarWash", "CarWashWorkers", "OrderServicesCarWash.ClientsOfCarWash"));

        #endregion

        #region
        private DateTime final { get; set; }

        public async Task<IEnumerable<OrderCarWashWorkersDayGroupBll>> OrderCarWashWorkers(int? id, DateTime startDate, DateTime? finalDate)
        {
            if (finalDate != null)
            {
            }

            if (id != null)
            {
                if (finalDate == null)
                    return OrderCarWashWorkersDayGroup(await Reports(id.Value, startDate));
                else
                    return OrderCarWashWorkersDayGroup(await Reports(id.Value, startDate, finalDate.Value));
            }
            else
            {
                if (finalDate == null)
                    return OrderCarWashWorkersDayGroup(await Reports(startDate));
                else
                    return OrderCarWashWorkersDayGroup(await Reports(startDate, finalDate.Value));
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

        public async Task Insert(OrderCarWashWorkersBll element)
        {
            _unitOfWork.OrderCarWasWorkersUnitOFWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        public async Task Update(OrderCarWashWorkersBll elementToUpdate)
        {
            _unitOfWork.OrderCarWasWorkersUnitOFWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        public OrderCarWashWorkers TransformAnEntity(OrderCarWashWorkersBll entity) =>
            Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(entity);
    }
}

