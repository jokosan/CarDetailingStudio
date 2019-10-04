using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class GroupWashServicesView
    {
        public GroupWashServicesView()
        {
            this.Detailings = new HashSet<DetailingsView>();
            this.WashServices = new HashSet<WashServicesView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string group { get; set; }

        public virtual ICollection<DetailingsView> Detailings { get; set; }
        public virtual ICollection<WashServicesView> WashServices { get; set; }
    }
}