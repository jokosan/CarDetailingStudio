using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderInfoViewBll
    {
        public int id { get; set; }
        public Nullable<int> countOrder { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string MobilePhone { get; set; }
        public Nullable<int> InterestRate { get; set; }
        public Nullable<bool> CalculationStatus { get; set; }
        public Nullable<double> Expr1 { get; set; }
    }
}
