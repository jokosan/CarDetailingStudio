using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CarMarkView
    {
        public CarMarkView()
        {
            this.car_model = new List<CarModelView>();
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_car_mark { get; set; }

        [Display(Prompt = "Mark")]
        public string name { get; set; }
        public string name_rus { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }
        public string ImageMark { get; set; }


        public virtual ICollection<CarModelView> car_model { get; set; }

        public virtual ICollection<ClientsOfCarWashView> ClientsOfCarWash { get; set; }

    }
}