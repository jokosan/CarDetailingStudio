using System;

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
