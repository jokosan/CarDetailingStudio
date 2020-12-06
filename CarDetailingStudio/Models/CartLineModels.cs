using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class CartLineModels
    {
        public ListOfGoodsView listOfGoodsView { get; set; }
        public int Quantity { get; set; }

    }
}