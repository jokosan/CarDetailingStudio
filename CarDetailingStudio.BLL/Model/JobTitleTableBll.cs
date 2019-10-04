using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class JobTitleTableBll
    {
        public JobTitleTableBll()
        {
            this.CarWashWorkers = new HashSet<CarWashWorkersBll>();
        }

        public int Id { get; set; }
        public string Position { get; set; }

        public virtual ICollection<CarWashWorkersBll> CarWashWorkers { get; set; }
    }
}
