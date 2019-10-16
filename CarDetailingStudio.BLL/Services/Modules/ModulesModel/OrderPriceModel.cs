using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.ModulesModel
{
    public class OrderPriceModel
    {
        public int id { get; set; }
        public double price { get; set; }
        public int priceSum { get; set; }
    }
}
