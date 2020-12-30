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
using System.Globalization;

namespace CarDetailingStudio.Controllers
{
    public class SalaryArchiveController : Controller
    {
        private readonly ISalaryArchive _salaryArchive;

        public SalaryArchiveController(
            ISalaryArchive salaryArchive)
        {
            _salaryArchive = salaryArchive;
        }

        // GET: SalaryArchive
        public async Task<ActionResult> SalaryArchive(DateTime? dateView)
        {
            if (dateView == null)
            {
                dateView = DateTime.Now;
            }

            var result = Mapper.Map<IEnumerable<SalaryArchiveView>>(await _salaryArchive.MonthlySalary(dateView.Value.Month - 1, dateView.Value.Year));
            var resultSinglDate = result.LastOrDefault();

            if (resultSinglDate != null)
            {
                dateView = resultSinglDate.date;
            }           

            Dictionary<int, string> monthName = new Dictionary<int, string>();

            int year = dateView.Value.Year;            
            int month = dateView.Value.Month;
            int day = new DateTime(year, month, 1).Day;

            for (int i = 1; i <= DateTime.Now.AddMonths(-1).Month; i++)
            {
                monthName.Add(i, nameOfTheMonth(year, i, day));
            }

            ViewBag.MonthName = monthName;
            ViewBag.CurrentMonth = nameOfTheMonth(year, month, day);

            return View(result);
        }

        private string nameOfTheMonth(int year, int month, int day) => new DateTime(year, month, day).ToString("MMMM", CultureInfo.CreateSpecificCulture("ru"));

        // GET: SalaryArchive/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryArchiveView salaryArchiveView = Mapper.Map<SalaryArchiveView>(await _salaryArchive.SelectId(id));
            if (salaryArchiveView == null)
            {
                return HttpNotFound();
            }
            return View(salaryArchiveView);
        }

        // GET: SalaryArchive/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryArchiveView salaryArchiveView = Mapper.Map<SalaryArchiveView>(await _salaryArchive.SelectId(id));
            if (salaryArchiveView == null)
            {
                return HttpNotFound();
            }
            //ViewBag.carWashWorkersId = new SelectList(db.CarWashWorkersViews, "id", "Name", salaryArchiveView.carWashWorkersId);
            return View(salaryArchiveView);
        }

        // POST: SalaryArchive/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idSalaryArchive,carWashWorkersId,date,amountOfCompletedOrders,monthlySalaryAmountEarned,monthlySalaryAmountReceived,balanceAtTheEndOfTheMonth,salaryStatus")] SalaryArchiveView salaryArchiveView)
        {
            if (ModelState.IsValid)
            {
                SalaryArchiveBll salaryArchive = Mapper.Map<SalaryArchiveView, SalaryArchiveBll>(salaryArchiveView);
                await _salaryArchive.Update(salaryArchive);
                return RedirectToAction("Index");
            }
            //ViewBag.carWashWorkersId = new SelectList(db.CarWashWorkersViews, "id", "Name", salaryArchiveView.carWashWorkersId);
            return View(salaryArchiveView);
        }

    }
}
