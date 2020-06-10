using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.Models.ModelViews;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        public async Task<ActionResult> Index()
        {
            return View(Mapper.Map<IEnumerable<CostsCarWashAndDeteylingView>>(await _costsCarWashAndDeteyling.GetTableAll()));
        }

        // GET: CostsCarWashAndDeteyling/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CostsCarWashAndDeteylingView costsCarWashAndDeteylingView = Mapper.Map<CostsCarWashAndDeteylingView>(await _costsCarWashAndDeteyling.SelectId(id));

            if (costsCarWashAndDeteylingView == null)
            {
                return HttpNotFound();
            }

            return View(costsCarWashAndDeteylingView);
        }

        // GET: CostsCarWashAndDeteyling/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Category = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View();
        }

        // POST: CostsCarWashAndDeteyling/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idCostsCarWashAndDeteyling,nameExpenses,amount,dateExpenses,expenseCategoryId")] CostsCarWashAndDeteylingView costsCarWashAndDeteylingView)
        {
            if (ModelState.IsValid)
            {
                CostsCarWashAndDeteylingBll costsCarWashAndDeteyling = Mapper.Map<CostsCarWashAndDeteylingView, CostsCarWashAndDeteylingBll>(costsCarWashAndDeteylingView);
                await _costsCarWashAndDeteyling.Insert(costsCarWashAndDeteyling);

                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(costsCarWashAndDeteylingView);
        }

        // GET: CostsCarWashAndDeteyling/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostsCarWashAndDeteylingView costsCarWashAndDeteylingView = Mapper.Map<CostsCarWashAndDeteylingView>(await _costsCarWashAndDeteyling.SelectId(id));
            if (costsCarWashAndDeteylingView == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(costsCarWashAndDeteylingView);
        }

        // POST: CostsCarWashAndDeteyling/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idCostsCarWashAndDeteyling,nameExpenses,amount,dateExpenses,expenseCategoryId")] CostsCarWashAndDeteylingView costsCarWashAndDeteylingView)
        {
            if (ModelState.IsValid)
            {
                CostsCarWashAndDeteylingBll costsCarWashAndDeteyling = Mapper.Map<CostsCarWashAndDeteylingView, CostsCarWashAndDeteylingBll>(costsCarWashAndDeteylingView);
                await _costsCarWashAndDeteyling.Update(costsCarWashAndDeteyling);
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(costsCarWashAndDeteylingView);
        }
    }
}
