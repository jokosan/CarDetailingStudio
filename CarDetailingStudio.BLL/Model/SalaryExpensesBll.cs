using System;

namespace CarDetailingStudio.BLL.Model
{
    public class SalaryExpensesBll 
    {
        public int idSalaryExpenses { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public Nullable<int> expenseId { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
        public virtual ExpensesBll Expenses { get; set; }
    }
}
