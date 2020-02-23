using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model.ModelViewBll
{
    public class DayResultModelBll
    {
        public int carWashWorkersId { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public int orderCount { get; set; }
        public Nullable<bool> calculationStatus { get; set; }
        public Nullable<double> payroll { get; set; }

    }
}
