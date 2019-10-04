using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class GroupWashServicesBll
    {
        public GroupWashServicesBll()
        {
            this.Detailings = new HashSet<DetailingsBll>();
            this.WashServices = new HashSet<WashServicesBll>();
        }

        public int Id { get; set; }
        public string group { get; set; }

        public virtual ICollection<DetailingsBll> Detailings { get; set; }
        public virtual ICollection<WashServicesBll> WashServices { get; set; }
    }
}
