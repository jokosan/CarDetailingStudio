using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CarGenerationBll
    {
        public CarGenerationBll()
        {
            this.car_serie = new HashSet<CarSerieBll>();
        }

        public int id_car_generation { get; set; }
        public string name { get; set; }
        public Nullable<int> id_car_model { get; set; }
        public Nullable<double> year_begin { get; set; }
        public Nullable<double> year_end { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }

        public CarModelBll car_model { get; set; }
        public ICollection<CarSerieBll> car_serie { get; set; }
    }
}
