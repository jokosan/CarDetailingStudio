using System;

namespace CarDetailingStudio.BLL.Model.ModelViewBll
{
    public class WagesForDaysWorkedBll
    {
        public int idOrder { get; set; }
        public int carWashWorkersId { get; set; }
        public Nullable<DateTime> ClosingData { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public int orderCount { get; set; }
        public Nullable<bool> calculationStatus { get; set; }
        public Nullable<double> payroll { get; set; }
    }
}
