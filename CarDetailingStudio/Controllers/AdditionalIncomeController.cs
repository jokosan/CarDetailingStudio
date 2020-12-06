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
using CarDetailingStudio.BLL.Services.Contract;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers
{
    public class AdditionalIncomeController : Controller
    {
        private readonly IAdditionalIncome _additionalIncome;

        public AdditionalIncomeController(
            IAdditionalIncome additionalIncome)
        {
            _additionalIncome = additionalIncome;
        }

        // GET: AdditionalIncome
        public async Task<ActionResult> Index()
        {
            return View(Mapper.Map<IEnumerable<AdditionalIncomeView>>(await _additionalIncome.GetTableAll()));
        }

        // GET: AdditionalIncome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdditionalIncome/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idAdditionalIncome,IncomeCategory,Date,Amount,Note")] AdditionalIncomeView additionalIncomeView)
        {
            if (ModelState.IsValid)
            {
                await _additionalIncome.Insert(TransformEntity(additionalIncomeView));
                return RedirectToAction("Index");
            }

            return View(additionalIncomeView);
        }

        // GET: AdditionalIncome/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdditionalIncomeView additionalIncomeView = Mapper.Map<AdditionalIncomeView>(await _additionalIncome.SelectId(id));
            if (additionalIncomeView == null)
            {
                return HttpNotFound();
            }
            return View(additionalIncomeView);
        }

        // POST: AdditionalIncome/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idAdditionalIncome,IncomeCategory,Date,Amount,Note")] AdditionalIncomeView additionalIncomeView)
        {
            if (ModelState.IsValid)
            {
                await _additionalIncome.Update(TransformEntity(additionalIncomeView));
                return RedirectToAction("Index");
            }
            return View(additionalIncomeView);
        }

        private AdditionalIncomeBll TransformEntity(AdditionalIncomeView entity) => Mapper.Map<AdditionalIncomeView, AdditionalIncomeBll>(entity);
    }
}
