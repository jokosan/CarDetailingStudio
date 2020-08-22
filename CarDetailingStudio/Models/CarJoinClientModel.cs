using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class CarJoinClientModel
    {

        public int idCar { get; set; }
        public int idClien { get; set; }
        public string IdClientsGroups { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PatronymicName { get; set; }
        public string phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public Nullable<int> discont { get; set; }
        public string carMarkName { get; set; }
        public string carModelName { get; set; }
        public string carBodyName { get; set; }
        public string NumberCar { get; set; }
    }
}