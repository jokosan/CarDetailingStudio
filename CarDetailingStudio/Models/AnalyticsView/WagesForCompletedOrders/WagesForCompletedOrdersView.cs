using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView.WagesForCompletedOrders
{
    public class WagesForCompletedOrdersView
    {
        public SalaryInformationView Washing { get; set; } // Мойка
        public SalaryInformationView Detailing { get; set; } // Детейлинг
        public SalaryInformationView TireFitting { get; set; } // Шиномонтаж
        public SalaryInformationView CarpetWashing { get; set; } // Стирка ковров
        public SalaryInformationView TireStorage { get; set; } // Хранения шин
        public double SumBonusTOSalary { get; set; } // Премии
        public double SumEmployeeRate { get; set; } // Ставка задень 
    }
}