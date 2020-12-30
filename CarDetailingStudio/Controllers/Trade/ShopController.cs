using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using CarDetailingStudio.BLL.Services.Trade.TradeContract;
using AutoMapper;
using CarDetailingStudio.Utilities;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers.Trade
{
    public class ShopController : Controller
    {
        private IListOfGoods _listOfGoods;
        private IProductCategories _productCategories;
        private IGoodsSold _goodsSold;
        private IProcurement _procurement;


        public ShopController(
            IListOfGoods listOfGoods,
            IProductCategories productCategories,
            IGoodsSold goodsSold,
            IProcurement procurement)
        {
            _goodsSold = goodsSold;
            _productCategories = productCategories;
            _procurement = procurement;
            _listOfGoods = listOfGoods;
        }

        // GET: Shop
        public async Task<ActionResult> Сatalog(Cart cart)
        {
            var listOfGoods = Mapper.Map<IEnumerable<ListOfGoodsView>>(await _listOfGoods.GetTableAll());
            var categories = Mapper.Map<IEnumerable<ProductCategoriesView>>(await _productCategories.GetTableAll());

            if (cart.ComputeTotalValu() != 0)
            {
                ViewBag.Cart = true;
                ViewBag.InfoQuantity = cart.Lines.Sum(x => x.Quantity);
                ViewBag.InfoSum = cart.ComputeTotalValu().ToString();
            }
            else
            {
                ViewBag.Cart = false;
            }

            ViewBag.Categories = categories;
            return View(listOfGoods);
        }

        public PartialViewResult Summary(Cart cart) => PartialView(cart);

        public ViewResult Basket(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModels
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<ActionResult> Basket(Cart cart, int? formPayment)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, не добавлено ни одного товара в карзину заказов");
            }

            if (ModelState.IsValid)
            {
                var goodsSold = new List<GoodsSoldBll>();

                foreach (var itemCart in cart.Lines)
                {
                    goodsSold.Add(new GoodsSoldBll
                    {
                        listOfGoodsId = itemCart.listOfGoodsView.idListOfGoods,
                        Date = DateTime.Now,
                        priceForOne = itemCart.listOfGoodsView.price,
                        orderPrice = (itemCart.Quantity * itemCart.listOfGoodsView.price),
                        amount = itemCart.Quantity,
                        percentageOfSale = (itemCart.Quantity * itemCart.listOfGoodsView.salaryFromSale),
                        PaymentState = formPayment
                        
                    });
                }

                await _goodsSold.InsertList(goodsSold);
                cart.Clear();

                return RedirectToAction("OrderShopArxiv");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public async Task<RedirectToRouteResult> AddToCart(Cart cart, int? productId, string returnUrl, int? quantity)
        {
            var listOfGoods = Mapper.Map<ListOfGoodsView>(await _listOfGoods.SelectId(productId));

            if (quantity == 0 || quantity == null)
            {
                return RedirectToAction("Сatalog", new { returnUrl });
            }

            if (listOfGoods != null)
            {
                cart.AddItem(listOfGoods, quantity.Value);
            }

            return RedirectToAction("Сatalog", new { returnUrl });
        }

        public async Task<RedirectToRouteResult> RemoveFromCart(Cart cart, int? productId, string returnUrl)
        {
            var listOfGoods = Mapper.Map<ListOfGoodsView>(await _listOfGoods.SelectId(productId));

            if (listOfGoods != null)
            {
                cart.RemoveLine(listOfGoods);
            }

            return RedirectToAction("Basket", new { returnUrl });
        }

        public async Task<ActionResult> OrderShopArxiv() => View(Mapper.Map<IEnumerable<GoodsSoldView>>(await _goodsSold.GetTableAll()).OrderByDescending(x => x.idGoodsSold));

    }
}
