using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CostsBll
    {
        public int Id { get; set; }
        public Nullable<int> Type { get; set; }
        public string Name { get; set; }
        public Nullable<double> expenses { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public virtual TypeOfCostsBll TypeOfCosts { get; set; }
    }
}
