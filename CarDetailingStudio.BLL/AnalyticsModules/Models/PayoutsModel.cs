using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    public class PayoutsModel
    {
        public int IdPayouts { get; set; }
        public string ServicesOfPayouts { get; set; }
        public DateTime DateOfPayouts { get; set; }
        public double SumOfPayouts { get; set; }
    }
}
