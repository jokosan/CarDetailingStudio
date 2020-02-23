using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Checkout
{
    public class Order : IOrder
    {
        private IOrderServicesCarWashServices _orderServicesCarWash;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private IStorageFee _storageFee;
        private ITireStorage _tireStorage;

        public Order(IOrderServicesCarWashServices orderServicesCarWash, IOrderCarWashWorkersServices orderCarWashWorkers, IStorageFee storageFee, ITireStorage tireStorage)
        {
            _orderServicesCarWash = orderServicesCarWash;
            _orderCarWashWorkers = orderCarWashWorkers;
            _storageFee = storageFee;
            _tireStorage = tireStorage;
        }

        public int Chekout(OrderTireStorageModelBll orderTireStorage, double? sum, int? idPaymentState)
        {
            var orderservices = new OrderServicesCarWashBll
            {
                IdClientsOfCarWash = orderTireStorage.carWashWorkersId,
                StatusOrder = 5,
                OrderDate = orderTireStorage.dateOfAdoption,
                ClosingData = DateTime.Now,
                TotalCostOfAllServices = sum,
                DiscountPrice = sum,
                typeOfOrder = 2,
                PaymentState = idPaymentState                
            };

            int idOrder = _orderServicesCarWash.CreateOrder(orderservices);
            int idStorageFee = CreateStorageFee(orderTireStorage, sum);

            CreateOrderTireStorage(orderTireStorage, idOrder, idStorageFee);

            return idOrder;
        }

        private void CreateOrderTireStorage(OrderTireStorageModelBll orderTireStorage, int idOrder, int idStorageFee)
        {
            var tireStorage = new TireStorageBll
            {
                carWashWorkersId = orderTireStorage.carWashWorkersId,
                dateOfAdoption = orderTireStorage.dateOfAdoption,
                quantity = orderTireStorage.quantity,
                radius = orderTireStorage.radius,
                firm = orderTireStorage.firm,
                discAvailability = orderTireStorage.discAvailability,
                storageFeeId = idStorageFee,
                tireStorageBags = orderTireStorage.tireStorageBags,
                wheelWash = orderTireStorage.wheelWash,
                IdOrderServicesCarWash = idOrder,
                silicone = orderTireStorage.silicone
            };

            _tireStorage.Insert(tireStorage);
        }

        private int CreateStorageFee(OrderTireStorageModelBll orderTireStorage, double? sum)
        {
            var storageFeeAdd = new StorageFeeBll
            {
                DateOfPayment = DateTime.Now,
                amount = sum,
                storageTime = orderTireStorage.storageTime,
                storageStatus = false                
            };

            return _storageFee.InsertVoidInt(storageFeeAdd);
        }
    }
}
