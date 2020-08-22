using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class OrderCarWashWorkersDayGroupView
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdCarWashWorkers { get; set; }

        [Display(Name="Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Display(Name = "Заказы")]
        public int CountOrder { get; set; }
        [Display (Name = "Номер авто клиента")]
        public string NumberCar { get; set; }
        [Display(Name = "Дата заказа")]
        public Nullable<System.DateTime> OrderDate { get; set; }
        [Display(Name = "Дата закрытия заказа")]
        public Nullable<System.DateTime> ClosingData { get; set; }

        [Display(Name = "ЗП")]
        public Nullable<double> Payroll { get; set; }
        [Display(Name = "стоимость заказа")]
        public Nullable<double> TotalCostOfAllServices { get; set; }
        [Display(Name = "стоимость с учетом скидки")]
        public Nullable<double> DiscountPrice { get; set; }
    }
}