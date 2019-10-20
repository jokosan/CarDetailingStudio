using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OrderCarWashWorkersView
    {
        public int Id { get; set; }
        public Nullable<int> IdOrder { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }
        public Nullable<bool> CalculationStatus { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
    }
}