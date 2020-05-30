using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class SalaryBalanceView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idSalaryBalance { get; set; }
        public Nullable<int> CarWashWorkersId { get; set; }
        public Nullable<System.DateTime> dateOfPayment { get; set; }
        public Nullable<double> payoutAmount { get; set; }
        public Nullable<double> accountBalance { get; set; }
        public bool currentMonthStatus { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
    }
}