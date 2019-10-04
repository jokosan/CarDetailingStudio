using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CarBodyView
    {
        public CarBodyView()
        {
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashView>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Display(Prompt = "CarBody")]
        public string Name { get; set; }
        public virtual ICollection<ClientsOfCarWashView> ClientsOfCarWash { get; set; }
    }

}