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
    public class CostsCarWashAndDeteylingController : Controller
    {
        private ICostsCarWashAndDeteyling _costsCarWashAndDeteyling;
        private IExpenseCategory _expenseCategory;

        public CostsCarWashAndDeteylingController(ICostsCarWashAndDeteyling costsCarWashAndDeteyling, IExpenseCategory expenseCategory)
        {
            _costsCarWashAndDeteyling = costsCarWashAndDeteyling;
            _expenseCategory = expenseCategory;
        }

        // GET: CostsCarWashAndDeteyling
        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<CostsCarWashAndDeteylingView>>(_costsCarWashAndDeteyling.GetTableAll()));
        }

        // GET: CostsCarWashAndDeteyling/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CostsCarWashAndDeteylingView costsCarWashAndDeteylingView = Mapper.Map<CostsCarWashAndDeteylingView>(_costsCarWashAndDeteyling.SelectId(id));

            if (costsCarWashAndDeteylingView == null)
            {
                return HttpNotFound();
            }
            return View(costsCarWashAndDeteylingView);
        }

        // GET: CostsCarWashAndDeteyling/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View();
        }

        // POST: CostsCarWashAndDeteyling/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCostsCarWashAndDeteyling,nameExpenses,amount,dateExpenses,expenseCategoryId")] CostsCarWashAndDeteylingView costsCarWashAndDeteylingView)
        {
            if (ModelState.IsValid)
            {
                CostsCarWashAndDeteylingBll costsCarWashAndDeteyling = Mapper.Map<CostsCarWashAndDeteylingView, CostsCarWashAndDeteylingBll>(costsCarWashAndDeteylingView);
                _costsCarWashAndDeteyling.Insert(costsCarWashAndDeteyling);
               
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(costsCarWashAndDeteylingView);
        }

        // GET: CostsCarWashAndDeteyling/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostsCarWashAndDeteylingView costsCarWashAndDeteylingView = Mapper.Map<CostsCarWashAndDeteylingView>(_costsCarWashAndDeteyling.SelectId(id));
            if (costsCarWashAndDeteylingView == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(costsCarWashAndDeteylingView);
        }

        // POST: CostsCarWashAndDeteyling/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCostsCarWashAndDeteyling,nameExpenses,amount,dateExpenses,expenseCategoryId")] CostsCarWashAndDeteylingView costsCarWashAndDeteylingView)
        {
            if (ModelState.IsValid)
            {
                CostsCarWashAndDeteylingBll costsCarWashAndDeteyling = Mapper.Map<CostsCarWashAndDeteylingView, CostsCarWashAndDeteylingBll>(costsCarWashAndDeteylingView);
                _costsCarWashAndDeteyling.Update(costsCarWashAndDeteyling);
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(costsCarWashAndDeteylingView);
        }
    }
}
