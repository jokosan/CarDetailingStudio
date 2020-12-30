using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class TireRadiusView 
    {
        public TireRadiusView()
        {
            this.PriceListTireFitting = new HashSet<PriceListTireFittingView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idTireRadius { get; set; }

        [Display(Name = "Радиус")]
        public string radius { get; set; }

        [Display(Name = "Радиус")]
        public Nullable<int> number { get; set; }

        public Nullable<int> id { get;set; }

        public virtual ICollection<PriceListTireFittingView> PriceListTireFitting { get; set; }
    }
}