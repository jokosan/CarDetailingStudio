using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using System;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Checkout
{
    public class Order : IOrder
    {
        private IOrderServicesCarWashServices _orderServicesCarWash;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private IStorageFee _storageFee;
        private ITireStorage _tireStorage;
        private IClientsOfCarWashServices _clientsOfCarWashServices;

        public Order(IOrderServicesCarWashServices orderServicesCarWash, IOrderCarWashWorkersServices orderCarWashWorkers, IStorageFee storageFee, ITireStorage tireStorage,
                                            IClientsOfCarWashServices clientsOfCarWashServices)
        {
            _orderServicesCarWash = orderServicesCarWash;
            _orderCarWashWorkers = orderCarWashWorkers;
            _storageFee = storageFee;
            _tireStorage = tireStorage;
            _clientsOfCarWashServices = clientsOfCarWashServices;
        }

        #region

        public async Task<int> OrderForCarpetCleaning(OrderCarpetWashingBll orderCarpetWashing, int? idPaymentState, int prise, int clientId)
        {
            double? sumOrder = orderCarpetWashing.area * (double)prise;
            var resultClients = await _clientsOfCarWashServices.ClientWhereToInfoClient(clientId);

            OrderServicesCarWashBll orderservices = new OrderServicesCarWashBll();

            if (resultClients != null)
            {
                orderservices = new OrderServicesCarWashBll
                {
                    StatusOrder = 1,
                    OrderDate = orderCarpetWashing.orderDate,
                    IdClientsOfCarWash = resultClients.id,
                    TotalCostOfAllServices = sumOrder,
                    DiscountPrice = sumOrder,
                    typeOfOrder = 3,
                    PaymentState = idPaymentState
                };
            }
            else
            {
                orderservices = new OrderServicesCarWashBll
                {
                    StatusOrder = 1,
                    OrderDate = orderCarpetWashing.orderDate,
                    IdClientsOfCarWash = null,
                    TotalCostOfAllServices = sumOrder,
                    DiscountPrice = sumOrder,
                    typeOfOrder = 3,
                    PaymentState = idPaymentState
                };
            }


            return await _orderServicesCarWash.CreateOrder(orderservices);
        }

        #endregion

        #region оформление заказа "Хранение шин"
            
        public  async Task<int> CreateStorageFee(int storageTime, double? sum)
        {
            var storageFeeAdd = new StorageFeeBll
            {
                DateOfPayment = DateTime.Now,
                amount = sum,
                storageTime = storageTime,
                storageStatus = false
            };

            return await _storageFee.InsertVoidInt(storageFeeAdd);
        }
        #endregion
    }
}
