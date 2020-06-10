using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    public class CreditController : Controller
    {
        private ICreditServices _creditServices;
        private ICarWashWorkersServices _carWashWorkers;

        public CreditController(ICreditServices creditServices, ICarWashWorkersServices carWashWorkers)
        {
            _creditServices = creditServices;
            _carWashWorkers = carWashWorkers;
        }

        // GET: Credit
        public async Task<ActionResult> CreditInfo()
        {
            return View(Mapper.Map<IEnumerable<CreditView>>(await _creditServices.GetAll()));
        }

        // GET: Credit/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CarWashWorkersId = new SelectList(await _carWashWorkers.GetStaffAll(), "id", "Name", "Surname");
            return View();
        }

        // POST: Credit/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CarWashWorkersId,Date,Sum,RepaidDebt")] CreditView creditView)
        {
            if (ModelState.IsValid)
            {
                CreditBll credit = Mapper.Map<CreditView, CreditBll>(creditView);
                await _creditServices.Create(credit);

                return RedirectToAction("Index");
            }

            ViewBag.CarWashWorkersId = new SelectList(await _carWashWorkers.GetStaffAll(), "id", "Name", "Surname");
            return View(creditView);
        }
    }
}
