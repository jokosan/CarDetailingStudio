using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CarSerieBll
    {
        public CarSerieBll()
        {
            this.car_modification = new HashSet<CarModificationBll>();
        }

        public int id_car_serie { get; set; }
        public Nullable<int> id_car_model { get; set; }
        public string name { get; set; }
        public Nullable<int> id_car_generation { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }
        public string idbody { get; set; }

        public virtual CarGenerationBll car_generation { get; set; }
        public virtual ICollection<CarModificationBll> car_modification { get; set; }
    }
}
