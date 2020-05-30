using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OtherExpensesView : ExpenseCategoryGroupView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idOtherExpenses { get; set; }

        [Display(Name = "Наименование расходов")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public string nameExpenses { get; set; }

        [Display(Name = "Сумма")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<double> amount { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<System.DateTime> dateExpenses { get; set; }

        [Display(Name = "Категория расходов")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual ExpenseCategoryView expenseCategory { get; set; }
    }
}