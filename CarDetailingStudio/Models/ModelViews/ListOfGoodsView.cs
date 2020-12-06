using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ListOfGoodsView
    {
        public ListOfGoodsView()
        {
            this.goodsSold = new HashSet<GoodsSoldView>();
            this.procurement = new HashSet<ProcurementView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idListOfGoods { get; set; }
        [Display(Name = "Категория товара")]
        public Nullable<int> productCategoriesId { get; set; }
        [Display (Name ="Наименование")]
        public string name { get; set; }
        [Display(Name = "Описание товара")]
        public string productDescription { get; set; }
        [Display(Name = "Картинка товара")]
        public string urlFoto { get; set; }
        [Display(Name = "Видимость товара")]
        public Nullable<bool> visible { get; set; }
        [Display(Name = "Цена")]
        public Nullable<double> price { get; set; }
        [Display(Name = "ЗП с продажи")]
        public Nullable<double> salaryFromSale { get; set; }

        public virtual ICollection<GoodsSoldView> goodsSold { get; set; }
        public virtual ProductCategoriesView ProductCategories { get; set; }
        public virtual ICollection<ProcurementView> procurement { get; set; }
    }
}