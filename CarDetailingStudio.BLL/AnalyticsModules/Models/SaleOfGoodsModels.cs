using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.Models
{
    // Продажа товаров
    public class SaleOfGoodsModels
    {
        public double CauntGoodsSold { get; set; } // Количество проданных товаров
        public double SumGoodsSold { get; set; } // Касса проданных товаров
        public double SumWagesAdminstrator { get; set; } // ЗП администратора

    }
}
