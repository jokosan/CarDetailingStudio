using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class SalaryArchiveView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idSalaryArchive { get; set; }
        public int carWashWorkersId { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> amountOfCompletedOrders { get; set; }
        public Nullable<double> monthlySalaryAmountEarned { get; set; }
        public Nullable<double> monthlySalaryAmountReceived { get; set; }
        public Nullable<double> balanceAtTheEndOfTheMonth { get; set; }
        public Nullable<bool> salaryStatus { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
    }
}