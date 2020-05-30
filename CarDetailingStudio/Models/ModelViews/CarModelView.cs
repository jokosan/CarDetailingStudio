using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CarModelView
    {
        public CarModelView()
        {
            this.car_generation = new HashSet<CarGenerationView>();
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashView>();

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_car_model { get; set; }
        public Nullable<int> id_car_mark { get; set; }

        [Display(Prompt = "Model")]
        public string name { get; set; }
        public string name_rus { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }

        public virtual ICollection<CarGenerationView> car_generation { get; set; }
        public virtual CarMarkView car_mark { get; set; }
        public virtual ICollection<ClientsOfCarWashView> ClientsOfCarWash { get; set; }
    }
}