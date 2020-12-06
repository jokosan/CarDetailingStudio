using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class ExpenseCategoryBll
    {
        public ExpenseCategoryBll()
        {
            this.costCategories = new HashSet<CostCategoriesBll>();
            this.Expenses = new HashSet<ExpensesBll>();
        }

        public int idExpenseCategory { get; set; }
        public string name { get; set; }

        public virtual ICollection<CostCategoriesBll> costCategories { get; set; }
        public virtual ICollection<ExpensesBll> Expenses { get; set; }
    }
}
