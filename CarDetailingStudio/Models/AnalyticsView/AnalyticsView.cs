using CarDetailingStudio.Models.AnalyticsView.CompletedOrders;
using CarDetailingStudio.Models.AnalyticsView.WagesForCompletedOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView
{
    public class AnalyticsView : AnalyticsIncomeView
    {
        public ExpensesView expenses { get; set; }
        public List<ExpensesClassView> expensesClassModels { get; set; }
        public CompletedOrdersView completedOrders { get; set; }
        public WagesForCompletedOrdersView wagesForCompletedOrders { get; set; }
        public AdditionalIncomeView additionalIncome { get; set; }
        public SaleOfGoodsView saleOfGoods { get; set; }
        public InformationPreviousPeriod informationPreviousPeriod { get; set; }
    }
}