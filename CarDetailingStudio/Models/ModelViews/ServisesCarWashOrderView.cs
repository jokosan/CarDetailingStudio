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
        public ServisesCarWashOrderView()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int IdClientsOfCarWash { get; set; }
        public int IdOrderServicesCarWash { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public Nullable<int> IdWashServices { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual DetailingsView Detailings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
        public virtual WashServicesView WashServices { get; set; }
    }
}