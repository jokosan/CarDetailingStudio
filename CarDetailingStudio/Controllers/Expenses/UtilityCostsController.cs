using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers.Expenses
{
    public class UtilityCostsController : Controller
    {
        private IUtilityCosts _utilityCosts;
        private IExpenseCategory _expenseCategory;

        public UtilityCostsController(IUtilityCosts utilityCosts, IExpenseCategory expenseCategory)
        {
            _utilityCosts = utilityCosts;
            _expenseCategory = expenseCategory;
        }

        // GET: UtilityCosts
        public ActionResult ViewUtilityCosts()
        {
            return View(Mapper.Map<IEnumerable<UtilityCostsView>>(_utilityCosts.GetTableAll()));
        }

        // GET: UtilityCosts/Create
        public ActionResult CreateUtilityCosts()
        {
            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll().Where(x => (x.idExpenseCategory >= 10) && (x.idExpenseCategory <= 15)), "idExpenseCategory", "name" );
            return View();
        }

        // POST: UtilityCosts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUtilityCosts([Bind(Include = "idUtilityCosts,indicationCounter,amount,dateExpenses,expenseCategoryId")] UtilityCostsView utilityCostsView)
        {
            if (ModelState.IsValid)
            {
                UtilityCostsBll utilityCosts = Mapper.Map<UtilityCostsView, UtilityCostsBll>(utilityCostsView);
                _utilityCosts.Insert(utilityCosts);

                return RedirectToAction("ViewUtilityCosts");
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll().Where(x => (x.idExpenseCategory >= 10) && (x.idExpenseCategory <= 15)), "idExpenseCategory", "name");
            return View(utilityCostsView);
        }


        // GET: UtilityCosts/Edit/5
        public ActionResult EditUtilityCosts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UtilityCostsView utilityCostsView = Mapper.Map<UtilityCostsView>(_utilityCosts.SelectId(id));
            
            if (utilityCostsView == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll().Where(x => (x.idExpenseCategory >= 10) && (x.idExpenseCategory <= 15)), "idExpenseCategory", "name");
            return View(utilityCostsView);
        }

        // POST: UtilityCosts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUtilityCosts([Bind(Include = "idUtilityCosts,indicationCounter,amount,dateExpenses,expenseCategoryId")] UtilityCostsView utilityCostsView)
        {
            if (ModelState.IsValid)
            {
                UtilityCostsBll utilityCosts = Mapper.Map<UtilityCostsView, UtilityCostsBll>(utilityCostsView);
                _utilityCosts.Update(utilityCosts);
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll().Where(x => (x.idExpenseCategory >= 10) && (x.idExpenseCategory <= 15)), "idExpenseCategory", "name");
            return View(utilityCostsView);
        }
    }
}
