using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class UtilityCostsView : ExpenseCategoryGroupView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idUtilityCosts { get; set; }

        [Display(Name = "Показание счетчика")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<int> indicationCounter { get; set; }
        public Nullable<int> utilityCostsCategoryId { get; set; }
        public Nullable<int> expenseId { get; set; }

        public virtual ExpensesView Expenses { get; set; }
        public virtual UtilityCostsCategoryView utilityCostsCategory { get; set; }

    }
}