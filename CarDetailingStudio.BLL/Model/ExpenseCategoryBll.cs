using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public int idExpenseCategory { get; set; }
        public string name { get; set; }

        public virtual ICollection<ConsumablesTireFittingBll> consumablesTireFitting { get; set; }
        public virtual ICollection<CostsCarWashAndDeteylingBll> costsCarWashAndDeteyling { get; set; }
        public virtual ICollection<OtherExpensesBll> otherExpenses { get; set; }
        public virtual ICollection<SalaryExpensesBll> salaryExpenses { get; set; }
        public virtual ICollection<UtilityCostsBll> utilityCosts { get; set; }
    }
}
