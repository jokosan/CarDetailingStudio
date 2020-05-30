using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ClientsOfCarWashView
    {
        public ClientsOfCarWashView()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        [Display(Name = "Статус клиента")]
        public Nullable<int> IdClientsGroups { get; set; }

        [Display(Name = "Марка машины")]
        public Nullable<int> IdMark { get; set; }

        [Display(Name = "Модель машины")]
        public Nullable<int> IdModel { get; set; }

        [Required]
        [Display(Name = "Тип кузова")]
        public Nullable<int> IdBody { get; set; }

        public Nullable<int> IdInfoClient { get; set; }

        [Required]
        [Display(Name = "Номер машины")]
        public string NumberCar { get; set; }

        [Display(Name = "Дисконт")]
        public Nullable<int> discont { get; set; }

        public Nullable<bool> arxiv { get; set; }

        public virtual CarMarkView car_mark { get; set; }
        public virtual CarModelView car_model { get; set; }
        public virtual CarBodyView CarBody { get; set; }
        public virtual ClientInfoView ClientInfo { get; set; }
        public virtual ClientsGroupsView ClientsGroups { get; set; }
        public virtual ICollection<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
    }
}