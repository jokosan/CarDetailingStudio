using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OtherExpensesView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idOtherExpenses { get; set; }
        [Display(Name = "Наименование расходов")]
        public string nameExpenses { get; set; }
        [Display(Name = "Сумма")]
        public Nullable<double> amount { get; set; }
       [Display(Name ="Дата")]
        public Nullable<System.DateTime> dateExpenses { get; set; }
        [Display(Name = "Категория расходов")]
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual ExpenseCategoryView expenseCategory { get; set; }
    }
}