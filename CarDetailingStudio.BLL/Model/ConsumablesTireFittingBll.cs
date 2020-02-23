using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ConsumablesTireFittingBll
    {
        public int idConsumablesTireFitting { get; set; }
        public string nameExpenses { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> dateExpenses { get; set; }
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual ExpenseCategoryBll expenseCategory { get; set; }
    }
}
