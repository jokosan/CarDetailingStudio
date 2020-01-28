using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;

namespace CarDetailingStudio.Controllers
{
    public class CostsController : Controller
    {
        private ICostServices _costServices;

        public CostsController(ICostServices costServices)
        {
            _costServices = costServices;
        }

        // GET: Costs
        public ActionResult CostsInfo()
        {
            return View(Mapper.Map<IEnumerable<CostsView>>(_costServices.GetCost()));
        }

        //// GET: Costs/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CostsView costsView = db.CostsViews.Find(id);
        //    if (costsView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(costsView);
        //}

        //// GET: Costs/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Costs/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Type,Name,expenses,Date")] CostsView costsView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CostsViews.Add(costsView);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(costsView);
        //}

        //// GET: Costs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CostsView costsView = db.CostsViews.Find(id);
        //    if (costsView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(costsView);
        //}

        //// POST: Costs/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Type,Name,expenses,Date")] CostsView costsView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(costsView).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(costsView);
        //}

        //// GET: Costs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CostsView costsView = db.CostsViews.Find(id);
        //    if (costsView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(costsView);
        //}

        //// POST: Costs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CostsView costsView = db.CostsViews.Find(id);
        //    db.CostsViews.Remove(costsView);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
