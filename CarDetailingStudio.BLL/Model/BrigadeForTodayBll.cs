using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class BrigadeForTodayBll
    {
        public BrigadeForTodayBll()
        {
            this.EmployeeRate = new HashSet<EmployeeRateBll>();
        }

        public int id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> EarlyTermination { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }
        public Nullable<int> StatusId { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
        public virtual ICollection<EmployeeRateBll> EmployeeRate { get; set; }
    }
}
