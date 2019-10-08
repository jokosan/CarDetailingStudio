using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class StatusOrderBll
    {
        public StatusOrderBll()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashBll>();
        }

        public int Id { get; set; }
        public string StatusOrder1 { get; set; }

        public virtual ICollection<OrderServicesCarWashBll> OrderServicesCarWash { get; set; }
    }
}
