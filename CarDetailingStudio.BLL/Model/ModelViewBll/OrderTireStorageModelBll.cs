using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model.ModelViewBll
{
    public class OrderTireStorageModelBll
    {
        public int Id { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> carWashWorkersId { get; set; }
        public Nullable<System.DateTime> dateOfAdoption { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> radius { get; set; }
        public string firm { get; set; }
        public Nullable<int> discAvailability { get; set; }
        public Nullable<int> storageFeeId { get; set; }
        public Nullable<int> tireStorageBags { get; set; }
        public Nullable<int> wheelWash { get; set; }
        public Nullable<int> IdOrderServicesCarWash { get; set; }
        public Nullable<int> silicone { get; set; }
        public Nullable<int> storageTime { get; set; }
    }
}
