using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class BrigadeForTodayBll
    {
        public BrigadeForTodayBll()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashBll>();
        }

        public int id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string EarlyTermination { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
        
        public virtual ICollection<OrderServicesCarWashBll> OrderServicesCarWash { get; set; }
    }
}
