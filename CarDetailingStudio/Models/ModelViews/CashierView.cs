using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CashierView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idCashier { get; set; }
        public System.DateTime date { get; set; }
        [Display(Name ="Касса наличных")]
        [Range(1, 10000, ErrorMessage = "Недоступное значение")]
        public double amountBeginningOfTheDay { get; set; }
        public double amountEndOfTheDay { get; set; }
    }
}