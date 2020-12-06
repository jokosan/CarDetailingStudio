using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models
{
    public class ClientView
    {
        public ClientView()
        {
            this.brigadeForToday = new HashSet<BrigadeForTodayView>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string PatronymicName { get; set; }

        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 7, ErrorMessage = "Количество символов должно быть равно 7")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string phone { get; set; }

        [Display(Name = "Дата регистрации")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateRegistration { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "% Скидки")]
        public Nullable<int> discont { get; set; }
        public Nullable<bool> arxiv { get; set; }
        public string Recommendation { get; set; }

        [Display(Name = "Номер машины")]
        [Required]
        public string NumberCar { get; set; }

        [Required]
        [Display(Name = "Статус клиента")]
        public Nullable<int> IdClientsGroups { get; set; }

        [Display(Name = "Марка")]
        public Nullable<int> Idmark { get; set; }

        [Display(Name = "Модель")]
        public Nullable<int> Idmodel { get; set; }

        [Display(Name = "Тип кузова")]
        [Required]
        public Nullable<int> IdBody { get; set; }

        [Display(Name = "Примечание")]
        public string note { get; set; }
        public string barcode { get; set; }

        public virtual CarMarkView car_mark { get; set; }
        public virtual CarModelView car_model { get; set; }
        public virtual CarBodyView CarBody { get; set; }

        public virtual ICollection<BrigadeForTodayView> brigadeForToday { get; set; }
        public virtual JobTitleTableView JobTitleTable { get; set; }
        public virtual ICollection<OrderCarWashWorkersView> OrderCarWashWorkers { get; set; }

    }
}