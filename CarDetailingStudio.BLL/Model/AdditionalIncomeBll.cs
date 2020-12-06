using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class AdditionalIncomeBll
    {
        public int idAdditionalIncome { get; set; }
        public string IncomeCategory { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Note { get; set; }
    }
}
