using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class CloseOrderModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public OrderServicesCarWashView Order { get; set; }
        public ServisesCarWashOrderView Servises { get; set; }
        public ClientsGroupsView Clients { get; set; }
        public BrigadeForTodayView Brigade { get; set; }
    }
}