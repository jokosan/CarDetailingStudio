using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class PositionView
    {
        public PositionView()
        {
            this.premiumAndRate = new HashSet<PremiumAndRateView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idPosition { get; set; }
        public string name { get; set; }
        public Nullable<int> positionsOfAdministrators { get; set; }
        public Nullable<int> servises { get; set; }

        public virtual ICollection<PremiumAndRateView> premiumAndRate { get; set; }
    }
}