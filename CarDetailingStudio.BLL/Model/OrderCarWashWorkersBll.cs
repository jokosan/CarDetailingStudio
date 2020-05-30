using System;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderCarWashWorkersBll
    {

        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdCarWashWorkers { get; set; }
        public Nullable<bool> CalculationStatus { get; set; }
        public Nullable<double> Payroll { get; set; }
        public Nullable<System.DateTime> salaryDate { get; set; }
        public Nullable<bool> closedDayStatus { get; set; }

        public virtual CarWashWorkersBll CarWashWorkers { get; set; }
        public virtual OrderServicesCarWashBll OrderServicesCarWash { get; set; }
    }
}
