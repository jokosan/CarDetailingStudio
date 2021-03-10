using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.BLL.Services.Contract;
using AutoMapper;
using CarDetailingStudio.Models.ModelViews;
using System.Net;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers.Administration
{
    public partial class AdminController
    {
        [HttpGet]
        public async Task<ActionResult> EditingExpenseGroups()
        {
            ViewBag.ExpenseCategory = Mapper.Map<IEnumerable<ExpenseCategoryView>>(await _expenseCategory.GetTableAll());
            ViewBag.CostCategories = Mapper.Map<IEnumerable<CostCategoriesView>>(await _costCategories.GetTableAll());

            return View();
        }

        #region expenseCategory
        [HttpGet]
        public ActionResult ExpenseCategoryCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExpenseCategoryCreate([Bind(Include = "idExpenseCategory,name")] ExpenseCategoryView expenseCategory)
        {
            if (ModelState.IsValid)
            {
                await _expenseCategory.Insert(TransformAnEntity(expenseCategory));
                return RedirectToAction("EditingExpenseGroups");
            }

            return View(expenseCategory);
        }

        [HttpGet]
        public async Task<ActionResult> ExpenseCategoryEdit(int? idExpenseCategory)
        {
            return View(Mapper.Map<ExpenseCategoryView>(await _expenseCategory.SelectId(idExpenseCategory)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExpenseCategoryEdit([Bind(Include = "idExpenseCategory,name")] ExpenseCategoryView expenseCategory)
        {
            if (ModelState.IsValid)
            {
                await _expenseCategory.Update(TransformAnEntity(expenseCategory));
                return RedirectToAction("EditingExpenseGroups");
            }

            return View(expenseCategory);
        }
        #endregion

  
        #region CostCategories
        [HttpGet]
        public async Task<ActionResult> CostCategoriesCreate()
        {
            ViewBag.ExpenseCategory = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CostCategoriesCreate([Bind(Include = "idCostCategories,typeOfExpenses,Name")] CostCategoriesView costCategories, int? ExpenseCategory)
        {
            if (ModelState.IsValid && ExpenseCategory != null)
            {
                costCategories.typeOfExpenses = ExpenseCategory;
                await _costCategories.Update(TransformAnEntity(costCategories));
                return RedirectToAction("EditingExpenseGroups");

            }

            ViewBag.ExpenseCategory = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CostCategoriesEdit(int? idCostCategories)
        {
            if (idCostCategories == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costCategories = Mapper.Map<CostCategoriesView>(await _costCategories.SelectId(idCostCategories));
            
            if (costCategories == null)
            {
                return HttpNotFound();
            }

            ViewBag.ExpenseCategory = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name", costCategories.typeOfExpenses);
            return View(costCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CostCategoriesEdit([Bind(Include = "idCostCategories,typeOfExpenses,Name")] CostCategoriesView costCategories, int? ExpenseCategory)
        {
            if (ModelState.IsValid && ExpenseCategory != null)
            {
                costCategories.typeOfExpenses = ExpenseCategory;
                await _costCategories.Update(TransformAnEntity(costCategories));
                return RedirectToAction("EditingExpenseGroups");
            }

             ViewBag.ExpenseCategory = new SelectList(await _expenseCategory.GetTableAll(), "idExpenseCategory", "name", costCategories.typeOfExpenses);
            return View(costCategories);
        }
        #endregion

        private ExpenseCategoryBll TransformAnEntity(ExpenseCategoryView entity) => Mapper.Map<ExpenseCategoryView, ExpenseCategoryBll>(entity);

        private CostCategoriesBll TransformAnEntity(CostCategoriesView entity) => Mapper.Map<CostCategoriesView, CostCategoriesBll>(entity);
    }
}