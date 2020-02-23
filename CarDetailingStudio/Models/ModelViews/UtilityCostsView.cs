using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class UtilityCostsView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idUtilityCosts { get; set; }
        [Display(Name = "Показание счетчика")]
        public Nullable<int> indicationCounter { get; set; }
        [Display(Name = "Сумма")]
        public Nullable<double> amount { get; set; }
        [Display(Name = "Дата")]
        public Nullable<System.DateTime> dateExpenses { get; set; }
        [Display(Name = "Категория расходов")]
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual ExpenseCategoryView expenseCategory { get; set; }
    }
}