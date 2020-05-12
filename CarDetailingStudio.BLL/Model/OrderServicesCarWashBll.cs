using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderServicesCarWashBll
    {
        public OrderServicesCarWashBll()
        {
            this.orderCarpetWashing = new HashSet<OrderCarpetWashingBll>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersBll>();
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrderBll>();
            this.TireStorage = new HashSet<TireStorageBll>();
        }

        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> StatusOrder { get; set; }
        public Nullable<int> PaymentState { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> ClosingData { get; set; }
        public Nullable<double> TotalCostOfAllServices { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public Nullable<int> typeOfOrder { get; set; }

        public virtual ClientsOfCarWashBll ClientsOfCarWash { get; set; }
        public virtual ICollection<OrderCarpetWashingBll> orderCarpetWashing { get; set; }
        public virtual PaymentStateBll PaymentState1 { get; set; }
        public virtual StatusOrderBll StatusOrder1 { get; set; }
        public virtual ICollection<ServisesCarWashOrderBll> ServisesCarWashOrder { get; set; }
        public virtual ICollection<OrderCarWashWorkersBll> OrderCarWashWorkers { get; set; }
        public virtual ICollection<TireStorageBll> TireStorage { get; set; }
    }
}
