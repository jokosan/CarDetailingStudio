using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CreditBll
    {
        public int Id { get; set; }
        public Nullable<int> CarWashWorkersId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> Sum { get; set; }
        public Nullable<bool> RepaidDebt { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
    }
}
