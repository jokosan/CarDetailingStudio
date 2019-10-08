using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class StatusOrderView
    {
        public StatusOrderView()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashView>();
        }

        public int Id { get; set; }
        public string StatusOrder1 { get; set; }

        public virtual ICollection<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
    }
}