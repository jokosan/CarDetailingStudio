using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CashierBll
    {
        public int idCashier { get; set; }
        public System.DateTime date { get; set; }
        public double amountBeginningOfTheDay { get; set; }
        public double amountEndOfTheDay { get; set; }
    }
}
