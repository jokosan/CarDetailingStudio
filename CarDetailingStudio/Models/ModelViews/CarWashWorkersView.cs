using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string MobilePhone { get; set; }
        public string Experience { get; set; }
        public Nullable<int> InterestRate { get; set; }
        public string DataRegistration { get; set; }
        public string DataDismissal { get; set; }
        public string status { get; set; }
        public string Photo { get; set; }
        public Nullable<int> IdPosition { get; set; }

        public virtual ICollection<BrigadeForTodayView> brigadeForToday { get; set; }
        public virtual JobTitleTableView JobTitleTable { get; set; }
        public virtual ICollection<OrderCarWashWorkersView> OrderCarWashWorkers { get; set; }
        public virtual ICollection<WageView> Wage { get; set; }
    }
}