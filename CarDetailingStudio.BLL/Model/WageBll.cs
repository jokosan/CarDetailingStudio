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
        public System.DateTime Date { get; set; }
        public double Salary { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
    }
}
