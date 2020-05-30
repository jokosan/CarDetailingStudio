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

        [Display(Name = "Сумма выдачи")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<double> amount { get; set; }

        [Display(Name = "Дата выдачи")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<System.DateTime> dateExpenses { get; set; }

        [Display(Name = "Категория расходов")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
        public virtual ExpenseCategoryView expenseCategory { get; set; }
    }
}