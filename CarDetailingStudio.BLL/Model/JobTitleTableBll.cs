using System.Collections.Generic;

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
