using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class EmployeeRateView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idEmployeeRate { get; set; }
        public Nullable<int> brigadeForTodayId { get; set; }
        [Display(Name = "Отработанные часы")]
        public Nullable<int> numberHoursWorked { get; set; }
        [Display(Name = "Ставка за час")]
        public Nullable<double> hourlyRate { get; set; }
        [Display(Name = "Оплата за ставку")]
        public Nullable<double> wage { get; set; }

        public virtual BrigadeForTodayView brigadeForToday { get; set; }
    }
}