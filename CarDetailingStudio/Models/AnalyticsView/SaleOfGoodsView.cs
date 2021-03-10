using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView
{
    public class SaleOfGoodsView
    {
        public int CauntGoodsSold { get; set; } // Количество проданных товаров
        public double SumGoodsSold { get; set; } // Сумма проданных товаров
        public double SumWagesAdminstrator { get; set; } // Процент ЗП администратора от суммы проданных товаров 
    }
}