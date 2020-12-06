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
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;

namespace CarDetailingStudio.Controllers.Expenses
{
    public class ExpensesController : Controller
    {
        private IExpenses _expenses;

        public ExpensesController(
            IExpenses expenses)
        {
            _expenses = expenses;
        }

        // GET: Expenses
        public async Task<ActionResult> Index(int? TypeExpenses = null)
        {
            if (TypeExpenses == null)
            {
                var resultExpenses = Mapper.Map<IEnumerable<ExpensesView>>(await _expenses.GetTableAll()).OrderByDescending(x => x.dateExpenses);
                return View(resultExpenses);
            }
            else
            {
                var resultExpenses = Mapper.Map<IEnumerable<ExpensesView>>(await _expenses.GetTableAll(TypeExpenses.Value)).OrderByDescending(x => x.dateExpenses);
                return View(resultExpenses);
            }            
        }

        // GET: Expenses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpensesView expensesView = Mapper.Map<ExpensesView>(await _expenses.SelectId(id));
            if (expensesView == null)
            {
                return HttpNotFound();
            }
            return View(expensesView);
        }
    }
}
