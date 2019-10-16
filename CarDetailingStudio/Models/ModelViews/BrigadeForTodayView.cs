using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class BrigadeForTodayView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> EarlyTermination { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
    }
}