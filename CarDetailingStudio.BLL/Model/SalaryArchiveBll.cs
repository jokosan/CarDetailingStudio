using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class SalaryArchiveBll
    {
        public int idSalaryArchive { get; set; }
        public int carWashWorkersId { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> amountOfCompletedOrders { get; set; }
        public Nullable<double> monthlySalaryAmountEarned { get; set; }
        public Nullable<double> monthlySalaryAmountReceived { get; set; }
        public Nullable<double> balanceAtTheEndOfTheMonth { get; set; }
        public Nullable<bool> salaryStatus { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
    }
}
