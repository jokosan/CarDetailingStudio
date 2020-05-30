using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class TypeServicesBll
    {
        public TypeServicesBll()
        {
            this.costsCarWashAndDeteyling = new HashSet<CostsCarWashAndDeteylingBll>();
        }

        public int idTypeServices { get; set; }
        public string name { get; set; }
        public virtual ICollection<CostsCarWashAndDeteylingBll> costsCarWashAndDeteyling { get; set; }
    }
}
