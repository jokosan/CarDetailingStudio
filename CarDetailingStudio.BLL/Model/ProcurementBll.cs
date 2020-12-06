using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ProcurementBll
    {
        public int idProcurement { get; set; }
        public Nullable<int> ListOfGoodsId { get; set; }
        public Nullable<int> expensesId { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<int> remainder { get; set; }

        public virtual ExpensesBll Expenses { get; set; }
        public virtual ListOfGoodsBll listOfGoods { get; set; }
    }
}
