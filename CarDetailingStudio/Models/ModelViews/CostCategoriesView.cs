using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CostCategoriesView
    {
        public CostCategoriesView()
        {
            this.Expenses = new HashSet<ExpensesView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idCostCategories { get; set; }
        public Nullable<int> typeOfExpenses { get; set; }

        [Display (Name = "Под категория расходов")]
        public string Name { get; set; }

        public virtual ExpenseCategoryView expenseCategory { get; set; }
        public virtual ICollection<ExpensesView> Expenses { get; set; }
    }
}