using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderInformationWashingDetailingBll
    {
        public double? OrderCarWashSum { get; set; }    // Касса мойки
        public double? OrderDetailing { get; set; }     // Касса детейлинг
        public double? carpetOrder { get; set; }        // Касса ковров
        public int? OrderCount { get; set; }            // Количество Авто мойка
        public int? CarCountDetailings { get; set; }    // Количество авто детейлинг
        public int? CauntCarpet { get; set; }           // Количество заказов ковров
        public double? cash { get; set; }               // Наличный расчет
        public double? nonCash { get; set; }            // Безналичный расчет
    }   
}
