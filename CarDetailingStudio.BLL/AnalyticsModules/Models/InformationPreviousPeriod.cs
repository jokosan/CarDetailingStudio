using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    public class InformationPreviousPeriod
    {
        public OrdersForThePreviousPeriod Washing { get; set; } // Мойка
        public OrdersForThePreviousPeriod Detailing { get; set; } // Детейлинг
        public OrdersForThePreviousPeriod TireFitting { get; set; } // Шиномонтаж
        public OrdersForThePreviousPeriod CarpetWashing { get; set; } // Стирка ковров
        public OrdersForThePreviousPeriod TireStorage { get; set; } // Хранения шин
    }
}
