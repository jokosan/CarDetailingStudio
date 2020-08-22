using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class DetailingsView
    {
        public DetailingsView()
        {
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrderView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public int Id { get; set; }

        [Required]
        [Display(Name = "Название услуги")]
        public string services_list { get; set; }

        [Display(Name = "Гарантийный срок")]
        public string validity { get; set; }

        [Display(Name = "Описание услуги")]
        public string note { get; set; }

        [Required]
        [Display(Name = "Тип кузова S")]
        [Range(0, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> S { get; set; }

        [Required]
        [Display(Name = "Тип кузова M")]
        [Range(0, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> M { get; set; }

        [Required]
        [Display(Name = "Тип кузова L")]
        [Range(0, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> L { get; set; }

        [Required]
        [Display(Name = "Тип кузова XL")]
        [Range(0, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> XL { get; set; }

        public string group { get; set; }

        [Required]
        public string status { get; set; }

        [Display(Name = "Тип валюты")]
        [Required]
        public string currency { get; set; }

        [Display(Name = "Статус услуги")]
        public Nullable<bool> mark { get; set; }

        [Required]
        [Display(Name = "Группа услуги")]
        public Nullable<int> IdGroupWashServices { get; set; }

        [Required]
        [Display(Name = "Категория услуги")]
        public Nullable<int> IdTypeService { get; set; }
        public Nullable<bool> forUnit { get; set; }

        public virtual GroupWashServicesView GroupWashServices { get; set; }
        public virtual ICollection<ServisesCarWashOrderView> ServisesCarWashOrder { get; set; }
    }
}