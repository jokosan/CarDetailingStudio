using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.EmployeesModules.Model
{
    public class ChangeOfDay
    {
        public List<CarWashWorkersBll> WashingAdministrator { get; set; }
        public List<CarWashWorkersBll> DetailingAdministrator { get; set; }
        public List<CarWashWorkersBll> ShiftStaff { get; set; }
    }
}
