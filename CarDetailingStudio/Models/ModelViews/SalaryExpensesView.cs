using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class SalaryExpensesView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idSalaryExpenses { get; set; }
        [Display(Name ="№")]
        public Nullable<int> idCarWashWorkers { get; set; }
        [Display(Name = "Сумма выдачи")]
        public Nullable<double> amount { get; set; }
        [Display(Name = "Дата выдачи")]
        public Nullable<System.DateTime> dateExpenses { get; set; }
        [Display(Name = "Категория расходов")]
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
        public virtual ExpenseCategoryView expenseCategory { get; set; }
    }
}