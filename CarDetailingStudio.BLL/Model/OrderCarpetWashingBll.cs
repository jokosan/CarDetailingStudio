using System;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderCarpetWashingBll 
    {
        public int idOrderCarpetWashing { get; set; }
        public int orderServicesCarWashId { get; set; }
        public Nullable<int> clientId { get; set; }
        public Nullable<double> area { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<System.DateTime> orderClosingDate { get; set; }
        public Nullable<System.DateTime> orderCompletionDate { get; set; }

        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
    }
}
