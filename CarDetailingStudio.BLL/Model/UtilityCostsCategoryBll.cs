using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class UtilityCostsCategoryBll
    {
        public UtilityCostsCategoryBll()
        {
            this.utilityCosts = new HashSet<UtilityCostsBll>();
        }

        public int idUtilityCostsCategory { get; set; }
        public string Named { get; set; }

        public virtual ICollection<UtilityCostsBll> utilityCosts { get; set; }
    }
}
