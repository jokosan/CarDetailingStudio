using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.Models.ModelViews;
using Quartz.Impl.AdoJobStore;

namespace CarDetailingStudio.Controllers.Administration
{
    public partial class AdminController
    {
        [HttpGet]
        public async Task<ActionResult> IndexPriceListTireFittingAdditionalServices()
        {
            return View(Mapper.Map<IEnumerable<PriceListTireFittingAdditionalServicesView>>(await _priceTireFittingAdditionalServices.GetTableAll()).OrderBy(x => x.sorting));
        }

        [HttpGet]
        public async Task<ActionResult> EditPriceListTireFittingAdditionalServices(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PriceListTireFittingAdditionalServicesView priceListTireFitting = Mapper.Map<PriceListTireFittingAdditionalServicesView>(await _priceTireFittingAdditionalServices.SelectId(id.Value));

            if (priceListTireFitting == null)
            {
                return HttpNotFound();
            }

            return View(priceListTireFitting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPriceListTireFittingAdditionalServices([Bind(Include = "idPriceListTireFittingAdditionalServices,JobTitle,TheCost,sorting")] PriceListTireFittingAdditionalServicesView priceListTireFitting)
        {
            if (ModelState.IsValid)
            {
                PriceListTireFittingAdditionalServicesBll priceListTire = Mapper.Map<PriceListTireFittingAdditionalServicesView, PriceListTireFittingAdditionalServicesBll>(priceListTireFitting);
                await _priceTireFittingAdditionalServices.Update(priceListTire);
                return RedirectToAction("IndexPriceListTireFittingAdditionalServices");
            }

            return View(priceListTireFitting);
        }
    }
}