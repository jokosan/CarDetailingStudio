using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class InfoBrigadeForTodayView
    {
        public int id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Nullable<int> sumOrder { get; set; }
        public int Expr1 { get; set; }
    }
}