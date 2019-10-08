using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class DetailingsBll
    {
        public DetailingsBll()
        {
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrderBll>();
        }

        public int Id { get; set; }
        public string services_list { get; set; }
        public string validity { get; set; }
        public string note { get; set; }
        public Nullable<double> S { get; set; }
        public Nullable<double> M { get; set; }
        public Nullable<double> L { get; set; }
        public Nullable<double> XL { get; set; }
        public string group { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
        public Nullable<bool> mark { get; set; }
        public Nullable<int> IdGroupWashServices { get; set; }
        public Nullable<int> IdTypeService { get; set; }

        public virtual GroupWashServicesBll GroupWashServices { get; set; }
        public virtual ICollection<ServisesCarWashOrderBll> ServisesCarWashOrder { get; set; }
    }
}
