using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ProductCategoriesView
    {
        public ProductCategoriesView()
        {
            this.listOfGoods = new HashSet<ListOfGoodsView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idProductСategories { get; set; }
        public string Name { get; set; }
        public string img { get; set; }

        public virtual ICollection<ListOfGoodsView> listOfGoods { get; set; }

    }
}