using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class TireStorageServicesBll
    {
        public TireStorageServicesBll()
        {
            this.TireStorage = new HashSet<TireStorageBll>();
        }

        public int Id { get; set; }
        public string ServicesName { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<double> Price { get; set; }
        
        public virtual ICollection<TireStorageBll> TireStorage { get; set; }
    }
}
