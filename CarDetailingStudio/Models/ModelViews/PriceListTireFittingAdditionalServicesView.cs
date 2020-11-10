using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class PriceListTireFittingAdditionalServicesView
    {
        public PriceListTireFittingAdditionalServicesView()
        {
            this.tireService = new HashSet<TireServiceView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idPriceListTireFittingAdditionalServices { get; set; }
        public string JobTitle { get; set; }
        public Nullable<double> TheCost { get; set; }
        public Nullable<int> sorting { get; set; }

        public virtual ICollection<TireServiceView> tireService { get; set; }
    }
}