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
using CarDetailingStudio.BLL.Services.Modules.ModulesModel;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderServicesCarWashServices
    {
        private IUnitOfWork _ubitOfWork;
        private AutomapperConfig _automapper;
        private OrderServices _orderServices;
        private ServisesCarWashOrderBll _servisesCar;
        private OrderCarWashWorkersServices _orderCarWashWorkersServices;

        public OrderServicesCarWashServices(UnitOfWork unitOfWork, AutomapperConfig automapper,
                                            OrderServices orderServices, ServisesCarWashOrderBll servisesCar, OrderCarWashWorkersServices orderCarWash)
        {
            _ubitOfWork = unitOfWork;
            _automapper = automapper;
            _orderServices = orderServices;
            _servisesCar = servisesCar;
            _orderCarWashWorkersServices = orderCarWash;
        }

        public IEnumerable<OrderServicesCarWashBll> GetAll(int statusOrder)
        {
            var GetAllResult = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_ubitOfWork.orderUnitiOfWork.GetWhere(x => x.StatusOrder == statusOrder));

            return GetAllResult;
        }

        public OrderServicesCarWashBll GetId(int? id)
        {
            return Mapper.Map<OrderServicesCarWashBll>(_ubitOfWork.orderUnitiOfWork.GetById(id));
        }

        public void InsertOrders(List<double> carBody, List<int> id, List<int> sum)
        {
            OrderServicesCarWashBll OrderFormation = new OrderServicesCarWashBll();

            OrderFormation = new OrderServicesCarWashBll
            {
                IdClientsOfCarWash = OrderServices.idClient,
                StatusOrder = 1,
                OrderDate = DateTime.Now,
                TotalCostOfAllServices = _orderServices.OrderPrice()
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
                    Price = PriceServices(carBody, id, sum, item.Id)
                };

                ServisesCarWashOrder servisesCarWash = Mapper.Map<ServisesCarWashOrderBll, ServisesCarWashOrder>(_servisesCar);

                _ubitOfWork.ServisesCarWashOrderUnitOfWork.Insert(servisesCarWash);
                _ubitOfWork.Save();
            }

            _orderServices.ClearListOrder();
        }

        public double PriceServices(List<double> carBody, List<int> idList, List<int> sum, int id)
        {
            List<OrderPriceModel> OrderPrice = new List<OrderPriceModel>();

            for (int i = 0; i < idList.Count; i++)
            {
                OrderPrice.Add(new OrderPriceModel
                {
                    id = idList[i],
                    price = carBody[i],
                    priceSum = sum[i]
                });
            }


            foreach (var i in OrderPrice)
            {
                if (i.id == id)
                {
                    return i.price + i.priceSum;
                }
            }

            return 0;
        }

        public void CloseOrder(int idPaymentState, int idOrder, List<string> idBrigade)
        {
            var Order = GetId(idOrder);

            Order.PaymentState = idPaymentState;
            Order.ClosingData = DateTime.Now;
            Order.StatusOrder = 2;

            double x = Convert.ToDouble(_orderServices.Discont(Order.ClientsOfCarWash.discont, Order.TotalCostOfAllServices));
            Order.DiscountPrice = Math.Ceiling(x);

            OrderServicesCarWash orderCarWashWorkers = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(Order);

            _ubitOfWork.OrderServicesCarWashUnitOfWork.Update(orderCarWashWorkers);
            _ubitOfWork.Save();

            _orderCarWashWorkersServices.AddEmployeeToOrder(idBrigade, idOrder);
        }

        public void RecountOrder(int idOrder)
        {
            OrderServicesCarWashBll WhereIdOrder = GetId(idOrder);

            OrderServicesCarWash EditOrder = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(WhereIdOrder);

            EditOrder.TotalCostOfAllServices = _ubitOfWork.ServisesCarWashOrderUnitOfWork.GetWhere(x => x.IdOrderServicesCarWash == idOrder).Sum(x => x.Price);

            _ubitOfWork.OrderServicesCarWashUnitOfWork.Update(EditOrder);
            _ubitOfWork.Save();
        }

        public IEnumerable<OrderServicesCarWashBll> OrderReport(DateTime start, DateTime final)
        {
            var OrderByDate = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_ubitOfWork.orderUnitiOfWork.GetWhere(x => (x.OrderDate >= start)
                                                                                                                       && (x.OrderDate <= final)
                                                                                                                       && (x.StatusOrder == 2)));
            return OrderByDate;
        }
    }
}


