using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class AllExpensesBll
    {
        public int id { get; set; }
        public Nullable<int> indicationCounter { get; set; }
        public Nullable<int> utilityCostsCategoryId { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> dateExpenses { get; set; }
        public string nameExpenses { get; set; }
        public Nullable<int> expenseCategoryId { get; set; }
        public Nullable<int> typeServicesId { get; set; }
        public Nullable<int> paymentType { get; set; }
    }
}
