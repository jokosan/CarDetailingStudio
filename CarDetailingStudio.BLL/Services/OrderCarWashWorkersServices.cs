using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

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

        public IEnumerable<OrderCarWashWorkersBll> SampleForPayroll(DateTime dateTime)
        {           
            return TableCalculationStatusFolse().Where(x => x.OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy") == dateTime.ToString("dd.MM.yyyy"));
        }

        public IEnumerable<OrderCarWashWorkersBll> SampleForPayroll(int id, DateTime date)
        {   
            return TableCalculationStatusFolse().Where(x =>  (x.IdCarWashWorkers == id) && (x.OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyyy") == date.ToString("dd.MM.yyyy")));
        }

        public IEnumerable<OrderCarWashWorkersBll> TableCalculationStatusFolse()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => x.CalculationStatus == false, "OrderServicesCarWash"));           
        }

        public IEnumerable<OrderCarWashWorkersBll> SampleForPayroll(int? IdCarWashWorkers)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.IdCarWashWorkers == IdCarWashWorkers)
                                                                                                                         && (x.CalculationStatus == false), "OrderServicesCarWash"));
        }

        public IEnumerable<OrderCarWashWorkersBll> СontractorAllId(int? id)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.GetWhere(x => x.Id == id));
        }

        public void OrderServicesUpdate(int id)
        {
            var OrderCarWashWorkersWhere = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.GetWhere(x => (x.CalculationStatus == false)
                                                                                                                                             && (x.IdCarWashWorkers == id)));
            OrderCarWashWorkersBll order = new OrderCarWashWorkersBll();

            foreach (var x in OrderCarWashWorkersWhere)
            {
                order.Id = x.Id;
                order.IdOrder = x.IdOrder;
                order.IdCarWashWorkers = x.IdCarWashWorkers;
                order.CalculationStatus = true;

                UpdateOrderCarWashWorkers(order);
            }
        }

        public IEnumerable<OrderCarWashWorkersBll> GetTableInclud()
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.closedDayStatus == true)
                                                                                                                               && (x.CalculationStatus == false), "CarWashWorkers"));
        }

        public IEnumerable<OrderCarWashWorkersBll> GetClosedDay()
        { 
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.closedDayStatus == false) 
                                                                                                                               && (x.CalculationStatus == false), "CarWashWorkers"));
        }

        public IEnumerable<OrderCarWashWorkersBll> GetClosedDay(int? id)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.QueryObjectGraph(x => (x.IdCarWashWorkers == id) 
                                                                                                                               && (x.closedDayStatus == true)
                                                                                                                               && (x.CalculationStatus == false),
                                                                                                                               "OrderServicesCarWash"));
        }

        public void SaveOrderCarWashWorkers(OrderCarWashWorkersBll orderCarWash)
        {
            OrderCarWashWorkers orderCarWashWorkers = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(orderCarWash);

            _unitOfWork.OrderCarWasWorkersUnitOFWork.Insert(orderCarWashWorkers);
            _unitOfWork.Save();
        }

        public void UpdateOrderCarWashWorkers(OrderCarWashWorkersBll orderCarWash)
        {
            OrderCarWashWorkers orderCarWashWorkers = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(orderCarWash);

            _unitOfWork.OrderCarWasWorkersUnitOFWork.Update(orderCarWashWorkers);
            _unitOfWork.Save();
        }
    }
}

