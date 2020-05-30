using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.BLL.Services.Modules.ModulesModel;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CarDetailingStudio.BLL.Services
{
    public class OrderServicesCarWashServices : IOrderServicesCarWashServices
    {
        private IUnitOfWork _unitOfWork;
        private IOrderServices _orderServices;
        private ServisesCarWashOrderBll _servisesCar;
        private IOrderCarWashWorkersServices _orderCarWashWorkersServices;
        private IServisesCarWashOrderServices _servisesCarWashOrder;

        public OrderServicesCarWashServices(IUnitOfWork unitOfWork, IOrderServices orderServices, ServisesCarWashOrderBll servisesCar,
                                            IOrderCarWashWorkersServices orderCarWash, IServisesCarWashOrderServices servisesCarWashOrder)
        {
            _unitOfWork = unitOfWork;
            _orderServices = orderServices;
            _servisesCar = servisesCar;
            _orderCarWashWorkersServices = orderCarWash;
            _servisesCarWashOrder = servisesCarWashOrder;
        }

        public IEnumerable<OrderServicesCarWashBll> MonthlyReport(DateTime date)
        {
            return Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.WhereMonthlyReport(x => x.ClosingData?.Month == date.Month));
        }

        public IEnumerable<OrderServicesCarWashBll> GetOrderAllTireStorage()
        {
            return Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.GetWhere(x => x.StatusOrder == 5));
        }

        public IEnumerable<OrderServicesCarWashBll> GetAll(int statusOrder)
        {
            if (statusOrder != 2)
            {
                var GetAllResult = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.GetWhere(x => (x.StatusOrder == statusOrder) && (x.typeOfOrder == 1) || (x.StatusOrder == 4)));
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

        public IEnumerable<OrderServicesCarWashBll> GetAll(int statusOrder, int typeOfOrder)
        {
            return Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.GetWhere(x => (x.StatusOrder == statusOrder) && (x.typeOfOrder == typeOfOrder)));
        }

        public IEnumerable<OrderServicesCarWashBll> AllOrderOneEmployee(List<int> idOrder)
        {
            return Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.QueryObjectGraph(x => idOrder.Contains(x.Id)));
        }

        public OrderServicesCarWashBll GetId(int? id)
        {
            return Mapper.Map<OrderServicesCarWashBll>(_unitOfWork.orderUnitiOfWork.GetById(id));
        }

        public void InsertOrders(List<double> carBody, List<int> id, List<int> sum, double total)
        {
            OrderServicesCarWashBll OrderFormation = new OrderServicesCarWashBll();

            OrderFormation = new OrderServicesCarWashBll
            {
                IdClientsOfCarWash = OrderServices.idClient,
                StatusOrder = 1,
                OrderDate = DateTime.Now,
                TotalCostOfAllServices = _orderServices.OrderPrice(),
                DiscountPrice = Math.Round(total),
                typeOfOrder = 1

            };

            int orderServicesCar = CreateOrder(OrderFormation);

            foreach (var item in OrderServices.OrderList)
            {
                _servisesCar = new ServisesCarWashOrderBll
                {
                    IdClientsOfCarWash = Convert.ToInt32(OrderServices.idClient),
                    IdOrderServicesCarWash = orderServicesCar,
                    IdWashServices = item.Id,
                    Price = PriceServices(carBody, id, sum, item.Id)
                };

                ServisesCarWashOrder servisesCarWash = Mapper.Map<ServisesCarWashOrderBll, ServisesCarWashOrder>(_servisesCar);

                _unitOfWork.ServisesCarWashOrderUnitOfWork.Insert(servisesCarWash);
                _unitOfWork.Save();
            }

            _orderServices.ClearListOrder();
        }

        public void InsertOrders(List<double> carBody, List<int> id, List<int> sum, double total, int? idOrder)
        {
            var OrderServicesCarWashGetId = Mapper.Map<OrderServicesCarWashBll>(_unitOfWork.OrderServicesCarWashUnitOfWork.GetById(idOrder));

            OrderServicesCarWashGetId.TotalCostOfAllServices = OrderServicesCarWashGetId.TotalCostOfAllServices + _orderServices.OrderPrice();
            OrderServicesCarWashGetId.DiscountPrice = Math.Round(total);

            SaveOrder(OrderServicesCarWashGetId);

            foreach (var item in OrderServices.OrderList)
            {
                _servisesCar = new ServisesCarWashOrderBll
                {
                    IdClientsOfCarWash = Convert.ToInt32(OrderServices.idClient),
                    IdOrderServicesCarWash = idOrder,
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

        public IEnumerable<OrderServicesCarWashBll> GetDataClosing()
        {
            return Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.OrderServicesCarWashUnitOfWork
                                                                              .GetWhere(x => x.ClosingData?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")));
        }

        public void RecountOrder(int idOrder, int? ClientDiscont = null)
        {
            OrderServicesCarWashBll WhereIdOrder = GetId(idOrder);

            OrderServicesCarWash EditOrder = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(WhereIdOrder);

            EditOrder.TotalCostOfAllServices = _unitOfWork.ServisesCarWashOrderUnitOfWork.GetWhere(x => x.IdOrderServicesCarWash == idOrder).Sum(x => x.Price);

            if (ClientDiscont != null)
            {
                EditOrder.DiscountPrice = _orderServices.Discont(ClientDiscont, EditOrder.TotalCostOfAllServices);
            }
            else
            {
                EditOrder.DiscountPrice = EditOrder.TotalCostOfAllServices;
            }

            _unitOfWork.OrderServicesCarWashUnitOfWork.Update(EditOrder);
            _unitOfWork.Save();
        }

        public void StatusOrder(int? idOrder, int status)
        {
            var order = GetId(idOrder);
            order.StatusOrder = status;

            SaveOrder(order);
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

        public void SaveOrder(OrderServicesCarWashBll orderSave)
        {
            OrderServicesCarWash orderCarWashWorkers = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(orderSave);

            _unitOfWork.OrderServicesCarWashUnitOfWork.Update(orderCarWashWorkers);
            _unitOfWork.Save();
        }

        public int CreateOrder(OrderServicesCarWashBll orderSave)
        {
            OrderServicesCarWash orderCarWashWorkers = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(orderSave);

            _unitOfWork.OrderServicesCarWashUnitOfWork.Insert(orderCarWashWorkers);
            _unitOfWork.Save();

            return orderCarWashWorkers.Id;
        }
    }
}