using CarDetailingStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class CartIndexViewModels
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}