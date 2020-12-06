using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ListOfGoodsBll
    {
        public ListOfGoodsBll()
        {
            this.goodsSold = new HashSet<GoodsSoldBll>();
            this.procurement = new HashSet<ProcurementBll>();
        }

        public int idListOfGoods { get; set; }
        public Nullable<int> productCategoriesId { get; set; }
        public string name { get; set; }
        public string productDescription { get; set; }
        public string urlFoto { get; set; }
        public Nullable<bool> visible { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<double> salaryFromSale { get; set; }

        public virtual ICollection<GoodsSoldBll> goodsSold { get; set; }
        public virtual ProductCategoriesBll ProductCategories { get; set; }
        public virtual ICollection<ProcurementBll> procurement { get; set; }
    }
}
