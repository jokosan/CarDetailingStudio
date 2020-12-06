using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ProductCategoriesBll
    {
        public ProductCategoriesBll()
        {
            this.listOfGoods = new HashSet<ListOfGoodsBll>();
        }

        public int idProductСategories { get; set; }
        public string Name { get; set; }
        public string img { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListOfGoodsBll> listOfGoods { get; set; }
    }
}
