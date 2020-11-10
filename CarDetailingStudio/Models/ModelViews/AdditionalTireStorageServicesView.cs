using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class AdditionalTireStorageServicesView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdAdditionalTireStorageServices { get; set; }
        public int clientsOfCarWashId { get; set; }
        public int orderServicesCarWashId { get; set; }
        public int tireStorageServicesd { get; set; }
        public double Price { get; set; }

        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
        public virtual TireStorageServicesView TireStorageServices { get; set; }
    }
}