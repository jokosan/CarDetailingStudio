using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CarGenerationView
    {
        public CarGenerationView()
        {
            this.car_serie = new HashSet<CarSerieView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_car_generation { get; set; }
        public string name { get; set; }
        public Nullable<int> id_car_model { get; set; }
        public Nullable<double> year_begin { get; set; }
        public Nullable<double> year_end { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }

        public CarModelView car_model { get; set; }
        public ICollection<CarSerieView> car_serie { get; set; }
    }
}