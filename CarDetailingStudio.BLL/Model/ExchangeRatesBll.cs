using System;

namespace CarDetailingStudio.BLL.Model
{
    public class ExchangeRatesBll
    {
        public int Id { get; set; }
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public Nullable<double> buy { get; set; }
        public Nullable<double> sale { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
}
