using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderCarWashWorkersBll
    {
        public int Id { get; set; }
        public Nullable<int> IdOrder { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }
        public Nullable<bool> CalculationStatus { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
    }
}
