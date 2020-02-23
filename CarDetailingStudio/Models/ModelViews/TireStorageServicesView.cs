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
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Display(Name ="Услуга")]
        public string ServicesName { get; set; }

        [Display (Name ="Радиус шин")]
        public Nullable<int> radius { get; set; }

        [Display (Name ="количество (шт)")]
        public Nullable<int> amount { get; set; }

        [Display (Name ="Период хранения")]
        public Nullable<int> storageTime { get; set; }

        [Display (Name ="Цена")]
        public Nullable<double> Price { get; set; }
    }
}