using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class AllExpenses
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        [Display(Name = "Показание счетчика")]
        //[Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<int> indicationCounter { get; set; } // поле только для таблицы UtilityCostsView
        [Display(Name = "Категория коммунальных услуг")]
        public Nullable<int> utilityCostsCategoryId { get; set; }  // поле только для таблицы UtilityCostsView

        [Display(Name = "Сумма")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<double> amount { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<System.DateTime> dateExpenses { get; set; }

        [Display(Name = "Наименование расходов")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public string nameExpenses { get; set; }

        [Display(Name = "Категория расходов")]
        public Nullable<int> expenseCategoryId { get; set; }

        [Display(Name = "Группа расходов")]
        public Nullable<int> typeServicesId { get; set; } // поле только для таблицы CostsCarWashAndDeteylingView
        
        [Display(Name = "Вид оплаты")]
        public Nullable<int> paymentType { get; set; }
    }
}