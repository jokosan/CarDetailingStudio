using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class TypeOfCostsBll
    {       
        public TypeOfCostsBll()
        {
            this.Costs = new HashSet<CostsBll>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
       
        public virtual ICollection<CostsBll> Costs { get; set; }
    }
}
