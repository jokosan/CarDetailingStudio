using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    public class FinanceModel
    {
        public List<IncomeModel> incomeModel { get; set; }
        public List<PayoutsModel> payoutsModel { get; set; }
    }
}
