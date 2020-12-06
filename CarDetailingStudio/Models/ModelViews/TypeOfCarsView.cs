using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class TypeOfCarsView
    {
        public TypeOfCarsView()
        {
            this.PriceListTireFitting = new HashSet<PriceListTireFittingView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public double idTypeOfCars { get; set; }

        [Display(Name = "Тип авто")]
        public string Type { get; set; }

        public virtual ICollection<PriceListTireFittingView> PriceListTireFitting { get; set; }
    }
}