using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.OrderModules.TireStorage
{
    public class Order
    {
        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> StatusOrder { get; set; }
        public Nullable<int> PaymentState { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> ClosingData { get; set; }
        public Nullable<double> TotalCostOfAllServices { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public Nullable<int> typeOfOrder { get; set; }
    }

    public class TireStorageService
    {
        private readonly IOrderServicesCarWashServices _order;
        private readonly ITireStorage _tireStorage;

        public TireStorageService(
            IOrderServicesCarWashServices order,
             ITireStorage tireStorage)
        {
            _order = order;
            _tireStorage = tireStorage;
        }

      
    }
}
