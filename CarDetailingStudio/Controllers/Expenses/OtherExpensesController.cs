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
        public async Task<ActionResult> Index()
        {
            return View(Mapper.Map<IEnumerable<OtherExpensesView>>(await _otherExpenses.GetTableAll()));
        }

        // GET: OtherExpenses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherExpensesView otherExpensesView = Mapper.Map<OtherExpensesView>(await _otherExpenses.SelectId(id));
            if (otherExpensesView == null)
            {
                return HttpNotFound();
            }
            return View(otherExpensesView);
        }

        // GET: OtherExpenses/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Category = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View();
        }

        // POST: OtherExpenses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idOtherExpenses,nameExpenses,amount,dateExpenses,expenseCategoryId")] OtherExpensesView otherExpensesView)
        {
            if (ModelState.IsValid)
            {
                OtherExpensesBll otherExpenses = Mapper.Map<OtherExpensesView, OtherExpensesBll>(otherExpensesView);
                await _otherExpenses.Insert(otherExpenses);

                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(otherExpensesView);
        }

        // GET: OtherExpenses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherExpensesView otherExpensesView = Mapper.Map<OtherExpensesView>(await _otherExpenses.SelectId(id));
            if (otherExpensesView == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(otherExpensesView);
        }

        // POST: OtherExpenses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idOtherExpenses,nameExpenses,amount,dateExpenses,expenseCategoryId")] OtherExpensesView otherExpensesView)
        {
            if (ModelState.IsValid)
            {
                OtherExpensesBll otherExpenses = Mapper.Map<OtherExpensesView, OtherExpensesBll>(otherExpensesView);
                await _otherExpenses.Insert(otherExpenses);
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(otherExpensesView);
        }
    }
}
