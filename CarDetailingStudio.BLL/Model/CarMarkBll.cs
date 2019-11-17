using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CarMarkBll
    {
        public CarMarkBll()
        {
            this.car_model = new List<CarModelBll>();
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashBll>();
        }

        public int id_car_mark { get; set; }
        public string name { get; set; }
        public string name_rus { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }
        public string ImageMark { get; set; }


        public virtual ICollection<CarModelBll> car_model { get; set; }

        public virtual ICollection<ClientsOfCarWashBll> ClientsOfCarWash { get; set; }


    }
}
