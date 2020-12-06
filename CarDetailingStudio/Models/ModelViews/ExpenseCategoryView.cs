using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ExpenseCategoryView
    {
        public ExpenseCategoryView()
        {
            this.costCategories = new HashSet<CostCategoriesView>();
            this.Expenses = new HashSet<ExpensesView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idExpenseCategory { get; set; }
        [Display (Name = "Категория расходов")]
        public string name { get; set; }


        public virtual ICollection<CostCategoriesView> costCategories { get; set; }
        public virtual ICollection<ExpensesView> Expenses { get; set; }
    }
}