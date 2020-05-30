using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class TypeServicesView
    {
        public TypeServicesView()
        {
            this.costsCarWashAndDeteyling = new HashSet<CostsCarWashAndDeteylingView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idTypeServices { get; set; }

        [Display(Name = "Группа расходов")]
        public string name { get; set; }

        public virtual ICollection<CostsCarWashAndDeteylingView> costsCarWashAndDeteyling { get; set; }
    }
}