using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class EmployeeRateBll
    {
        public int idEmployeeRate { get; set; }
        public Nullable<int> brigadeForTodayId { get; set; }
        public Nullable<int> numberHoursWorked { get; set; }
        public Nullable<double> hourlyRate { get; set; }
        public Nullable<double> wage { get; set; }

        public virtual BrigadeForTodayBll brigadeForToday { get; set; }
    }
}
