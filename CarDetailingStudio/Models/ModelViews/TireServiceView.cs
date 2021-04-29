using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class TireServiceView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idTireService { get; set; }
        public int clientsOfCarWashId { get; set; }
        public int orderServicesCarWashId { get; set; }
        public int priceListTireFittingAdditionalServicesId { get; set; }
        public Nullable<double> priceTireFitting { get; set; }

        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
        public virtual PriceListTireFittingAdditionalServicesView PriceListTireFittingAdditionalServices { get; set; }
    }
}