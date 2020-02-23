using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class SalaryExpensesBll
    {
        public int idSalaryExpenses { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> dateExpenses { get; set; }
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
        public virtual ExpenseCategoryBll expenseCategory { get; set; }
    }
}
