using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CostsView
    {
        
        public CostsView()
        {
            this.PurchaseCosts = new HashSet<PurchaseCostsView>();
            this.Wage = new HashSet<WageView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Nullable<int> Type { get; set; }
        public string Name { get; set; }
        public Nullable<double> expenses { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public virtual TypeOfCostsView TypeOfCosts { get; set; }
        public virtual ICollection<PurchaseCostsView> PurchaseCosts { get; set; }
        public virtual ICollection<WageView> Wage { get; set; }
    }
}