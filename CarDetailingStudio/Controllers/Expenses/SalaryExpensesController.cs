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
using CarDetailingStudio.BLL.Services.Contract;

namespace CarDetailingStudio.Controllers.Expenses
{
    public class SalaryExpensesController : Controller
    {
        private ISalaryExpenses _salaryExpenses;
        private IExpenseCategory _expenseCategory;
        private ICarWashWorkersServices _carWashWorkers;

        public SalaryExpensesController(ISalaryExpenses salaryExpenses, IExpenseCategory expenseCategory, ICarWashWorkersServices carWashWorkers)
        {
            _salaryExpenses = salaryExpenses;
            _expenseCategory = expenseCategory;
            _carWashWorkers = carWashWorkers;
        }

        // GET: SalaryExpenses
        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<SalaryExpensesView>>(_salaryExpenses.GetTableAll()));
        }

        // GET: SalaryExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryExpensesView salaryExpensesView = Mapper.Map<SalaryExpensesView>(_salaryExpenses.SelectId(id));
            if (salaryExpensesView == null)
            {
                return HttpNotFound();
            }
            return View(salaryExpensesView);
        }

        // GET: SalaryExpenses/Create
        public ActionResult Create()
        {
            DropDownListView();
            return View();
        }

        // POST: SalaryExpenses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSalaryExpenses,idCarWashWorkers,amount,dateExpenses,expenseCategoryId")] SalaryExpensesView salaryExpensesView)
        {
            if (ModelState.IsValid)
            {
                SalaryExpensesBll salaryExpensesBll = Mapper.Map<SalaryExpensesView, SalaryExpensesBll>(salaryExpensesView);
                _salaryExpenses.Insert(salaryExpensesBll);

                return RedirectToAction("Index");
            }

            DropDownListView();
            return View(salaryExpensesView);
        }

        // GET: SalaryExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SalaryExpensesView salaryExpensesView = Mapper.Map<SalaryExpensesView>(_salaryExpenses.SelectId(id));

            if (salaryExpensesView == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(salaryExpensesView);
        }

        // POST: SalaryExpenses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSalaryExpenses,idCarWashWorkers,amount,dateExpenses,expenseCategoryId")] SalaryExpensesView salaryExpensesView)
        {
            if (ModelState.IsValid)
            {
                SalaryExpensesBll salaryExpensesBll = Mapper.Map<SalaryExpensesView, SalaryExpensesBll>(salaryExpensesView);
                _salaryExpenses.Insert(salaryExpensesBll);
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
            return View(salaryExpensesView);
        }

        public void DropDownListView()
        {
            ViewBag.CarWashWorkers = new SelectList((from s in _carWashWorkers.GetStaffAll()
                                                     select new
                                                     {
                                                         id = s.id,
                                                         FullName = s.Surname + " " + s.Name + " " + s.Patronymic
                                                     }), "id", "FullName", null);

            ViewBag.Category = new SelectList(_expenseCategory.GetTableAll(), "idExpenseCategory", "name");
        }
    }
}
