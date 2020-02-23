﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class WagesForDaysWorkedView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int carWashWorkersId { get; set; }
        [Display(Name = "Дата Выполнения заказа")]
        public string ClosingData { get; set; }
        [Display(Name = "Стоимость заказа")]
        public Nullable<double> DiscountPrice { get; set; }
        [Display(Name ="Количество заказов")]
        public int orderCount { get; set; }
        public Nullable<bool> calculationStatus { get; set; }
        [Display(Name = "Всего на зарплату")]
        public Nullable<double> payroll { get; set; }
    }
}