using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models
{
    public class DayResultModelView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int carWashWorkersId { get; set; }

        [Display(Name = "Фамилия")]
        public string surname { get; set; }

        [Display(Name = "Имя")]
        public string name { get; set; }

        [Display(Name = "Отчество")]
        public string patronymic { get; set; }

        [Display(Name = "Количество заказов")]
        public int orderCount { get; set; }

        public Nullable<bool> calculationStatus { get; set; }

        [Display(Name = "Всего на зарплату")]
        public Nullable<double> payroll { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата оформления заказа")]
        public Nullable<System.DateTime> salaryDate { get; set; }
    }
}