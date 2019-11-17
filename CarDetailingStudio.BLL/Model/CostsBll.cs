using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CostsBll
    {
     
        public CostsBll()
        {
            this.PurchaseCosts = new HashSet<PurchaseCostsBll>();
            this.Wage = new HashSet<WageBll>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Type { get; set; }
        public string Name { get; set; }
        public Nullable<double> expenses { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual TypeOfCostsBll TypeOfCosts { get; set; }
        public virtual ICollection<PurchaseCostsBll> PurchaseCosts { get; set; }
        public virtual ICollection<WageBll> Wage { get; set; }
    }
}
