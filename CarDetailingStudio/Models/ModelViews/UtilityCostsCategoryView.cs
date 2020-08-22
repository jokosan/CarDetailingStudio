using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class UtilityCostsCategoryView
    {
        public UtilityCostsCategoryView()
        {
            this.utilityCosts = new HashSet<UtilityCostsView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idUtilityCostsCategory { get; set; }
        public string Named { get; set; }

        public virtual ICollection<UtilityCostsView> utilityCosts { get; set; }
    }
}