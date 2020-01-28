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
    public class OrderServicesCarWashServices : IOrderServicesCarWashServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private IOrderServices _orderServices;
        private ServisesCarWashOrderBll _servisesCar;
        private IOrderCarWashWorkersServices _orderCarWashWorkersServices;
        private IServisesCarWashOrderServices _servisesCarWashOrder;

        public OrderServicesCarWashServices(IUnitOfWork unitOfWork, AutomapperConfig automapper,
                                            IOrderServices orderServices, ServisesCarWashOrderBll servisesCar,
                                            IOrderCarWashWorkersServices orderCarWash, IServisesCarWashOrderServices servisesCarWashOrder)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
            _orderServices = orderServices;
            _servisesCar = servisesCar;
            _orderCarWashWorkersServices = orderCarWash;
            _servisesCarWashOrder = servisesCarWashOrder;
        }
        
        public IEnumerable<OrderServicesCarWashBll> GetAll(int statusOrder, string searchTable = null)
        {
            if (statusOrder != 2)
            {
                var GetAllResult = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.GetWhere(x => x.StatusOrder == statusOrder));

                if (searchTable != null)
                {
                     var GetSearcgResult = GetAllResult.Where(x => x.ClientsOfCarWash.NumberCar.StartsWith(searchTable)).OrderBy(p => p.OrderDate);
                  
                    return GetSearcgResult;
                }               

                //var GetAllResult = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.GetWhere(x => (x.ClientsOfCarWash.NumberCar.StartsWith(searchTable) || searchTable == null)
                //                                                    && (x.StatusOrder == 1)));
                return GetAllResult;
            }
            else
            {
                var dateCriteria = DateTime.Now.Date.AddDays(-7);
                var OrderByDate = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.GetWhere(x => x.StatusOrder == statusOrder));
                var Test = OrderByDate.Where(x => x.ClosingData >= dateCriteria);

                return Test;
            }
        }

        public OrderServicesCarWashBll GetId(int? id)
        {
            return Mapper.Map<OrderServicesCarWashBll>(_unitOfWork.orderUnitiOfWork.GetById(id));
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

            _unitOfWork.OrderServicesCarWashUnitOfWork.Insert(orderServicesCar);
            _unitOfWork.Save();

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

                _unitOfWork.ServisesCarWashOrderUnitOfWork.Insert(servisesCarWash);
                _unitOfWork.Save();
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

            if (Order.ClientsOfCarWash.discont > 0)
            {
                double x = Convert.ToDouble(_orderServices.Discont(Order.ClientsOfCarWash.discont, Order.TotalCostOfAllServices));
                Order.DiscountPrice = Math.Ceiling(x);
            }
            else
            {
                Order.DiscountPrice = Order.TotalCostOfAllServices;
            }

            OrderServicesCarWash orderCarWashWorkers = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(Order);

            _unitOfWork.OrderServicesCarWashUnitOfWork.Update(orderCarWashWorkers);
            _unitOfWork.Save();

            _orderCarWashWorkersServices.AddEmployeeToOrder(idBrigade, idOrder);
        }

        public void RecountOrder(int idOrder)
        {
            OrderServicesCarWashBll WhereIdOrder = GetId(idOrder);

            OrderServicesCarWash EditOrder = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(WhereIdOrder);

            EditOrder.TotalCostOfAllServices = _unitOfWork.ServisesCarWashOrderUnitOfWork.GetWhere(x => x.IdOrderServicesCarWash == idOrder).Sum(x => x.Price);

            _unitOfWork.OrderServicesCarWashUnitOfWork.Update(EditOrder);
            _unitOfWork.Save();
        }

        public IEnumerable<OrderServicesCarWashBll> OrderReport(DateTime start, DateTime final)
        {
            var dateStart = start.Date;
            var dateFinal = final.Date.AddDays(1);

            var OrderByDate = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.GetWhereDate(x => x.StatusOrder == 2));
            var Test = OrderByDate.Where(x => x.ClosingData > dateStart && x.ClosingData <= final.Date.AddDays(1));
            //va);
            return Test;
        }

        public void DeleteOrder(int idOrder)
        {
            _unitOfWork.OrderServicesCarWashUnitOfWork.Delete(idOrder);

            _servisesCarWashOrder.ServicesDelete(idOrder, nameof(OrderServicesCarWashServices));

            _unitOfWork.Save();
        }
    }
}