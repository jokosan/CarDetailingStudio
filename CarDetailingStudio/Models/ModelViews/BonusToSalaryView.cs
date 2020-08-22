using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class BonusToSalaryView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idBonusToSalary { get; set; }
        public Nullable<int> carWashWorkersId { get; set; }
        
        [Display (Name = "Сумма премии")]
        public Nullable<double> amount { get; set; }
        
        [DataType(DataType.Date)]
        [Display (Name = "Дата начисления премии")]
        public Nullable<System.DateTime> date { get; set; }
        
        public string note { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
    }
}