using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class WageView
    {
        public int Id { get; set; }
        public int IdCarWashWorkers { get; set; }
        public System.DateTime Date { get; set; }
        public double Salary { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
    }
}