using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class StorageFeeView
    {
        public StorageFeeView()
        {
            this.TireStorage = new HashSet<TireStorageView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idStorageFee { get; set; }

        [Display(Name = "Дата оплаты")]
        public Nullable<System.DateTime> DateOfPayment { get; set; }
        [Display(Name = "Сумма оплаты")]
        public Nullable<double> amount { get; set; }
        [Display(Name = "Количество месяцев хранения")]
        public Nullable<int> storageTime { get; set; }
        public Nullable<bool> storageStatus { get; set; }
        public virtual ICollection<TireStorageView> TireStorage { get; set; }
    }
}