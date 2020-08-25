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
using CarDetailingStudio.BLL.Services.Contract;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers
{
    public class CashierController : Controller
    {
        private ICashier _cashier;

        public CashierController(ICashier cashier)
        {
            _cashier = cashier;
        }

        // GET: Cashier
        public async Task<ActionResult> Index()
        {
            return View( Mapper.Map<IEnumerable<CashierView>>(await _cashier.GetTableAll()));
        }

       
        // GET: Cashier/Create
        public ActionResult AmountAtTheBeginningOfTheDay()
        {
            return View();
        }

        // POST: Cashier/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AmountAtTheBeginningOfTheDay([Bind(Include = "idCashier,date,amountBeginningOfTheDay,amountEndOfTheDay")] CashierView cashierView)
        {
            if (ModelState.IsValid)
            {
                CashierBll cashier = Mapper.Map<CashierView, CashierBll>(cashierView);
                cashier.date = DateTime.Now;

                await _cashier.Insert(cashier);

                return Redirect("/Order/Index");
            }

            return View(cashierView);
        }        
    }
}
