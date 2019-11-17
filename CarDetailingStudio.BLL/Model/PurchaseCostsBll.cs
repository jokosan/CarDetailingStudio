using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class PurchaseCostsBll
    {
        public int Id { get; set; }
        public Nullable<int> IdRetail { get; set; }
        public Nullable<int> IdCosts { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Amount { get; set; }

        public virtual CostsBll Costs { get; set; }
        public virtual RetailBll Retail { get; set; }
    }
}
