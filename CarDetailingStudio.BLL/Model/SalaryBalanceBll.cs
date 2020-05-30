using System;

namespace CarDetailingStudio.BLL.Model
{
    public class SalaryBalanceBll
    {
        public int idSalaryBalance { get; set; }
        public Nullable<int> CarWashWorkersId { get; set; }
        public Nullable<System.DateTime> dateOfPayment { get; set; }
        public Nullable<double> payoutAmount { get; set; }
        public Nullable<double> accountBalance { get; set; }
        public bool currentMonthStatus { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
    }
}
