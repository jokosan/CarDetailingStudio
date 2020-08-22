﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class BonusToSalaryGroupView
    {
        public int idBonusToSalary { get; set; }
        public Nullable<int> carWashWorkersId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string note { get; set; }
    }
}