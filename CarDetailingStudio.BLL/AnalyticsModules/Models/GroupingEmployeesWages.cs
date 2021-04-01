using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
   public class GroupingEmployeesWages
    {
        public int idEmployees { get; set; }
        public DateTime date { get; set; }
        public string nameEmployees { get; set; }
        public int orderCount { get; set; }
        public double wegesSum { get; set; }
        public double orderSum { get; set; }
        public int idOrder { get; set; }
        public double bonus { get; set; }
    }
}
