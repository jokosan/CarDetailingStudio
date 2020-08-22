using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderCarWashWorkersDayGroupBll
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdCarWashWorkers { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public string NumberCar { get; set; }
        public int CountOrder { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> ClosingData { get; set; }

        public Nullable<double> Payroll { get; set; }
        public Nullable<double> TotalCostOfAllServices { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
    }
}
