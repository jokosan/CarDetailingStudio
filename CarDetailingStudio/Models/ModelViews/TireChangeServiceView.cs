using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class TireChangeServiceView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idTireChangeService { get; set; }
        public int idOrder { get; set; }
        public int priceListTireFittingId { get; set; }
        public Nullable<int> numberOfTires { get; set; }
        public Nullable<int> tireRadius { get; set; }
        public Nullable<double> price { get; set; }

        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
        public virtual PriceListTireFittingView PriceListTireFitting { get; set; }
    }
}