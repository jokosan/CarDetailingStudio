using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class ChangeOfDayModel
    {
        public List<CarWashWorkersView> WashingAdministrator { get; set; }
        public List<CarWashWorkersView> DetailingAdministrator { get; set; }
        public List<CarWashWorkersView> ShiftStaff { get; set; }
    }
}