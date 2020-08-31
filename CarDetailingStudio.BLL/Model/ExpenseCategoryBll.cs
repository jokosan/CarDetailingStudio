using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class ExpenseCategoryBll
    {
        public ExpenseCategoryBll()
        {
            this.consumablesTireFitting = new HashSet<ConsumablesTireFittingBll>();
            this.costsCarWashAndDeteyling = new HashSet<CostsCarWashAndDeteylingBll>();
            this.otherExpenses = new HashSet<OtherExpensesBll>();
            this.salaryExpenses = new HashSet<SalaryExpensesBll>();
            this.utilityCosts = new HashSet<UtilityCostsBll>();
            this.costCategories = new HashSet<CostCategoriesBll>();
        }

        public int idExpenseCategory { get; set; }
        public string name { get; set; }

        public virtual ICollection<ConsumablesTireFittingBll> consumablesTireFitting { get; set; }
        public virtual ICollection<CostsCarWashAndDeteylingBll> costsCarWashAndDeteyling { get; set; }
        public virtual ICollection<OtherExpensesBll> otherExpenses { get; set; }
        public virtual ICollection<SalaryExpensesBll> salaryExpenses { get; set; }
        public virtual ICollection<UtilityCostsBll> utilityCosts { get; set; }
        public virtual ICollection<CostCategoriesBll> costCategories { get; set; }
    }
}
