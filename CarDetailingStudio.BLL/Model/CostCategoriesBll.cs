using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CostCategoriesBll
    {
        public int idCostCategories { get; set; }
        public Nullable<int> typeOfExpenses { get; set; }
        public string Name { get; set; }

        public virtual ExpenseCategoryBll expenseCategory { get; set; }
    }
}
