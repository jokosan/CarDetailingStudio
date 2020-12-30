using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CarWashWorkersView
    {
        public CarWashWorkersView()
        {
            this.bonusToSalary = new HashSet<BonusToSalaryView>();
            this.brigadeForToday = new HashSet<BrigadeForTodayView>();
            this.Credit = new HashSet<CreditView>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersView>();
            this.salaryBalance = new HashSet<SalaryBalanceView>();
            this.salaryExpenses = new HashSet<SalaryExpensesView>();
            this.salaryArchive = new HashSet<SalaryArchiveView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string MobilePhone { get; set; }

        [Display(Name = "Стаж")]
        public string Experience { get; set; }

        [Range(0, 100, ErrorMessage = "Допустимое значение от 0 до 100")]
        [Display(Name = "Процентная ставка от заказа (Администратор)")]
        public Nullable<int> AdministratorsInterestRate { get; set; }

        [Range(0, 100, ErrorMessage = "Допустимое значение от 0 до 100")]
        [Display(Name = "Процентная ставка сотрудников")]
        public Nullable<int> InterestRate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Недопустимое значение")]
        [Display(Name = "Ставка")]
        public Nullable<double> rate { get; set; }

        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        [Display(Name = "Дата трудоустройства")]
        public string DataRegistration { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата увольнения")]
        public string DataDismissal { get; set; }

        //[Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        [DefaultValue("true")]
        [Display(Name = "Статус работы")]
        public string status { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "Данное поле являеться обезательным для заполнения")]
        [Display(Name = "Должность")]
        public Nullable<int> IdPosition { get; set; }

        // public IEnumerable<SelectListItem> Position { get; set; }

        public virtual ICollection<BonusToSalaryView> bonusToSalary { get; set; }
        public virtual ICollection<BrigadeForTodayView> brigadeForToday { get; set; }
        public virtual ICollection<CreditView> Credit { get; set; }
        public virtual ICollection<OrderCarWashWorkersView> OrderCarWashWorkers { get; set; }
        public virtual ICollection<SalaryBalanceView> salaryBalance { get; set; }
        public virtual ICollection<SalaryExpensesView> salaryExpenses { get; set; }
        public virtual JobTitleTableView JobTitleTable { get; set; }
        public virtual ICollection<SalaryArchiveView> salaryArchive { get; set; }
    }
}