using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OrderServicesCarWashView
    {

        public OrderServicesCarWashView()
        {
            this.orderCarpetWashing = new HashSet<OrderCarpetWashingView>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersView>();
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrderView>();
            this.tireChangeService = new HashSet<TireChangeServiceView>();
            this.tireService = new HashSet<TireServiceView>();
            this.TireStorage = new HashSet<TireStorageView>();
            this.additionalTireStorageServices = new HashSet<AdditionalTireStorageServicesView>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Номер заказа")]
        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }

        [Display(Name = "Статус заказа")]
        public Nullable<int> StatusOrder { get; set; }

        [Display(Name = "Состояние платежа")]
        public Nullable<int> PaymentState { get; set; }

        [Display(Name = "Дата/Время оформления заказа")]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [Display(Name = "Дата/Время выполнения заказа")]
        public Nullable<System.DateTime> ClosingData { get; set; }

        [Display(Name = "Стоимость заказа без скидки")]
        public Nullable<double> TotalCostOfAllServices { get; set; }

        [Display(Name = "Стоимость заказа со скидкой")]
        public Nullable<double> DiscountPrice { get; set; }

        public Nullable<int> typeOfOrder { get; set; }

        public virtual ClientsOfCarWashView ClientsOfCarWash { get; set; }
        public virtual ICollection<OrderCarpetWashingView> orderCarpetWashing { get; set; }
        public virtual PaymentStateView PaymentState1 { get; set; }
        public virtual StatusOrderView StatusOrder1 { get; set; }
        public virtual ICollection<ServisesCarWashOrderView> ServisesCarWashOrder { get; set; }
        public virtual ICollection<OrderCarWashWorkersView> OrderCarWashWorkers { get; set; }
        public virtual ICollection<TireStorageView> TireStorage { get; set; }
        public virtual ICollection<TireServiceView> tireService { get; set; }
        public virtual ICollection<TireChangeServiceView> tireChangeService { get; set; }
        public virtual ICollection<AdditionalTireStorageServicesView> additionalTireStorageServices { get; set; }
    }
}