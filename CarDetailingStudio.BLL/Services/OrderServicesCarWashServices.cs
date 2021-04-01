using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.BLL.Services.Modules.ModulesModel;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderServicesCarWashServices : IOrderServicesCarWashServices
    {
        private IUnitOfWork _unitOfWork;
        private IOrderServices _orderServices;
        private IOrderCarWashWorkersServices _orderCarWashWorkersServices;
        private IServisesCarWashOrderServices _servisesCarWashOrder;

        private ServisesCarWashOrderBll _servisesCar;

        public OrderServicesCarWashServices(
            IUnitOfWork unitOfWork,
            IOrderServices orderServices, 
            ServisesCarWashOrderBll servisesCar,
            IOrderCarWashWorkersServices orderCarWash, 
            IServisesCarWashOrderServices servisesCarWashOrder)
        {
            _unitOfWork = unitOfWork;
            _orderServices = orderServices;
            _servisesCar = servisesCar;
            _orderCarWashWorkersServices = orderCarWash;
            _servisesCarWashOrder = servisesCarWashOrder;
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> GetOrderAllTireStorage(int typeOfOrder, int statusOrder) =>
            Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.orderUnitiOfWork.GetWhere(x =>
                ((x.typeOfOrder == typeOfOrder) && (x.StatusOrder == statusOrder)) || ((x.typeOfOrder == typeOfOrder) && (x.StatusOrder == 4))));
        
        public async Task<IEnumerable<OrderServicesCarWashBll>> ArxivOrder(int typeOfOrder, int statusOrder) =>
         Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.orderUnitiOfWork.GetWhere(x =>
             (x.StatusOrder == statusOrder) && (x.typeOfOrder == typeOfOrder)));

        public async Task<IEnumerable<OrderServicesCarWashBll>> GetAll(int statusOrder)
        {
            if (statusOrder != 2)
            {
                var GetAllResult = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.orderUnitiOfWork.GetWhere(x =>
                    ((x.StatusOrder == statusOrder) && (x.typeOfOrder <= 2)) || ((x.StatusOrder == 4) && (x.typeOfOrder <= 2))));
                
                return GetAllResult;
            }
            else
            {
                var dateCriteria = DateTime.Now.Date.AddDays(-7);
                var OrderByDate = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.orderUnitiOfWork.GetWhere(x => x.StatusOrder == statusOrder));

                var Test = OrderByDate.Where(x => x.ClosingData >= dateCriteria);

                return Test;
            }
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> AllCustomerOrders(int client) =>
            Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.OrderServicesCarWashUnitOfWork.QueryObjectGraph(x =>
                x.IdClientsOfCarWash == client, "ClientsOfCarWash", "ClientsOfCarWash.ClientInfo"));

        public async Task<OrderServicesCarWashBll> GetId(int? id) =>
            Mapper.Map<OrderServicesCarWashBll>(await _unitOfWork.orderUnitiOfWork.GetById(id));

        public async Task InsertOrders(int service, List<double> carBody, List<int> id, List<int> sum, double total)
        {
            OrderServicesCarWashBll OrderFormation = new OrderServicesCarWashBll();

            OrderFormation = new OrderServicesCarWashBll
            {
                IdClientsOfCarWash = OrderServices.idClient,
                StatusOrder = 1,
                OrderDate = DateTime.Now,
                TotalCostOfAllServices = _orderServices.OrderPrice(),
                DiscountPrice = Math.Round(total),
                typeOfOrder = service

            };

            int orderServicesCar = await CreateOrder(OrderFormation);

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
                await _unitOfWork.Save();
            }

            _orderServices.ClearListOrder();
        }

        public async Task InsertOrders(List<double> carBody, List<int> id, List<int> sum, double total, int? idOrder)
        {
            var OrderServicesCarWashGetId = Mapper.Map<OrderServicesCarWashBll>(await _unitOfWork.OrderServicesCarWashUnitOfWork.GetById(idOrder));

            OrderServicesCarWashGetId.TotalCostOfAllServices = OrderServicesCarWashGetId.TotalCostOfAllServices + _orderServices.OrderPrice();
            OrderServicesCarWashGetId.DiscountPrice = Math.Round(total);

            await SaveOrder(OrderServicesCarWashGetId);

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
                await _unitOfWork.Save();
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

        public async Task RecountOrder(int idOrder, int? ClientDiscont = null)
        {
            OrderServicesCarWashBll WhereIdOrder = await GetId(idOrder);

            OrderServicesCarWash EditOrder = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(WhereIdOrder);

            var getResult = await _unitOfWork.ServisesCarWashOrderUnitOfWork.GetWhere(x => x.IdOrderServicesCarWash == idOrder);

            EditOrder.TotalCostOfAllServices = getResult.Sum(x => x.Price);

            if (ClientDiscont != null)
            {
                EditOrder.DiscountPrice = _orderServices.Discont(ClientDiscont, EditOrder.TotalCostOfAllServices);
            }
            else
            {
                EditOrder.DiscountPrice = EditOrder.TotalCostOfAllServices;
            }

            _unitOfWork.OrderServicesCarWashUnitOfWork.Update(EditOrder);
            await _unitOfWork.Save();
        }

        public async Task StatusOrder(int? idOrder, int status)
        {
            var order = await GetId(idOrder);
            order.StatusOrder = status;

            await SaveOrder(order);
        }

        public async Task DeleteOrder(int idOrder)
        {
            _unitOfWork.OrderServicesCarWashUnitOfWork.Delete(idOrder);

            await _servisesCarWashOrder.ServicesDelete(idOrder, nameof(OrderServicesCarWashServices));

            await _unitOfWork.Save();
        }

        public async Task SaveOrder(OrderServicesCarWashBll orderSave)
        {
            _unitOfWork.OrderServicesCarWashUnitOfWork.Update(EntityTransformation(orderSave));
            await _unitOfWork.Save();
        }

        public async Task<int> CreateOrder(OrderServicesCarWashBll orderSave)
        {
            OrderServicesCarWash orderCarWashWorkers = Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(orderSave);

            _unitOfWork.OrderServicesCarWashUnitOfWork.Insert(orderCarWashWorkers);
            await _unitOfWork.Save();

            return orderCarWashWorkers.Id;
        }

        public async Task CloseOrder(int? idOrder, int? idStatusOrder, int? idPaymentState)
        {
            var singlOrder = await GetId(idOrder);

            singlOrder.StatusOrder = idStatusOrder;
            singlOrder.PaymentState = idPaymentState;

            await SaveOrder(singlOrder);
        }

        #region Отчеты
        public async Task<IEnumerable<OrderServicesCarWashBll>> Reports(DateTime datepresentDay) =>
            Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.orderUnitiOfWork.WhereMonthlyReport(x =>
                DbFunctions.TruncateTime(x.OrderDate.Value) == datepresentDay.Date));

        public async Task<IEnumerable<OrderServicesCarWashBll>> ReportsClosingData(DateTime datepresentDay) =>
            Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.orderUnitiOfWork.WhereMonthlyReport(x =>
                DbFunctions.TruncateTime(x.ClosingData.Value) == datepresentDay.Date));

        public async Task<IEnumerable<OrderServicesCarWashBll>> Reports(DateTime startDate, DateTime finalDate) => 
            Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.orderUnitiOfWork.WhereMonthlyReport(x =>
                (DbFunctions.TruncateTime(x.OrderDate.Value) >= startDate.Date) && (DbFunctions.TruncateTime(x.OrderDate.Value) <= finalDate.Date)));

        public async Task<IEnumerable<OrderServicesCarWashBll>> OrderReport(DateTime start, DateTime final)
        {
            var dateStart = start.Date;
            var dateFinal = final.Date.AddDays(1);

            var OrderByDate = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(await _unitOfWork.orderUnitiOfWork.GetWhereDate(x => x.StatusOrder == 2));
            
            return OrderByDate.Where(x => x.ClosingData > dateStart && x.ClosingData <= final.Date.AddDays(1));
        }

        #endregion

        private OrderServicesCarWash EntityTransformation(OrderServicesCarWashBll Entity) => 
            Mapper.Map<OrderServicesCarWashBll, OrderServicesCarWash>(Entity);

       
    }
}