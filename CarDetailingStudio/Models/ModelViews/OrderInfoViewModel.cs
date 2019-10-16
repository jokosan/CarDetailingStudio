using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OrderInfoViewModel
    {
        public string NumderCar { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public string CarBody { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PatronymicName { get; set; }
        public string phone { get; set; }
        public string StatusOrder { get; set; }
        public Nullable<double> TotalCostOfAllServices { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ib { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public int Id { get; set; }
    }
}