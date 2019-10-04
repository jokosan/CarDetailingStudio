using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OrderServicesCarWashView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
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

        public virtual BrigadeForTodayView brigadeForToday { get; set; }
        public virtual ClientsOfCarWashView ClientsOfCarWash { get; set; }
        public virtual ServisesCarWashOrderView ServisesCarWashOrder { get; set; }
    }
}