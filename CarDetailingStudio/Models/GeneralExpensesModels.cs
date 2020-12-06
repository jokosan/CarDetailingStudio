using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class GeneralExpensesModels
    {
        public int idExpenses { get; set; }
        public int idTypeExpenses { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> dateExpenses { get; set; }
        public string expenseCategoryId { get; set; }
    }
}