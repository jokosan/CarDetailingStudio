using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class CreateOrderView
    {
        public List<PriceListTireFittingView> priceListTireFittings { get; set; }
        public List<PriceListTireFittingAdditionalServicesView> priceListTireFittingAdditionals { get; set; }
        public Nullable<int> client { get; set; }
        public Nullable<int> numberOfTires { get; set; }

    }
}