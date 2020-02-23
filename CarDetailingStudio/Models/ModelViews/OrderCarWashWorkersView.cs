﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OrderCarWashWorkersView
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdCarWashWorkers { get; set; }
        public Nullable<bool> CalculationStatus { get; set; }
        [Display(Name = "Всего на зарплату")]
        public Nullable<double> Payroll { get; set; }
        public Nullable<System.DateTime> salaryDate { get; set; }
        public Nullable<bool> closedDayStatus { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
    }
}