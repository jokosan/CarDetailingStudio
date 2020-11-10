using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class TireStorageServicesView
    {
        public TireStorageServicesView()
        {
            this.additionalTireStorageServices = new HashSet<AdditionalTireStorageServicesView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Display(Name = "Услуга")]
        public string ServicesName { get; set; }

        [Display(Name = "Радиус шин")]
        public Nullable<int> radius { get; set; }

        [Display(Name = "количество (шт)")]
        public Nullable<int> amount { get; set; }

        [Display(Name = "Период хранения")]
        public Nullable<int> storageTime { get; set; }

        [Display(Name = "Цена")]
        public Nullable<double> Price { get; set; }

        public virtual ICollection<AdditionalTireStorageServicesView> additionalTireStorageServices { get; set; }
    }
}