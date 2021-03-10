using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class PremiumAndRateView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idPremiumAndRate { get; set; }
        public int carWashWorkersId { get; set; }
        
        [Display(Name = "Статус процента за заказ")]
        public bool percentageStatusForOrder { get; set; }
        
        [Display(Name = "Процентная ставка за заказ")]
        public Nullable<double> percentageRatePerOrder { get; set; }
        
        [Display(Name = "Статус работы")]
        public bool positionsStatus { get; set; }         
      
        [Display(Name = "Должность")]
        public int positionId { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
        public virtual PositionView Position { get; set; }
    }
}