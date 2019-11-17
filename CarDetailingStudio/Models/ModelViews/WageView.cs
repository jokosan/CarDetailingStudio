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
        public int CostsId { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
        public virtual CostsView Costs { get; set; }
    }
}