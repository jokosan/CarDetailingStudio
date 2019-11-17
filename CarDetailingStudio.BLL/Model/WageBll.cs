using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class WageBll
    {
        public int Id { get; set; }
        public int IdCarWashWorkers { get; set; }
        public int CostsId { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
        public virtual CostsBll Costs { get; set; }
    }
}
