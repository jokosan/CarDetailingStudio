using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDetailingStudio.Models.ModelViews
{
    public class PaymentStateView
    {
        public PaymentStateView()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashView>();
        }

        public int Id { get; set; }

        [Display(Name = "Способы оплаты")]
        public string PaymentState1 { get; set; }

        public virtual ICollection<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
    }
}