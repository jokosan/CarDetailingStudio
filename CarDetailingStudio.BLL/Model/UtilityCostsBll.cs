using System;

namespace CarDetailingStudio.BLL.Model
{
    public class UtilityCostsBll 
    {
        public int idUtilityCosts { get; set; }
        public Nullable<int> indicationCounter { get; set; }
        public Nullable<int> utilityCostsCategoryId { get; set; }
        public Nullable<int> expenseId { get; set; }

        public virtual ExpensesBll Expenses { get; set; }
        public virtual UtilityCostsCategoryBll utilityCostsCategory { get; set; }
    }
}
