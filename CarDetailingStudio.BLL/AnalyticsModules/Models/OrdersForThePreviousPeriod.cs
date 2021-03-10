using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    public class OrdersForThePreviousPeriod
    {
        public double OrderCount { get; set; }
        public double OrderSum { get; set; } = 0;
    }
}
