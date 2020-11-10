using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class PriceListTireFittingBll
    {
        public PriceListTireFittingBll()
        {
            this.tireChangeService = new HashSet<TireChangeServiceBll>();
        }
        public int idPriceListTireFitting { get; set; }
        public string JobTitle { get; set; }
        public Nullable<double> TheCost { get; set; }
        public Nullable<double> TireRadiusId { get; set; }
        public Nullable<double> TypeOfCarsId { get; set; }

        public virtual TireRadiusBll TireRadius { get; set; }
        public virtual TypeOfCarsBll TypeOfCars { get; set; }

        public virtual ICollection<TireChangeServiceBll> tireChangeService { get; set; }
    }
}
