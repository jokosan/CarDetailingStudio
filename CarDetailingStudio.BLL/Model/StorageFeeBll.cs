using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class StorageFeeBll
    {
        public StorageFeeBll()
        {
            this.TireStorage = new HashSet<TireStorageBll>();
        }

        public int idStorageFee { get; set; }
        public Nullable<System.DateTime> DateOfPayment { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<int> storageTime { get; set; }
        public Nullable<bool> storageStatus { get; set; }
        public virtual ICollection<TireStorageBll> TireStorage { get; set; }
    }
}
