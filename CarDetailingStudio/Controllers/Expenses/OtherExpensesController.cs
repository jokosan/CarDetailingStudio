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
    public class OtherExpensesController : Controller
    {
        private IOtherExpenses _otherExpenses;
        private IExpenseCategory _expenseCategory;

        public OtherExpensesController(IOtherExpenses otherExpenses, IExpenseCategory expenseCategory)
        {
            _otherExpenses = otherExpenses;
            _expenseCategory = expenseCategory;
        }

        // GET: OtherExpenses
        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<OtherExpensesView>>(_otherExpenses.GetTableAll()));
        }

        // GET: OtherExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherExpensesView otherExpensesView = Mapper.Map<OtherExpensesView>(_otherExpenses.SelectId(id));
            if (otherExpensesView == null)
            {
                return HttpNotFound();
            }
            return View(otherExpensesView);
        }

        // GET: OtherExpenses/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View();
        }

        // POST: OtherExpenses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOtherExpenses,nameExpenses,amount,dateExpenses,expenseCategoryId")] OtherExpensesView otherExpensesView)
        {
            if (ModelState.IsValid)
            {
                OtherExpensesBll otherExpenses = Mapper.Map<OtherExpensesView, OtherExpensesBll>(otherExpensesView);
                _otherExpenses.Insert(otherExpenses);
               
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(otherExpensesView);
        }

        // GET: OtherExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherExpensesView otherExpensesView = Mapper.Map<OtherExpensesView>(_otherExpenses.SelectId(id));
            if (otherExpensesView == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(otherExpensesView);
        }

        // POST: OtherExpenses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOtherExpenses,nameExpenses,amount,dateExpenses,expenseCategoryId")] OtherExpensesView otherExpensesView)
        {
            if (ModelState.IsValid)
            {
                OtherExpensesBll otherExpenses = Mapper.Map<OtherExpensesView, OtherExpensesBll>(otherExpensesView);
                _otherExpenses.Insert(otherExpenses);
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(otherExpensesView);
        }
    }
}
