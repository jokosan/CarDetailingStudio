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

        public BrigadeForTodayView()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string EarlyTermination { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
   
        public virtual ICollection<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
    }
}