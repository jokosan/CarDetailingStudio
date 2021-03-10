using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class entityEmployeeModel
    {
        public CarWashWorkersView carWashWorkers { get; set; }
        public PremiumAndRateView premiumAndRates { get; set; }
    }
}