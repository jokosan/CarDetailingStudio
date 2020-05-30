using System;

namespace CarDetailingStudio.BLL.Model
{
    public class TireStorageServicesBll
    {
        public int Id { get; set; }
        public string ServicesName { get; set; }
        public Nullable<int> radius { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<int> storageTime { get; set; }
        public Nullable<double> Price { get; set; }
    }
}
