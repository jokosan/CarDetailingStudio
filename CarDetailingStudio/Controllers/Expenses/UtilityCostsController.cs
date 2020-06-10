using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.Models.ModelViews;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        public async Task<ActionResult> ViewUtilityCosts()
        {
            return View(Mapper.Map<IEnumerable<UtilityCostsView>>(await _utilityCosts.GetTableAll()));
        }

        // GET: UtilityCosts/Create
        public async Task<ActionResult> CreateUtilityCosts()
        {
            var expenseCategory = await _expenseCategory.GetTableAll();

            ViewBag.Category = new SelectList(expenseCategory.Where(x => (x.idExpenseCategory >= 10) && (x.idExpenseCategory <= 15)), "idExpenseCategory", "name");
            return View();
        }

        // POST: UtilityCosts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUtilityCosts([Bind(Include = "idUtilityCosts,indicationCounter,amount,dateExpenses,expenseCategoryId")] UtilityCostsView utilityCostsView)
        {
            if (ModelState.IsValid)
            {
                UtilityCostsBll utilityCosts = Mapper.Map<UtilityCostsView, UtilityCostsBll>(utilityCostsView);
                await _utilityCosts.Insert(utilityCosts);

                return RedirectToAction("ViewUtilityCosts");
            }

            var expenseCategory = await _expenseCategory.GetTableAll();

            ViewBag.Category = new SelectList(expenseCategory.Where(x => (x.idExpenseCategory >= 10) && (x.idExpenseCategory <= 15)), "idExpenseCategory", "name");
            return View(utilityCostsView);
        }


        // GET: UtilityCosts/Edit/5
        public async Task<ActionResult> EditUtilityCosts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UtilityCostsView utilityCostsView = Mapper.Map<UtilityCostsView>(await _utilityCosts.SelectId(id));

            if (utilityCostsView == null)
            {
                return HttpNotFound();
            }

            var expenseCategory = await _expenseCategory.GetTableAll();

            ViewBag.Category = new SelectList(expenseCategory.Where(x => (x.idExpenseCategory >= 10) && (x.idExpenseCategory <= 15)), "idExpenseCategory", "name");
            return View(utilityCostsView);
        }

        // POST: UtilityCosts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUtilityCosts([Bind(Include = "idUtilityCosts,indicationCounter,amount,dateExpenses,expenseCategoryId")] UtilityCostsView utilityCostsView)
        {
            if (ModelState.IsValid)
            {
                UtilityCostsBll utilityCosts = Mapper.Map<UtilityCostsView, UtilityCostsBll>(utilityCostsView);
                await _utilityCosts.Update(utilityCosts);
                return RedirectToAction("Index");
            }

            var expenseCategory = await SelectValue();

            ViewBag.Category = new SelectList(expenseCategory.Where(x => (x.idExpenseCategory >= 10) && (x.idExpenseCategory <= 15)), "idExpenseCategory", "name");
            return View(utilityCostsView);
        }

        private async Task<IEnumerable<ExpenseCategoryView>> SelectValue() => Mapper.Map<IEnumerable<ExpenseCategoryView>>(await _expenseCategory.GetTableAll());
        
    }
}
