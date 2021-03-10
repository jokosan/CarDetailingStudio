using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView
{
    public class AnalyticsFull
    {
        public AnalyticsView analyticsViewResult { get; set; }
        public AnalyticsView analyticsViewCash { get; set; }
        public AnalyticsView analyticsViewNoCash { get; set; }
        public double cashStartOfTheDay { get; set; }
        public double CashEndDay { get; set; } // Касса конец дня
        public double CashEndDayCash { get; set; } // Касса конец дня
        public double CashEndDayNoCash { get; set; } // Касса конец дня
        public double SumWegesAdministrator { get; set; }
        public double SumWegesEmployees { get; set; }
        public double SumPendingPayment { get; set; } // Сумма заказов ожидающих оплат
    }
}