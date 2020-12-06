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
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers.Trade
{
    public class PriceController : Controller
    {
        private IListOfGoods _listOfGoods;
        private IProductCategories _productCategories;

        public PriceController(
            IListOfGoods listOfGoods,
            IProductCategories productCategories)
        {
            _listOfGoods = listOfGoods;
            _productCategories = productCategories;
        }

        // GET: Price
        public async Task<ActionResult> PriceList(int? idCategories = 1)
        {
            ViewBag.Categories = Mapper.Map<IEnumerable<ProductCategoriesView>>(await _productCategories.GetTableAll());

            return View(Mapper.Map<IEnumerable<ListOfGoodsView>>(await _listOfGoods.GetTableAll(idCategories.Value)));
        }

        // GET: Price/Create
        public async Task<ActionResult> PriceListCreate()
        {
            ViewBag.Categories = new SelectList(await _productCategories.GetTableAll(), "idProductСategories", "Name");
            return View();
        }

        // POST: Price/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PriceListCreate([Bind(Include = "idListOfGoods,productCategoriesId,name,productDescription,urlFoto,visible,price,salaryFromSale")] ListOfGoodsView listOfGoodsView)
        {
            if (ModelState.IsValid)
            {
                await _listOfGoods.Insert(TransformEntity(listOfGoodsView));
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(await _productCategories.GetTableAll(), "idProductСategories", "Name");
            return View(listOfGoodsView);
        }

        // GET: Price/Edit/5
        public async Task<ActionResult> PriceListEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ListOfGoodsView listOfGoodsView = Mapper.Map<ListOfGoodsView>(await _listOfGoods.SelectId(id));
            ViewBag.Categories = new SelectList(await _productCategories.GetTableAll(), "idProductСategories", "Name", listOfGoodsView.productCategoriesId);
            
            if (listOfGoodsView == null)
            {
                return HttpNotFound();
            }

            return View(listOfGoodsView);
        }

        // POST: Price/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PriceListEdit([Bind(Include = "idListOfGoods,productCategoriesId,name,productDescription,urlFoto,visible,price,salaryFromSale")] ListOfGoodsView listOfGoodsView)
        {
            if (ModelState.IsValid)
            {
                await _listOfGoods.Update(TransformEntity(listOfGoodsView));
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(await _productCategories.GetTableAll(), "idProductСategories", "Name", listOfGoodsView.productCategoriesId);
            return View(listOfGoodsView);
        }

        private ListOfGoodsBll TransformEntity(ListOfGoodsView entity) => Mapper.Map<ListOfGoodsView, ListOfGoodsBll>(entity); 
    }
}
