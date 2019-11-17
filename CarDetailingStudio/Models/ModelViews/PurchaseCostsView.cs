using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class PurchaseCostsView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Nullable<int> IdRetail { get; set; }
        public Nullable<int> IdCosts { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Amount { get; set; }

        public virtual CostsView Costs { get; set; }
        public virtual RetailView Retail { get; set; }
    }
}