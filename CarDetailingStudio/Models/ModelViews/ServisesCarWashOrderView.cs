using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ServisesCarWashOrderView
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> IdOrderServicesCarWash { get; set; }
        public Nullable<int> IdWashServices { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual DetailingsView Detailings { get; set; }
        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
    }
}