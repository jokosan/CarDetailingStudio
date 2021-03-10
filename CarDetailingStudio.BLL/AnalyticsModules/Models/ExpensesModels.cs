using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    public class ExpensesModels
    {
        [Display(Name = "Сумма всех раходов")]
        public double AmountExpenses { get; set; } 

        [Display(Name = "Расходы по выплаченной заработной плате")]
        public double PayrollCosts { get; set; }   

        [Display(Name = "Расходы по комунальным услугам")]
        public double Utilities { get; set; } 

        [Display(Name = "Затраты мойки")]
        public double WashingCosts { get; set; }  

        [Display(Name = "Затраты детейлинг")]
        public double DetailingCosts { get; set; }

        [Display(Name = "Затраты шиномонтаж")]
        public double TireCosts { get; set; }  

        [Display(Name = "Прочие расходы")]
        public double OtherExpenses{ get; set; }
    }

    public class ExpensesClassModels
    {     
        public int expenseCategoryId { get; set; }
        public string expenseCategoryName{ get; set; }
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
