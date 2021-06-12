using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    public class AnalyticsIncomeModel
    {
        public List<IncomeModel> incomeViews { get; set; }
        public List<IncomeModel> paymentofArrears { get; set; }
    }
}
