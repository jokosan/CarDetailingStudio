using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OrderServicesCarWashView
    {

        public OrderServicesCarWashView()
        {
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrderView>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersView>();
            this.TireStorage = new HashSet<TireStorageView>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> StatusOrder { get; set; }
        public Nullable<int> PaymentState { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        [Display(Name ="Дата Выполнения заказа")]
        public Nullable<System.DateTime> ClosingData { get; set; }
        public Nullable<double> TotalCostOfAllServices { get; set; }
        [Display(Name ="Стоимость заказа")]
        public Nullable<double> DiscountPrice { get; set; }
        public Nullable<int> typeOfOrder { get; set; }

        public virtual ClientsOfCarWashView ClientsOfCarWash { get; set; }
        public virtual PaymentStateView PaymentState1 { get; set; }
        public virtual StatusOrderView StatusOrder1 { get; set; }
        public virtual ICollection<ServisesCarWashOrderView> ServisesCarWashOrder { get; set; }
        public virtual ICollection<OrderCarWashWorkersView> OrderCarWashWorkers { get; set; }
        public virtual ICollection<TireStorageView> TireStorage { get; set; }
    }
}