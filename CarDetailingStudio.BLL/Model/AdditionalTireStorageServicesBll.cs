using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class AdditionalTireStorageServicesBll
    {
        public int IdAdditionalTireStorageServices { get; set; }
        public int clientsOfCarWashId { get; set; }
        public int orderServicesCarWashId { get; set; }
        public int tireStorageServicesd { get; set; }
        public double Price { get; set; }

        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
        public virtual TireStorageServicesBll TireStorageServices { get; set; }
    }
}
