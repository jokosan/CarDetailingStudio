﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDetailingStudio.Models.ModelViews
{
    public class StatusOrderView
    {
        public StatusOrderView()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashView>();
        }

        public int Id { get; set; }
        [Display(Name = "Статус заказа")]
        public string StatusOrder1 { get; set; }

        public virtual ICollection<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
    }
}