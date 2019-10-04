using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class ExchangeRatesModel
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public float buy { get; set; }
        public float sale { get; set; }
    }
}
