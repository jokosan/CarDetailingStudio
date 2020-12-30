using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class BrigadeForTodayView
    {
        public BrigadeForTodayView()
        {
            this.EmployeeRate = new HashSet<EmployeeRateView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        [Display (Name = "Начало смены")]
        public Nullable<System.DateTime> Date { get; set; }
        
        [Display (Name = "Завершение смены")]
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> EarlyTermination { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }
        public Nullable<int> StatusId { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
        public virtual ICollection<EmployeeRateView> EmployeeRate { get; set; }
    }
}