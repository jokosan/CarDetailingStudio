using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class BonusToSalaryBll
    {
        public int idBonusToSalary { get; set; }
        public Nullable<int> carWashWorkersId { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string note { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
    }
}
