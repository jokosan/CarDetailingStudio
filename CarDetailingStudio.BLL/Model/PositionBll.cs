using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class PositionBll
    {
        public PositionBll()
        {
            this.premiumAndRate = new HashSet<PremiumAndRateBll>();
        }
        public int idPosition { get; set; }
        public string name { get; set; }
        public Nullable<int> positionsOfAdministrators { get; set; }
        public Nullable<int> servises { get; set; }

        public virtual ICollection<PremiumAndRateBll> premiumAndRate { get; set; }
    }
}
