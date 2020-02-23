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
        public Nullable<int> ClientId { get; set; }      
        public Nullable<int> carWashWorkersId { get; set; }
        [Display(Name = "Дата оформления заказа")]
        public Nullable<System.DateTime> dateOfAdoption { get; set; }
        [Display(Name = "Количество шин")]
        public Nullable<int> quantity { get; set; }
        [Display(Name = "Радиус")]
        public Nullable<int> radius { get; set; }
        [Display(Name = "Фирма")]
        public string firm { get; set; }
        [Display(Name = "Количество шин с дисками")]
        public Nullable<int> discAvailability { get; set; }

        public Nullable<int> storageFeeId { get; set; }

        [Display(Name = "Количество пакетов (шт)")]
        public Nullable<int> tireStorageBags { get; set; }
        [Display(Name = "Мойка колес (шт)")]
        public Nullable<int> wheelWash { get; set; }

        public Nullable<int> IdOrderServicesCarWash { get; set; }

        [Display(Name = "Селикон (шт)")]
        public Nullable<int> silicone { get; set; }

        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
        public virtual StorageFeeView storageFee { get; set; }
    }
}