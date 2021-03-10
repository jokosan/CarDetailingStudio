using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models.CompletedOrders
{
    // CarWashWorkersServices
    // ВЫполненные заказы

    public class CompletedOrdersModels
    {
        public InformationCompletedOrders Washing { get; set; } // Мойка
        public InformationCompletedOrders Detailing { get; set; } // Детейлинг
        public InformationCompletedOrders TireFitting { get; set; } // Шиномонтаж
        public InformationCompletedOrders CarpetWashing { get; set; } // Стирка ковров
        public InformationCompletedOrders TireStorage { get; set; } // Хранения шин
    }
}
