using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView.CompletedOrders
{
    public class InformationCompletedOrdersView
    {
        public double Cashier { get; set; } // Касса
        public double Quantities { get; set; } // Количества

        public int OrdersAwaitingPaymentQuantites { get; set; } // Заказы ожидающие оплату количество заказов
        public double OrdersAwaitingPaymentCashier { get; set; } // Заказы ожидающие оплату сумма 

        public int SumQuantitiesAndOrdersAwaitingPaymentQuantites { get; set; }
        public double SumCashierAndOrdersAwaitingPaymentCashier { get; set; }
    }
}