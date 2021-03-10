using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView.CompletedOrders
{
    public class CompletedOrdersView
    {
        public InformationCompletedOrdersView Washing { get; set; } // Мойка
        public InformationCompletedOrdersView Detailing { get; set; } // Детейлинг
        public InformationCompletedOrdersView TireFitting { get; set; } // Шиномонтаж
        public InformationCompletedOrdersView CarpetWashing { get; set; } // Стирка ковров
        public InformationCompletedOrdersView TireStorage { get; set; } // Хранения шин
    }
}