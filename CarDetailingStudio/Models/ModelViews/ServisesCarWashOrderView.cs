using System;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ServisesCarWashOrderView
    {
        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> IdOrderServicesCarWash { get; set; }
        public Nullable<int> IdWashServices { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual DetailingsView Detailings { get; set; }
        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
    }
}