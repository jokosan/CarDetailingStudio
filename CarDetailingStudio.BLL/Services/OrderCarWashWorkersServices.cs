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

        public void AddEmployeeToOrder(List<string> idBrigade, int idOrder)
        {
            foreach (var item in idBrigade)
            {
                _orderCarBrigade.IdCarWashWorkers = Convert.ToInt32(item);
                _orderCarBrigade.IdOrder = idOrder;
                _orderCarBrigade.CalculationStatus = false;

                OrderCarWashWorkers orderCarWashWorkers = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(_orderCarBrigade);

                _unitOfWork.OrderCarWasWorkersUnitOFWork.Insert(orderCarWashWorkers);
                _unitOfWork.Save();
            }

            //ЗП для Adminstratora

            Adminnistrator(idOrder);



            //if (ServicesOrder.Any(x => x.Detailings.IdTypeService == 1)) // id услуги в таблице прайс
            //{
            //    int AdminCaarWash = Adminnistrator(2); //id сотрудника в таблице

            //    if (AdminCaarWash != 0)
            //    {
            //        double? SumServicesCarWash = ServicesOrder.Where(x => x.Detailings.IdTypeService == 1).Sum(n => n.Price);
            //    }

            //}
            //else if (ServicesOrder.Any(x => x.Detailings.IdTypeService == 2)) // id услуги в таблице прайс
            //{
            //    int AdminCaarWash = Adminnistrator(1); //id сотрудника в таблице
            //    double? SumServicesDetailings = ServicesOrder.Where(x => x.Detailings.IdTypeService == 2).Sum(n => n.Price);
            //}

        }
        private OrderCarWashWorkersBll ChoiceAdministrator(IEnumerable<BrigadeForTodayBll> brigade, int id, int idOrder)
        {
            var adminCarWash = brigade.Single(x => x.CarWashWorkers.IdPosition == id);

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            orderCarWashWorkers.IdOrder = idOrder;
            orderCarWashWorkers.IdCarWashWorkers = adminCarWash.CarWashWorkers.id;
            orderCarWashWorkers.CalculationStatus = false;

            return orderCarWashWorkers;
        }

        private void Adminnistrator(int idOrder)
        {
            var ServicesOrder = _servisesCarWash.GetAllId(idOrder); // получаем услуги по текущему заказу

            bool ServicesCarWash = ServicesOrder.Any(x => x.Detailings.IdTypeService == 2);
            bool ServicesDetailings = ServicesOrder.Any(x => x.Detailings.IdTypeService == 1);

            IEnumerable<BrigadeForTodayBll> brigade = _brigade.GetDateTimeNow(); //получаем всех сотрудников на смене

            bool CarWash = brigade.Any(x => x.CarWashWorkers.IdPosition == 1);
            bool Detailings = brigade.Any(x => x.CarWashWorkers.IdPosition == 2);


            if (ServicesCarWash) // id услуги в таблице прайс
            {
                if (CarWash)
                {
                    _orderCarBrigade = ChoiceAdministrator(brigade, 1, idOrder);   
                }
                else if (Detailings)
                {
                    _orderCarBrigade = ChoiceAdministrator(brigade, 2, idOrder);
                }
            }
            else if (ServicesDetailings)
            {
                if (Detailings)
                {
                    _orderCarBrigade = ChoiceAdministrator(brigade, 2, idOrder);

                }
                else if (CarWash)
                {
                    _orderCarBrigade = ChoiceAdministrator(brigade, 1, idOrder);
                }

            }
         
            OrderCarWashWorkers orderCarWashWorkers = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(_orderCarBrigade);

            _unitOfWork.OrderCarWasWorkersUnitOFWork.Insert(orderCarWashWorkers);
            _unitOfWork.Save();
        }

        public IEnumerable<OrderCarWashWorkersBll> SampleForPayroll(int IdCarWashWorkers)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.GetWhere(x => (x.IdCarWashWorkers == IdCarWashWorkers)
                                                                                                                       && (x.CalculationStatus == false)));
        }

        public IEnumerable<OrderCarWashWorkersBll> СontractorAllId(int? id)
        {
            return Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.GetWhere(x => x.IdOrder == id));
        }
    }
}
