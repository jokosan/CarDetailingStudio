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
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.BLL.Model;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;

namespace CarDetailingStudio.Controllers
{
    public class AllExpensesController : Controller
    {
        private IAllExpenses _allExpenses;
        private IExpenseCategory _expenseCategory;
        private IUtilityCostsCategory _utilityCostsCategory;

        public AllExpensesController(IAllExpenses allExpenses, IExpenseCategory expenseCategory, IUtilityCostsCategory utilityCostsCategory)
        {
            _allExpenses = allExpenses;
            _expenseCategory = expenseCategory;
            _utilityCostsCategory = utilityCostsCategory;
        }

        // GET: AllExpenses/Create
        public async Task<ActionResult> Create()
        {
            var expenseCategory = await _expenseCategory.GetTableAll();
            ViewBag.Category = new SelectList(expenseCategory, "idExpenseCategory", "name");
            ViewBag.UtilityCostsCategory = new SelectList(await _utilityCostsCategory.GetTableAll(), "idUtilityCostsCategory", "Named");

            return View();
        }

        // POST: AllExpenses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,indicationCounter,amount,dateExpenses,nameExpenses,expenseCategoryId,typeServicesId")] AllExpenses allExpenses, int? SelectGroupClient, int? UtilityCostsCategory)
        {
            if (ModelState.IsValid && SelectGroupClient != null)
            {
                AllExpensesBll allExpensesBll = Mapper.Map<AllExpenses, AllExpensesBll>(allExpenses);
                
                allExpensesBll.expenseCategoryId = SelectGroupClient;
                allExpensesBll.utilityCostsCategoryId = UtilityCostsCategory;
              
                await _allExpenses.SaveExpenses(allExpensesBll);

                return RedirectToAction("Create");
            }

            var expenseCategory = await _expenseCategory.GetTableAll();
            ViewBag.Category = new SelectList(expenseCategory, "idExpenseCategory", "name");
            ViewBag.UtilityCostsCategory = new SelectList(await _utilityCostsCategory.GetTableAll(), "idUtilityCostsCategory", "Named");

            return View(allExpenses);
        }

    }
}
 