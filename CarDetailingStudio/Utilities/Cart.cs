using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Utilities
{
    public class Cart
    {
        private List<CartLineModels> lineCollection = new List<CartLineModels>();

        public void AddItem(ListOfGoodsView listOfGoodsView, int quantity)
        {
            CartLineModels line = lineCollection
                .Where(p => p.listOfGoodsView.idListOfGoods == listOfGoodsView.idListOfGoods)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLineModels
                {
                    listOfGoodsView = listOfGoodsView,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(ListOfGoodsView listOfGoodsView)
        {
            lineCollection.RemoveAll(l => l.listOfGoodsView.idListOfGoods == listOfGoodsView.idListOfGoods);
        }

        public double ComputeTotalValu()
        {
            return lineCollection.Sum(e => e.listOfGoodsView.price * e.Quantity).Value;
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLineModels> Lines 
        {
            get { return lineCollection; }
        }

       
    }
}