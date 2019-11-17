using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ClientViewsBll
    {
        //public ClientViewsBll()
        //{
        //    this.brigadeForToday = new HashSet<BrigadeForTodayBll>();
        //    this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersBll>();
        //    this.Wage = new HashSet<WageBll>();
        //}

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

        //public virtual CarMarkBll car_mark { get; set; }
        //public virtual CarModelBll car_model { get; set; }
        //public virtual CarBodyBll CarBody { get; set; }

        //public virtual ICollection<BrigadeForTodayBll> brigadeForToday { get; set; }
        //public virtual JobTitleTableBll JobTitleTable { get; set; }
        //public virtual ICollection<OrderCarWashWorkersBll> OrderCarWashWorkers { get; set; }
        //public virtual ICollection<WageBll> Wage { get; set; }


    }
}
