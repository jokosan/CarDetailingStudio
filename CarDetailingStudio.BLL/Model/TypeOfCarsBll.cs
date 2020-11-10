using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class TypeOfCarsBll
    {
        public TypeOfCarsBll()
        {
            this.PriceListTireFitting = new HashSet<PriceListTireFittingBll>();
        }

        public double idTypeOfCars { get; set; }
        public string Type { get; set; }

        public virtual ICollection<PriceListTireFittingBll> PriceListTireFitting { get; set; }
    }
}
