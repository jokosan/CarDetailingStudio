using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderCarpetWashingBll
    {
        public int idOrderCarpetWashing { get; set; }
        public int orderServicesCarWashId { get; set; }
        public string Customer { get; set; }
        public string telephone { get; set; }
        public Nullable<double> area { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<System.DateTime> orderClosingDate { get; set; }
        public Nullable<System.DateTime> orderCompletionDate { get; set; }

        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
    }
}
