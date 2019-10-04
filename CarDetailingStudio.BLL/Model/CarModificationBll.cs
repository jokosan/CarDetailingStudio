using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CarModificationBll
    {
        public int id { get; set; }
        public int id_car_modification { get; set; }
        public Nullable<int> id_car_serie { get; set; }
        public Nullable<int> id_car_model { get; set; }
        public string name { get; set; }
        public Nullable<double> start_production_year { get; set; }
        public string end_production_year { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }

        public virtual CarSerieBll car_serie { get; set; }
    }
}
