using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class SalaryExpensesView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idSalaryExpenses { get; set; }

        [Display(Name = "№")]
        public Nullable<int> idCarWashWorkers { get; set; }
        public Nullable<int> expenseId { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
        public virtual ExpensesView Expenses { get; set; }
    }
}