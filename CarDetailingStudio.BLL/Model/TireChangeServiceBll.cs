using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class TireChangeServiceBll
    {
        public int idTireChangeService { get; set; }
        public int idOrder { get; set; }
        public int priceListTireFittingId { get; set; }
        public Nullable<int> numberOfTires { get; set; }
        public Nullable<int> tireRadius { get; set; }
        public Nullable<double> price { get; set; }

        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
        public virtual PriceListTireFittingBll PriceListTireFitting { get; set; }
    }
}
