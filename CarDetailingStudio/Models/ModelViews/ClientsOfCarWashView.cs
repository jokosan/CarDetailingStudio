using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public int ib { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PatronymicName { get; set; }
        public string phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public string discont { get; set; }
        public string Recommendation { get; set; }
        public string NumderCar { get; set; }
        public Nullable<int> IdClientsGroups { get; set; }
        public Nullable<int> Idmark { get; set; }
        public Nullable<int> Idmodel { get; set; }
        public Nullable<int> IdBody { get; set; }
        public string note { get; set; }
        public string barcode { get; set; }

        public virtual CarMarkView car_mark { get; set; }
        public virtual CarModelView car_model { get; set; }

        [Display(Prompt = "CarBody")]
        public virtual CarBodyView CarBody { get; set; }
        public virtual ClientsGroupsView ClientsGroups { get; set; }
        public virtual ICollection<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
    }
}