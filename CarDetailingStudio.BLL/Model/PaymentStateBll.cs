using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class PaymentStateBll
    {
      
        public PaymentStateBll()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashBll>();
        }

        public int Id { get; set; }
        public string PaymentState1 { get; set; }

        public virtual ICollection<OrderServicesCarWashBll> OrderServicesCarWash { get; set; }
    }
}
