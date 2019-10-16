using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderCarWashWorkersServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private OrderCarWashWorkersBll _orderCarBrigade;

        public OrderCarWashWorkersServices(UnitOfWork unitOfWork, AutomapperConfig automapperConfig, OrderCarWashWorkersBll  orderCar)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapperConfig;
            _orderCarBrigade = orderCar;
        }

        public void AddEmployeeToOrder(List<string> idBrigade, int idOrder)
        {
            

            foreach (var item in idBrigade)
            {
                _orderCarBrigade.Id = _orderCarBrigade.Id + 1;
                _orderCarBrigade.IdCarWashWorkers = Convert.ToInt32(item);
                _orderCarBrigade.IdOrder = idOrder;

                OrderCarWashWorkers orderCarWashWorkers = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(_orderCarBrigade);

                _unitOfWork.OrderCarWasWorkersUnitOFWork.Insert(orderCarWashWorkers);
                _unitOfWork.Save();
            }
        }
    }
}
