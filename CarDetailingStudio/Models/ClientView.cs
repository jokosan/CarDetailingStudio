using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDetailingStudio.Models
{
    public class ClientView
    {
        public ClientView()
        {
            this.brigadeForToday = new HashSet<BrigadeForTodayView>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersView>();
            this.Wage = new HashSet<WageView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }       
        public string PatronymicName { get; set; }
        public string phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public Nullable<int> discont { get; set; }
        public string Recommendation { get; set; }
        public string NumberCar { get; set; }
        public Nullable<int> IdClientsGroups { get; set; }
        public Nullable<int> Idmark { get; set; }
        public Nullable<int> Idmodel { get; set; }
        public Nullable<int> IdBody { get; set; }
        public string note { get; set; }
        public string barcode { get; set; }

        public virtual CarMarkView car_mark { get; set; }
        public virtual CarModelView car_model { get; set; }
        public virtual CarBodyView CarBody { get; set; }

        public virtual ICollection<BrigadeForTodayView> brigadeForToday { get; set; }
        public virtual JobTitleTableView JobTitleTable { get; set; }
        public virtual ICollection<OrderCarWashWorkersView> OrderCarWashWorkers { get; set; }
        public virtual ICollection<WageView> Wage { get; set; }


    }
}