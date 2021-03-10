using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class PremiumAndRateBll
    {
        public int idPremiumAndRate { get; set; }
        public int carWashWorkersId { get; set; }
        public bool percentageStatusForOrder { get; set; }
        public Nullable<double> percentageRatePerOrder { get; set; }
        public bool positionsStatus { get; set; }
        public int positionId { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
        public virtual PositionBll Position { get; set; }
    }
}
