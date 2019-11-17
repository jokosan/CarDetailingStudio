using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CarWashWorkersView
    {
        public CarWashWorkersView()
        {
            this.brigadeForToday = new HashSet<BrigadeForTodayView>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersView>();
            this.Wage = new HashSet<WageView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string MobilePhone { get; set; }

        public string Experience { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Допустимое значение от 0 до 100")]
        public Nullable<int> InterestRate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> rate { get; set; }

        [Required]
        public string DataRegistration { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DataDismissal { get; set; }

        [Required]
        [DefaultValue("true")]
        public string status { get; set; }
        public string Photo { get; set; }

        [Required]
        public Nullable<int> IdPosition { get; set; }    
       // public IEnumerable<SelectListItem> Position { get; set; }
      

        public virtual ICollection<BrigadeForTodayView> brigadeForToday { get; set; }
        public virtual JobTitleTableView JobTitleTable { get; set; }
        public virtual ICollection<OrderCarWashWorkersView> OrderCarWashWorkers { get; set; }
        public virtual ICollection<WageView> Wage { get; set; }
    }
}