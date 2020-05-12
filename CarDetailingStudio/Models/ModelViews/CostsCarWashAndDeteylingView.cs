using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CostsCarWashAndDeteylingView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idCostsCarWashAndDeteyling { get; set; }

        [Display(Name = "Название товара")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public string nameExpenses { get; set; }

        [Display(Name = "Сумма")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "В качестве разделителя дробной и целой части используется точка")]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<double> amount { get; set; }
       
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        public Nullable<System.DateTime> dateExpenses { get; set; }
      
        [Display(Name = "Категория расходов")]
        public Nullable<int> expenseCategoryId { get; set; }

        public virtual ExpenseCategoryView expenseCategory { get; set; }
    }
}