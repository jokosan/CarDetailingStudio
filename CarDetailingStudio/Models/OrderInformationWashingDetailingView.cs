using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class OrderInformationWashingDetailingView
    {
        public double? OrderCarWashSum { get; set; }                    // Касса мойки
        public double? OrderDetailing { get; set; }                     // Касса детейлинг
        public double? carpetOrder { get; set; }                        // Касса ковров
        public double? TireOrders { get; set; }                         // Заказ шиномантаж
        public int? OrderCount { get; set; }                            // Количество Авто мойка
        public int? CarCountDetailings { get; set; }                    // Количество авто детейлинг
        public int? CauntCarpet { get; set; }                           // Количество заказов ковров
        public int? CauntTire { get; set; }                             // Количество заказов шиномантаж
        public double? cash { get; set; }                               // Наличный расчет
        public double? nonCash { get; set; }                            // Безналичный расчет
        public double? numberOfUnpaidOrdersCarWash { get; set; }        // Количество заказов ожидающих оплату (мойка)
        public double? numberOfUnpaidOrdersDetailings { get; set; }     // Количество заказов ожидающих оплату (Детейлинг)
        public double? numberOfUnpaidOrdersCarpetWashing { get; set; }  // Количество заказов ожидающих оплату (Ковры)
        public double? numberOfUnpaidTire { get; set; }                 // Количество заказов ожидающих оплату (Шиномонтаж)

        public double? ordersInProgressCarWash { get; set; }            // заказы мойки ожидают оплаты
        public double? ordersInProgressDetailings { get; set; }         // заказы детейлиинг ожидает оплаты
        public double? ordersInProgressCarpetWashing { get; set; }       // заказы чистка ковров ожидает оплаты

        public double? carWashCashSum { get; set; }                     //автомойка наличные
        public double? detailingCashSum { get; set; }                   // детейлинг наличные
        public double? cashCarpets { get; set; }                        // ковры наличные
        public double? tireCeash { get; set; }                            // шиномонта наличные

    }
}