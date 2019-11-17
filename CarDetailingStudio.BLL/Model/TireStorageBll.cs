using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class TireStorageBll
    {
        public int Id { get; set; }
        public Nullable<int> ServicesId { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> BrigadeId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public virtual ClientsOfCarWashBll ClientsOfCarWash { get; set; }
        public virtual TireStorageServicesBll TireStorageServices { get; set; }
    }
}
