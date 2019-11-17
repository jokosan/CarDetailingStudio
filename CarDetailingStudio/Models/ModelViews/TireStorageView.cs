using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class TireStorageView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Nullable<int> ServicesId { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> BrigadeId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public virtual ClientsOfCarWashView ClientsOfCarWash { get; set; }
        public virtual TireStorageServicesView TireStorageServices { get; set; }
    }
}