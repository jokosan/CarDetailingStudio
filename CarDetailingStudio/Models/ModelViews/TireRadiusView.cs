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
        public double idTireRadius { get; set; }
        public string radius { get; set; }

        public virtual ICollection<PriceListTireFittingView> PriceListTireFitting { get; set; }
    }
}