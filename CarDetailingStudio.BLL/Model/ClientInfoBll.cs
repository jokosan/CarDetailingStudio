using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class ClientInfoBll
    {
        public ClientInfoBll()
        {
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashBll>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PatronymicName { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public string barcode { get; set; }
        public string note { get; set; }

        public virtual ICollection<ClientsOfCarWashBll> ClientsOfCarWash { get; set; }
    }
}
