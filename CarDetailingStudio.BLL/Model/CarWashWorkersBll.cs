using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class CarWashWorkersBll
    {
        public CarWashWorkersBll()
        {
            this.brigadeForToday = new HashSet<BrigadeForTodayBll>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersBll>();
            this.Wage = new HashSet<WageBll>();
        }

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

        public virtual ICollection<BrigadeForTodayBll> brigadeForToday { get; set; }
        public virtual JobTitleTableBll JobTitleTable { get; set; }
        public virtual ICollection<OrderCarWashWorkersBll> OrderCarWashWorkers { get; set; }
        public virtual ICollection<WageBll> Wage { get; set; }
    }
}
