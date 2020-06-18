using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.JoinModel.Model
{
    public class ClientJoinCarpetWashingModel
    {
        public int idOrderCarpetWashing { get; set; }
        public int orderServicesCarWashId { get; set; }
        public Nullable<int> clientId { get; set; }
        public Nullable<double> area { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<System.DateTime> orderClosingDate { get; set; }
        public Nullable<System.DateTime> orderCompletionDate { get; set; }


        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PatronymicName { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public string barcode { get; set; }
        public string note { get; set; }

    }
}
