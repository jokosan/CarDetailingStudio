using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class WashServicesView
    {
        public WashServicesView()
        {
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrderView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public string Services_list { get; set; }
        public Nullable<double> S { get; set; }
        public Nullable<double> M { get; set; }
        public Nullable<double> L { get; set; }
        public Nullable<double> XL { get; set; }
        public int IdGroupWashServices { get; set; }
        public Nullable<bool> mark { get; set; }

        public virtual GroupWashServicesView GroupWashServices { get; set; }
        public virtual ICollection<ServisesCarWashOrderView> ServisesCarWashOrder { get; set; }
    }
}