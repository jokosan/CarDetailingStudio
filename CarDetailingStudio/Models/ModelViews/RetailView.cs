using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class RetailView
    {
        public RetailView()
        {
            this.PurchaseCosts = new HashSet<PurchaseCostsView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> QuantityInStock { get; set; }

     
        public virtual ICollection<PurchaseCostsView> PurchaseCosts { get; set; }
    }
}