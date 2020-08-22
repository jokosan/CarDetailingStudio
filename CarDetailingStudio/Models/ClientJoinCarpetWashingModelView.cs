using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class ClientJoinCarpetWashingModelView
    {
        public int idOrderCarpetWashing { get; set; }
        public int orderServicesCarWashId { get; set; }
        public Nullable<int> clientId { get; set; }

        [Display(Name="m2")]
        public Nullable<double> area { get; set; }
        
        [Display(Name = "стоимость заказа")]
        public Nullable<double> DiscountPrice { get; set; }

        [Display(Name="Дата заказа")]
        public Nullable<System.DateTime> orderDate { get; set; }

        [Display(Name="Дата закрытия заказа")]
        public Nullable<System.DateTime> orderClosingDate { get; set; }

        [Display(Name="Дата выполнения заказа")]
        public Nullable<System.DateTime> orderCompletionDate { get; set; }


        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string PatronymicName { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public string barcode { get; set; }
        public string note { get; set; }

        public string statusOrder { get; set; }

    }
}