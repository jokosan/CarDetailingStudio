using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ExpensesBll
    {
        public ExpensesBll()
        {
            this.salaryExpenses = new HashSet<SalaryExpensesBll>();
            this.utilityCosts = new HashSet<UtilityCostsBll>();
            this.procurement = new HashSet<ProcurementBll>();
        }

        public int idExpenses { get; set; }
        public int expenseCategoryId { get; set; }
        public Nullable<System.DateTime> dateExpenses { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<int> paymentType { get; set; }
        public string note { get; set; }
        public Nullable<int> idCostCategories { get; set; }


        public virtual ExpenseCategoryBll expenseCategory { get; set; }
        public virtual ICollection<SalaryExpensesBll> salaryExpenses { get; set; }
        public virtual ICollection<UtilityCostsBll> utilityCosts { get; set; }
        public virtual CostCategoriesBll costCategories { get; set; }
        public virtual ICollection<ProcurementBll> procurement { get; set; }
    }
}
