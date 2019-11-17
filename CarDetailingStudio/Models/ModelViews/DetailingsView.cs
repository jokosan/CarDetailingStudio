﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class DetailingsView
    {
        public DetailingsView()
        {
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrderView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
       
        public int Id { get; set; }
        [Required]
        public string services_list { get; set; }

        public string validity { get; set; }
        public string note { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> S { get; set; }
      
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> M { get; set; }
       
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> L { get; set; }
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public Nullable<double> XL { get; set; }
        public string group { get; set; }

        [Required]
        public string status { get; set; }
        [Required]
        public string currency { get; set; }
        public Nullable<bool> mark { get; set; }

        [Required]
        public Nullable<int> IdGroupWashServices { get; set; }

        [Required]
        public Nullable<int> IdTypeService { get; set; }

        public virtual GroupWashServicesView GroupWashServices { get; set; }
        public virtual ICollection<ServisesCarWashOrderView> ServisesCarWashOrder { get; set; }
    }
}