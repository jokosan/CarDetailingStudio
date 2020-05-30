using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CarModificationView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
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

        public CarSerieView car_serie { get; set; }
    }
}