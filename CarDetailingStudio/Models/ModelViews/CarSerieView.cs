using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CarSerieView
    {
        public CarSerieView()
        {
            this.car_modification = new HashSet<CarModificationView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_car_serie { get; set; }
        public Nullable<int> id_car_model { get; set; }
        public string name { get; set; }
        public Nullable<int> id_car_generation { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }
        public string idbody { get; set; }

        public CarGenerationView car_generation { get; set; }
        public ICollection<CarModificationView> car_modification { get; set; }
    }
}