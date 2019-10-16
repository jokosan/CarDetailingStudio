using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ServisesCarWashOrderBll
    {
        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> IdOrderServicesCarWash { get; set; }
        public Nullable<int> IdWashServices { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual DetailingsBll Detailings { get; set; }
        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
    }
}
