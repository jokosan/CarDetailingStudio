using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models.WagesForCompletedOrders
{
    public class WagesForCompletedOrdersModels
    {
        public SalaryInformation Washing { get; set; } // Мойка
        public SalaryInformation Detailing { get; set; } // Детейлинг
        public SalaryInformation TireFitting { get; set; } // Шиномонтаж
        public SalaryInformation CarpetWashing { get; set; } // Стирка ковров
        public SalaryInformation TireStorage { get; set; } // Хранения шин

        public double SumBonusTOSalary { get; set; } // Премии
        public double SumEmployeeRate { get; set; } // Ставка задень 
      }
}
