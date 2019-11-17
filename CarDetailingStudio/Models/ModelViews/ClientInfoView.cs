using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ClientInfoView
    {
       
        public ClientInfoView()
        {
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PatronymicName { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public string barcode { get; set; }
        public string note { get; set; }

        public virtual ICollection<ClientsOfCarWashView> ClientsOfCarWash { get; set; }
    }
}