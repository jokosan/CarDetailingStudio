using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class OrderInfoViewBll
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
        public int ib { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public int Id { get; set; }
    }
}
