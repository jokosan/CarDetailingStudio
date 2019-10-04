using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ServisesCarWashOrderBll
    {
        public ServisesCarWashOrderBll()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashBll>();
        }

        public int Id { get; set; }
        public int IdClientsOfCarWash { get; set; }
        public int IdOrderServicesCarWash { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public Nullable<int> IdWashServices { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual DetailingsBll Detailings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderServicesCarWashBll> OrderServicesCarWash { get; set; }
        public virtual WashServicesBll WashServices { get; set; }
    }
}
