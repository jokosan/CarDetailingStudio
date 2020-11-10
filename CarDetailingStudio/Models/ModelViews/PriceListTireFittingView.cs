using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class PriceListTireFittingView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public int idPriceListTireFitting { get; set; }
        public string JobTitle { get; set; }
        public Nullable<double> TheCost { get; set; }
        public Nullable<double> TireRadiusId { get; set; }
        public Nullable<double> TypeOfCarsId { get; set; }

        public virtual TireRadiusView TireRadius { get; set; }
        public virtual TypeOfCarsView TypeOfCars { get; set; }
        public virtual ICollection<TireChangeServiceView> tireChangeService { get; set; }
    }
}