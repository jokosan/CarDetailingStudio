using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ClientsGroupsView
    {
        public ClientsGroupsView()
        {
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ClientsOfCarWashView> ClientsOfCarWash { get; set; }
    }
}