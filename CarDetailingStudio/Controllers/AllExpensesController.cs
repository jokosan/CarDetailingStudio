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
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.BLL.Model;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;

namespace CarDetailingStudio.Controllers
{
    public class AllExpensesController : Controller
    {
        private IAllExpenses _allExpenses;
        private IExpenseCategory _expenseCategory;
        private IUtilityCostsCategory _utilityCostsCategory;
        private ICostCategories _costCategories;

        public AllExpensesController(IAllExpenses allExpenses, IExpenseCategory expenseCategory, IUtilityCostsCategory utilityCostsCategory, ICostCategories costCategories)
        {
            _allExpenses = allExpenses;
            _expenseCategory = expenseCategory;
            _utilityCostsCategory = utilityCostsCategory;
            _costCategories = costCategories;
        }

        // GET: AllExpenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // Category
        public async Task<JsonResult> GetCountryList(string searchTerm)
        {
            var CounrtyList = Mapper.Map<IEnumerable<ExpenseCategoryView>>(await _expenseCategory.GetTableAll()).ToList();

            if (searchTerm != null)
            {
                CounrtyList = CounrtyList.Where(x => x.idExpenseCategory == Convert.ToInt32(searchTerm)).ToList();
            }

            var modifiedData = CounrtyList.Select(x => new
            {
                id = x.idExpenseCategory,
                text = x.name
            });

            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }

        // costCategories
        public async Task<JsonResult> GetStateList(string CountryIDs)
        {
            int modelCar = Int32.Parse(CountryIDs);
            TempData["ExpenseCategory"] = modelCar;

            List<CostCategoriesView> StateList = new List<CostCategoriesView>();

            var getCategory = Mapper.Map<IEnumerable<CostCategoriesView>>(await _costCategories.GetTableAll());
            var StateListGetCategory = getCategory.Where(x => x.typeOfExpenses == modelCar).ToList();

            foreach (var item in StateListGetCategory)
            {
                StateList.Add(item);
            }

            if (StateListGetCategory.Count > 0)
            {
                var OneItem = StateListGetCategory.First();
                TempData["CostCategories"] = Convert.ToInt32(OneItem.idCostCategories);
            }

            return Json(StateList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(string data)
        {
            TempData["CostCategories"] = Int32.Parse(data);
            return Json("");
        }

        // POST: AllExpenses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,indicationCounter,amount,dateExpenses,nameExpenses,expenseCategoryId,typeServicesId,paymentType")] AllExpenses allExpenses)
        {
            if (ModelState.IsValid)
            {
                if (TempData.ContainsKey("ExpenseCategory") && TempData.ContainsKey("CostCategories"))
                {
                    int expCategory = Convert.ToInt32(TempData["ExpenseCategory"]);
                    int costCategorie = Convert.ToInt32(TempData["CostCategories"]);

                    AllExpensesBll allExpensesBll = Mapper.Map<AllExpenses, AllExpensesBll>(allExpenses);

                    allExpensesBll.expenseCategoryId = expCategory;
                    allExpensesBll.utilityCostsCategoryId = costCategorie;

                    await _allExpenses.SaveExpenses(allExpensesBll);
                }              

                return RedirectToAction("Create");
            }

            var expenseCategory = await _expenseCategory.GetTableAll();
            ViewBag.Category = new SelectList(expenseCategory, "idExpenseCategory", "name");
            ViewBag.UtilityCostsCategory = new SelectList(await _utilityCostsCategory.GetTableAll(), "idUtilityCostsCategory", "Named");

            return View(allExpenses);
        }
    }
}
