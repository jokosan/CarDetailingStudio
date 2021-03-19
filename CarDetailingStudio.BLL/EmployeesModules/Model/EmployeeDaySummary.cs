using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.EmployeesModules.Model
{
    class EmployeeDaySummary
    {
        public int idServises { get; set; }
        public int idEmployee { get; set; }
        public string NameServises { get; set; }
        public double Sum { get; set; }
        public bool status { get; set; }
        public double multiplicityOfTheSum { get; set; }
        public double prizeAmount { get; set; }
        public double InterestRate { get; set; }
    }
}
