using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class PriceListTireFittingAdditionalServicesBll
    {
        public PriceListTireFittingAdditionalServicesBll()
        {
            this.tireService = new HashSet<TireServiceBll>();
        }

        public int idPriceListTireFittingAdditionalServices { get; set; }
        public string JobTitle { get; set; }
        public Nullable<double> TheCost { get; set; }
        public Nullable<int> sorting { get; set; }

        public virtual ICollection<TireServiceBll> tireService { get; set; }
    }
}
