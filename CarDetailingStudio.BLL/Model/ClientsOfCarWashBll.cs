using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class ClientsOfCarWashBll
    {
        public ClientsOfCarWashBll()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWashBll>();
        }

        public int id { get; set; }
        public Nullable<int> IdClientsGroups { get; set; }
        public Nullable<int> IdMark { get; set; }
        public Nullable<int> IdModel { get; set; }
        public Nullable<int> IdBody { get; set; }
        public Nullable<int> IdInfoClient { get; set; }
        public string NumberCar { get; set; }
        public Nullable<int> discont { get; set; }
        public Nullable<bool> arxiv { get; set; }

        public virtual CarMarkBll car_mark { get; set; }
        public virtual CarModelBll car_model { get; set; }
        public virtual CarBodyBll CarBody { get; set; }
        public virtual ClientInfoBll ClientInfo { get; set; }
        public virtual ClientsGroupsBll ClientsGroups { get; set; }
        public virtual ICollection<OrderServicesCarWashBll> OrderServicesCarWash { get; set; }
    }
}
