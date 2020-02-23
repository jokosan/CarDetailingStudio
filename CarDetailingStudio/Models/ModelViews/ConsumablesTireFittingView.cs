using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ConsumablesTireFittingView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idConsumablesTireFitting { get; set; }
        [Display(Name = "Название товара")]
        public string nameExpenses { get; set; }
        [Display(Name = "Сумма")]
        public Nullable<double> amount { get; set; }
        [Display(Name = "Дата")]
        public Nullable<System.DateTime> dateExpenses { get; set; }
        [Display(Name = "Категория расходов")]
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual ExpenseCategoryView expenseCategory { get; set; }
    }
}