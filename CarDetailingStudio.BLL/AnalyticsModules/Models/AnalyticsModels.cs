using CarDetailingStudio.BLL.AnalyticsModules.Models.CompletedOrders;
using CarDetailingStudio.BLL.AnalyticsModules.Models.WagesForCompletedOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    public class AnalyticsModels : AnalyticsIncomeModel
    {
        public ExpensesModels expenses { get; set; }
        public List<ExpensesClassModels> expensesClassModels { get; set; }
        public CompletedOrdersModels completedOrders { get; set; }
        public WagesForCompletedOrdersModels wagesForCompletedOrders { get; set; }
        public AdditionalIncomeModels additionalIncome { get; set; }
        public SaleOfGoodsModels saleOfGoods { get; set; }
        public InformationPreviousPeriod informationPreviousPeriod{ get; set; }
        
    }
}
