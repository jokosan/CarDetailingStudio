using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    public class CreditController : Controller
    {
        private ICreditServices _creditServices;
        private ICarWashWorkersServices _carWashWorkers;

        public CreditController(ICreditServices creditServices, ICarWashWorkersServices carWashWorkers)
        {
            _creditServices = creditServices;
            _carWashWorkers = carWashWorkers;
        }

        // GET: Credit
        public ActionResult CreditInfo()
        {
            return View(Mapper.Map<IEnumerable<CreditView>>(_creditServices.GetAll()));
        }

        // GET: Credit/Create
        public ActionResult Create()
        {
            ViewBag.CarWashWorkersId = new SelectList(_carWashWorkers.GetStaffAll(), "id", "Name", "Surname");
            return View();
        }

        // POST: Credit/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarWashWorkersId,Date,Sum,RepaidDebt")] CreditView creditView)
        {
            if (ModelState.IsValid)
            {
                CreditBll credit = Mapper.Map<CreditView, CreditBll>(creditView);
                _creditServices.Create(credit);

                return RedirectToAction("Index");
            }

            ViewBag.CarWashWorkersId = new SelectList(_carWashWorkers.GetStaffAll(), "id", "Name", "Surname");
            return View(creditView);
        }

        //// GET: Credit/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }



        //    if (creditView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(creditView);
        //}


        //// GET: Credit/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CreditView creditView = db.CreditViews.Find(id);
        //    if (creditView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CarWashWorkersId = new SelectList(db.CarWashWorkersViews, "id", "Name", creditView.CarWashWorkersId);
        //    return View(creditView);
        //}

        //// POST: Credit/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,CarWashWorkersId,Date,Sum,RepaidDebt")] CreditView creditView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(creditView).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CarWashWorkersId = new SelectList(db.CarWashWorkersViews, "id", "Name", creditView.CarWashWorkersId);
        //    return View(creditView);
        //}

        //// GET: Credit/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CreditView creditView = db.CreditViews.Find(id);
        //    if (creditView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(creditView);
        //}

        //// POST: Credit/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CreditView creditView = db.CreditViews.Find(id);
        //    db.CreditViews.Remove(creditView);
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
