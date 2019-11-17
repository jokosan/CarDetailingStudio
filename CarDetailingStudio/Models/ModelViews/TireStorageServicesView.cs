using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class TireStorageServicesView
    {
        
        public TireStorageServicesView()
        {
            this.TireStorage = new HashSet<TireStorageView>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ServicesName { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual ICollection<TireStorageView> TireStorage { get; set; }
    }
}