using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
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
            return  getResult.Where(x => x.OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy") == dateTime.ToString("dd.MM.yyyy"));
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

        public async Task<IEnumerable<OrderCarWashWorkersBll>> СontractorAllId(int? id)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.GetWhere(x => x.Id == id));
        }


        public async Task<IEnumerable<OrderCarWashWorkersBll>> GetTableInclud()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.closedDayStatus == true)
                                                                                                                               && (x.CalculationStatus == false), "CarWashWorkers"));
        }
        // Закрыть текущий день 
        public async Task<IEnumerable<OrderCarWashWorkersBll>> GetClosedDay()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.closedDayStatus == false)
                                                                                                                               && (x.CalculationStatus == false), "CarWashWorkers"));
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

    }
}

