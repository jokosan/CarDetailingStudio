using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class TireRadiusBll
    {
        public TireRadiusBll()
        {
            this.PriceListTireFitting = new HashSet<PriceListTireFittingBll>();
        }

        public int idTireRadius { get; set; }
        public string radius { get; set; }
        public Nullable<int> number { get; set; }
        public virtual ICollection<PriceListTireFittingBll> PriceListTireFitting { get; set; }
    }
}
