using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderServicesCarWashBll
    {
        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public int IdServisesCarWashOrder { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public string discount { get; set; }
        public string StatusOrder { get; set; }
        public string PaymentState { get; set; }
        public string OrderDate { get; set; }
        public string ExecutionTime { get; set; }
        public string ClosingData { get; set; }
        public string ClosingTime { get; set; }
        public Nullable<double> TotalCostOfAllServices { get; set; }

        public virtual BrigadeForTodayBll brigadeForToday { get; set; }
        public virtual ClientsOfCarWashBll ClientsOfCarWash { get; set; }
        public virtual ServisesCarWashOrderBll ServisesCarWashOrder { get; set; }
    }
}
