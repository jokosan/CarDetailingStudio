using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    public class IncomeModel
    {
        public int IdIncome { get; set; }
        public string ServicesOfInvome { get; set; }
        public DateTime DateOfIncome { get; set; }
        public int CountServices { get; set; }
        public double SumOfIncome { get; set; }
        public double SumNoCash { get; set; }
        public double SumCash { get; set; }
        public double AwaitingPayment { get; set; }
        public double SumTotalIncome { get; set; }
    }
}
