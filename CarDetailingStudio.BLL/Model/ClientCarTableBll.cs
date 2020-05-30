using System;

namespace CarDetailingStudio.BLL.Model
{
    public class ClientCarTableBll
    {
        public int id { get; set; }
        public Nullable<int> IdClientsGroups { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PatronymicName { get; set; }
        public string phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public Nullable<int> discont { get; set; }
        public Nullable<int> IdMark { get; set; }
        public Nullable<int> IdModel { get; set; }
        public Nullable<int> IdBody { get; set; }
        public Nullable<int> IdInfoClient { get; set; }
        public string NumberCar { get; set; }

        public virtual CarMarkBll car_mark { get; set; }
        public virtual CarModelBll car_model { get; set; }
        public virtual CarBodyBll CarBody { get; set; }
        public virtual ClientInfoBll ClientInfo { get; set; }
        public virtual ClientsGroupsBll ClientsGroups { get; set; }
    }
}
