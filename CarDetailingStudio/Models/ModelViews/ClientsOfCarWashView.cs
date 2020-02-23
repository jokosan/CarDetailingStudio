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
        public int id { get; set; }
        public Nullable<int> IdClientsGroups { get; set; }
        public Nullable<int> IdMark { get; set; }
        public Nullable<int> IdModel { get; set; }
        
        [Required]
        public Nullable<int> IdBody { get; set; }
        public Nullable<int> IdInfoClient { get; set; }
        
        [Required]
        public string NumberCar { get; set; }
        public Nullable<int> discont { get; set; }

        public virtual CarMarkView car_mark { get; set; }
        public virtual CarModelView car_model { get; set; }
        public virtual CarBodyView CarBody { get; set; }
        public virtual ClientInfoView ClientInfo { get; set; }
        public virtual ClientsGroupsView ClientsGroups { get; set; }
        public virtual ICollection<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
    }
}