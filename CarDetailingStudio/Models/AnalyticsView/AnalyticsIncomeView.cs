using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView
{
    public class AnalyticsIncomeView
    {
        public List<IncomeView> incomeViews { get; set; }
        public List<IncomeView> paymentofArrears { get; set; }
    }
}