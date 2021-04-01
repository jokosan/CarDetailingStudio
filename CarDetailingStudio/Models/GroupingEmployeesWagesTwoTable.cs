using CarDetailingStudio.Models.AnalyticsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class GroupingEmployeesWagesTwoTable
    {
        public IEnumerable<GroupingEmployeesWagesView> groupingEmployeesByPeriod { get; set; }
        public IEnumerable<GroupingEmployeesWagesView> groupDetailsWages { get; set; }
    }
}