using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class TireServiceBll
    {
        public int idTireService { get; set; }
        public int clientsOfCarWashId { get; set; }
        public int orderServicesCarWashId { get; set; }
        public int priceListTireFittingAdditionalServicesId { get; set; }
        public Nullable<double> priceTireFitting { get; set; }

        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
        public virtual PriceListTireFittingAdditionalServicesBll PriceListTireFittingAdditionalServices { get; set; }
    }
}
