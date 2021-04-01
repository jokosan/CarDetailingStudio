using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.AnalyticsView
{
    public class GroupingEmployeesWagesView
    {
        public int idEmployees { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime date { get; set; }
        public string nameEmployees { get; set; }
        public int orderCount { get; set; }
        public double wegesSum { get; set; }
       
        public double orderSum { get; set; }
        public int idOrder { get; set; }
        public double bonus { get; set; }
    }
}