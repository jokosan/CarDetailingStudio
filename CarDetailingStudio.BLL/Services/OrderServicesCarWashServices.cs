using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderServicesCarWashServices : IServices<OrderServicesCarWashBll>
    {
        private IUnitOfWork _ubitOfWork;
        private AutomapperConfig _automapper;
        private OrderServices _orderServices;
        private ServisesCarWashOrderBll _servisesCar;

        public OrderServicesCarWashServices(UnitOfWork unitOfWork, AutomapperConfig automapper, OrderServices orderServices, ServisesCarWashOrderBll servisesCar)
        {
            _ubitOfWork = unitOfWork;
            _automapper = automapper;
            _orderServices = orderServices;
            _servisesCar = servisesCar;
        }

        public IEnumerable<OrderServicesCarWashBll> GetAll()
        {
            return Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_ubitOfWork.OrderServicesCarWashUnitOfWork.Get());
        }

        public OrderServicesCarWashBll GetId(int? id)
        {
            var resilt = Mapper.Map<OrderServicesCarWashBll>(_ubitOfWork.OrderServicesCarWashUnitOfWork.GetById(id));
            return resilt;
        }

        public void InsertOrders(string price)
        {
            OrderServicesCarWashBll OrderFormation = new OrderServicesCarWashBll();

            OrderFormation = new OrderServicesCarWashBll
            {
                IdClientsOfCarWash = OrderServices.idClient,
                StatusOrder = 1,
                OrderDate = DateTime.Now,
                TotalCostOfAllServices = OrderPrice()
            };

            OrderServicesCarWash orderServicesCar = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(OrderFormation);

            _ubitOfWork.OrderServicesCarWashUnitOfWork.Insert(orderServicesCar);

            _ubitOfWork.Save();

            foreach (var item in OrderServices.OrderList)
            {
                _servisesCar = new ServisesCarWashOrderBll
                {
                    IdClientsOfCarWash = Convert.ToInt32(OrderServices.idClient),
                    IdOrderServicesCarWash = orderServicesCar.Id,
                    IdWashServices = item.Id,
                };

                ServisesCarWashOrder servisesCarWash = Mapper.Map<ServisesCarWashOrderBll, ServisesCarWashOrder>(_servisesCar);

                _ubitOfWork.ServisesCarWashOrderUnitOfWork.Insert(servisesCarWash);
                _ubitOfWork.Save();
            }           
        }


        public double? OrderPrice()
        {
            var result = OrderServices.OrderList;

            switch (OrderServices.body)
            {
                case "S":
                    return result.Sum(x => x.S);

                case "M":
                    return result.Sum(x => x.M);

                case "L":
                    return result.Sum(x => x.L);

                case "XL":
                    return result.Sum(x => x.XL);
            }

            return null;
        }
    }
}
