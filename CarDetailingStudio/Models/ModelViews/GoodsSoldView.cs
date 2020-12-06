using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class GoodsSoldView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idGoodsSold { get; set; }
        public Nullable<int> listOfGoodsId { get; set; }
        public Nullable<int> carWashWorkersId { get; set; }
        [Display (Name = "Дата Заказа")]
        public Nullable<System.DateTime> Date { get; set; }
        [Display(Name = "Стоимость заказа")]
        public Nullable<double> orderPrice { get; set; }
        [Display(Name = "Количество")]
        public Nullable<double> amount { get; set; }
        [Display(Name = "Процент от продажи")]
        public Nullable<double> percentageOfSale { get; set; }
        [Display (Name = "Цена за единицу товара")]
        public Nullable<double> priceForOne { get; set; }
        [Display(Name = "Вид оплаты")]
        public Nullable<int> PaymentState { get; set; }

        public virtual ListOfGoodsView listOfGoods { get; set; }
    }
}