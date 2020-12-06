using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using CarDetailingStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CarDetailingStudio.Models.ModelViews;
using CarDetailingStudio.BLL.Model;
using System.Net;

namespace CarDetailingStudio.Controllers
{
    public class PriceListTireChangeServicesController : Controller
    {
        private IPriceListTireFitting _priceListTireFitting;
        private IPriceTireFittingAdditionalServices _priceTireFittingAdditionalServices;
        private ITireRadius _tireRadius;

        public PriceListTireChangeServicesController(
            IPriceListTireFitting priceListTireFitting,
            IPriceTireFittingAdditionalServices priceTireFittingAdditionalServices,
            ITireRadius tireRadius)
        {
            _priceListTireFitting = priceListTireFitting;
            _priceTireFittingAdditionalServices = priceTireFittingAdditionalServices;
            _tireRadius = tireRadius;
        }

        // GET: PriceListTireChangeServices
        public async Task<ActionResult> PriceChangeServices()
        {
            var priceTireFitting = Mapper.Map<IEnumerable<PriceListTireFittingView>>(await _priceListTireFitting.GetTableAll());
            var priceListTireFittingAdditionalServices = Mapper.Map<IEnumerable<PriceListTireFittingAdditionalServicesView>>(await _priceTireFittingAdditionalServices.GetTableAll());

            ViewBag.RadiusCar = await SelectTypeCarRadius(priceTireFitting, 1);
            ViewBag.RadiusFreightCar = await SelectTypeCarRadius(priceTireFitting, 2);

            ViewBag.PriceTireFitting = priceTireFitting;
            ViewBag.PriceListTireFittingAdditionalServices = priceListTireFittingAdditionalServices;

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> EditPriceChangeServices(int? idRadius, int? typeCar)
        {
            if (idRadius == null && typeCar == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var selectResult = Mapper.Map<IEnumerable<PriceListTireFittingView>>(await _priceListTireFitting.SelectId(idRadius, typeCar)).ToList();

            if (selectResult == null)
            {
                return HttpNotFound();
            }

            var radius = await _tireRadius.GetTableAll();
           // radius.ToList().Insert(0, new TireRadiusBll() { idTireRadius = 0, radius = "------"});
            ViewBag.Radius = new SelectList(radius, "idTireRadius", "radius");

            return View(selectResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPriceChangeServices(List<PriceListTireFittingView> priceListTires)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in priceListTires)
                    await _priceListTireFitting.Update(TransformEntity(item));
            }

            return View();
        }

        private async Task<IEnumerable<TireRadiusBll>> SelectTypeCarRadius(IEnumerable<PriceListTireFittingView> prices, int carType)
        {
            var key = new List<int>();
            var resultCar = prices.Where(x => x.TypeOfCarsId == carType);

            foreach (var item in resultCar.GroupBy(x => x.TireRadiusId))
                key.Add(item.Key.Value);

            return await _tireRadius.SeletTireRadius(key);
        }

        private PriceListTireFittingBll TransformEntity(PriceListTireFittingView entity) => Mapper.Map<PriceListTireFittingView, PriceListTireFittingBll>(entity);
    }
}