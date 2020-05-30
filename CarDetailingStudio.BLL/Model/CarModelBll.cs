using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class CarModelBll
    {
        public CarModelBll()
        {
            this.car_generation = new HashSet<CarGenerationBll>();
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashBll>();
        }

        public int id_car_model { get; set; }
        public Nullable<int> id_car_mark { get; set; }
        public string name { get; set; }
        public string name_rus { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }

        public virtual ICollection<CarGenerationBll> car_generation { get; set; }
        public virtual CarMarkBll car_mark { get; set; }
        public virtual ICollection<ClientsOfCarWashBll> ClientsOfCarWash { get; set; }
    }
}
