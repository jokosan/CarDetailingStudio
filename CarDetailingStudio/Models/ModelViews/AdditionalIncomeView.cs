using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class AdditionalIncomeView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idAdditionalIncome { get; set; }
        [Display(Name = "Категория доходов")]
        public string IncomeCategory { get; set; }
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        [Display(Name = "Сумма дохода")]
        public Nullable<double> Amount { get; set; }
        [Display(Name = "Заметка")]
        public string Note { get; set; }
        [Display(Name = "Тип оплаты")]
        public Nullable<int> PaymentState { get; set; }
    }
}