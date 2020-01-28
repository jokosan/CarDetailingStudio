using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class BrigadeForTodayBll
    {
        public int id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> EarlyTermination { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }
        public Nullable<int> StatusId { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
    }
}
