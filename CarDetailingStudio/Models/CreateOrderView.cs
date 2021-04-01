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
        public ClientsOfCarWashView Client { get; set; }
        public List<CarWashWorkersView> CarWashWorkers { get; set; }
        public Nullable<int> client { get; set; }
        public Nullable<int> numberOfTires { get; set; } = 0;
        public double Total { get; set; }
        public double PriceListTireFittingsSum { get; set; } = 0;
        public double discontClient { get; set; } = 0;

    }
}