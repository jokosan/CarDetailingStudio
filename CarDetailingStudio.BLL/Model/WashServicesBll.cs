using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class WashServicesBll
    {
        public WashServicesBll()
        {
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrderBll>();
        }

        public int id { get; set; }
        public string Services_list { get; set; }
        public Nullable<double> S { get; set; }
        public Nullable<double> M { get; set; }
        public Nullable<double> L { get; set; }
        public Nullable<double> XL { get; set; }
        public int IdGroupWashServices { get; set; }
        public Nullable<bool> mark { get; set; }

        public virtual GroupWashServicesBll GroupWashServices { get; set; }
        public virtual ICollection<ServisesCarWashOrderBll> ServisesCarWashOrder { get; set; }
    }
}
