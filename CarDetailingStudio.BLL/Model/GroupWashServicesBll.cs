using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class GroupWashServicesBll
    {
        public GroupWashServicesBll()
        {
            this.Detailings = new HashSet<DetailingsBll>();
        }

        public int Id { get; set; }
        public string group { get; set; }

        public virtual ICollection<DetailingsBll> Detailings { get; set; }
    }
}
