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
    public class BonusToSalaryController : Controller
    {
        private IBonusToSalary _bonusToSalary;
        private ICarWashWorkersServices _carWashWorkers;
        private IOrderCarWashWorkersServices _orderCarWash;

        public BonusToSalaryController(
            IBonusToSalary bonusToSalary,
            ICarWashWorkersServices carWashWorkers,
            IOrderCarWashWorkersServices orderCarWash)
        {
            _bonusToSalary = bonusToSalary;
            _carWashWorkers = carWashWorkers;
            _orderCarWash = orderCarWash;
        }
                
        // GET: BonusToSalary
        public async Task<ActionResult> Index()
        {            
            return View(Mapper.Map<IEnumerable<BonusToSalaryGroupView>>(await _bonusToSalary.TableGroup()));
        }

        // GET: BonusToSalary/Details/5
        public async Task<ActionResult> Info(int? id, int? idEmployee, DateTime date)
        {
            if (id == null && idEmployee == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bonusToSalaryView = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.GetTableAll(idEmployee));

            if (bonusToSalaryView == null)
            {
                return HttpNotFound();
            }
            return View(bonusToSalaryView);
        }

        // GET: BonusToSalary/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.carWashWorkersId = new SelectList(Mapper.Map<IEnumerable<CarWashWorkersView>>(await _carWashWorkers.GetTable()), "id", "Name");
            return View();
        }

        // POST: BonusToSalary/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idBonusToSalary,carWashWorkersId,amount,date,note")] BonusToSalaryView bonusToSalaryView)
        {
            if (ModelState.IsValid)
            {
                BonusToSalaryBll bonusToSalaryBll = Mapper.Map<BonusToSalaryView, BonusToSalaryBll>(bonusToSalaryView);
                await _bonusToSalary.Insert(bonusToSalaryBll);
                return RedirectToAction("Index");
            }

            ViewBag.carWashWorkersId = new SelectList(Mapper.Map<IEnumerable<CarWashWorkersView>>(await _carWashWorkers.GetTable()), "id", "Name");
            return View(bonusToSalaryView);
        }

        // GET: BonusToSalary/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BonusToSalaryView bonusToSalaryView = Mapper.Map<BonusToSalaryView>(await _bonusToSalary.SelectId(id));
            if (bonusToSalaryView == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.carWashWorkersId = new SelectList(Mapper.Map<IEnumerable<CarWashWorkersView>>(await _carWashWorkers.GetTable()), "id", "Name", bonusToSalaryView.carWashWorkersId);
            return View(bonusToSalaryView);
        }

        // POST: BonusToSalary/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idBonusToSalary,carWashWorkersId,amount,date,note")] BonusToSalaryView bonusToSalaryView)
        {
            if (ModelState.IsValid)
            {
                BonusToSalaryBll bonusToSalaryBll = Mapper.Map<BonusToSalaryView, BonusToSalaryBll>(bonusToSalaryView);
                await _bonusToSalary.Update(bonusToSalaryBll);
                return RedirectToAction("Index");
            }

            ViewBag.carWashWorkersId = new SelectList(Mapper.Map<IEnumerable<CarWashWorkersView>>(await _carWashWorkers.GetTable()), "id", "Name", bonusToSalaryView.carWashWorkersId);
            return View(bonusToSalaryView);
        }             
    }
}
