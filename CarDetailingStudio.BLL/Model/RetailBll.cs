using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class RetailBll
    {
     
        public RetailBll()
        {
            this.PurchaseCosts = new HashSet<PurchaseCostsBll>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> QuantityInStock { get; set; }

        public virtual ICollection<PurchaseCostsBll> PurchaseCosts { get; set; }
    }
}
