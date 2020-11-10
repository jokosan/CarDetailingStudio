using System;

namespace CarDetailingStudio.BLL.Model
{
    public class TireStorageBll
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> dateOfAdoption { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> radius { get; set; }
        public string firm { get; set; }
        public Nullable<int> discAvailability { get; set; }
        public Nullable<int> storageFeeId { get; set; }
        public Nullable<int> tireStorageBags { get; set; }
        public Nullable<int> wheelWash { get; set; }
        public Nullable<int> IdOrderServicesCarWash { get; set; }
        public Nullable<int> RelatedOrders { get; set; }
        public Nullable<int> silicone { get; set; }
        public Nullable<double> serviceCostTirePackages { get; set; }

        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
        public virtual StorageFeeBll storageFee { get; set; }
    }
}
