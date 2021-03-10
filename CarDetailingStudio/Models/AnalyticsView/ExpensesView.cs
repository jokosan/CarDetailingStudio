using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView
{
    public class ExpensesView
    {
        public double AmountExpenses { get; set; }  // Сумма расходов
        public double PayrollCosts { get; set; }    // Расходы на заработную плату        
        public double Utilities { get; set; }       // Расходы камунальные услуги
        public double WashingCosts { get; set; }     // Затраты мойки
        public double DetailingCosts { get; set; }   // Затраты детейлинг
        public double TireCosts { get; set; }        // Затраты шиномонтаж 
        public double OtherExpenses { get; set; }     // Прочие расходы
    }

    public class ExpensesClassView
    {

        public int expenseCategoryId { get; set; }
        public string expenseCategoryName { get; set; }
        public Nullable<System.DateTime> dateExpenses { get; set; }
        public double Amount { get; set; }
        public int CountExpenses { get; set; }
        public Nullable<int> paymentType { get; set; }
        public string note { get; set; }
        public Nullable<int> idCostCategories { get; set; }
        public string NameCostCategories { get; set; }
        public string nameEmployees { get; set; }

    }
}