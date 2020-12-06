using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class GoodsSoldBll
    {
        public int idGoodsSold { get; set; }
        public Nullable<int> listOfGoodsId { get; set; }
        public Nullable<int> carWashWorkersId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> orderPrice { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<double> percentageOfSale { get; set; }
        public Nullable<double> priceForOne { get; set; }
        public Nullable<int> PaymentState { get; set; }

        public virtual ListOfGoodsBll listOfGoods { get; set; }
    }
}
