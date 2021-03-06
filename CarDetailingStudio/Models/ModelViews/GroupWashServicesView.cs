﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class GroupWashServicesView
    {
        public GroupWashServicesView()
        {
            this.Detailings = new HashSet<DetailingsView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Display (Name ="Название группы")]
        public string group { get; set; }

        public virtual ICollection<DetailingsView> Detailings { get; set; }
    }
}