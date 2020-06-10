using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    public class OrderCarpetWashingController : Controller
    {
        private IOrderCarpetWashingServices _orderCarpetWashingServices;
        private IBrigadeForTodayServices _brigadeForToday;
        private IDetailingsServises _detailings;
        private IOrder _order;
        private IWageModules _wageModules;

        public OrderCarpetWashingController(IOrderCarpetWashingServices orderCarpetWashing, IBrigadeForTodayServices brigadeForTodayServices,
                                            IDetailingsServises detailings, IOrder order, IWageModules wageModules)
        {
            _orderCarpetWashingServices = orderCarpetWashing;
            _brigadeForToday = brigadeForTodayServices;
            _detailings = detailings;
            _order = order;
            _wageModules = wageModules;
        }

        // GET: OrderCarpetWashing
        public async Task<ActionResult> OrderCarpetWashing()
        {
            return View(Mapper.Map<IEnumerable<OrderCarpetWashingView>>(await _orderCarpetWashingServices.GetIncludeWhere()));
        }

        // GET: OrderCarpetWashing/Details/5
        public async Task<ActionResult> DetailsOrderCarpetWashing(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderCarpetWashingView orderCarpetWashingView = Mapper.Map<OrderCarpetWashingView>(await _orderCarpetWashingServices.SelectId(id));
            if (orderCarpetWashingView == null)
            {
                return HttpNotFound();
            }
            return View(orderCarpetWashingView);
        }

        // GET: OrderCarpetWashing/Create
        public async Task<ActionResult> AddOrderCarpetWashing()
        {
            var brigadeForToday = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigadeForToday.GetDateTimeNow());
            var resultServisesPrice = Mapper.Map<DetailingsView>(await _detailings.GetId(87));

            TempData["priseServis"] = resultServisesPrice.S;

            ViewBag.Adninistrator = brigadeForToday.Where(x => x.StatusId < 3);
            ViewBag.Brigade = brigadeForToday.Where(x => x.StatusId == 3);
            ViewBag.Servises = resultServisesPrice;

            return View();
        }

        // POST: OrderCarpetWashing/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrderCarpetWashing([Bind(Include = "idOrderCarpetWashing,orderServicesCarWashId,Customer,telephone,area,orderDate,orderClosingDate,orderCompletionDate")] OrderCarpetWashingView orderCarpetWashingView, List<string> idBrigade, int? idPaymentState)
        {
            if (ModelState.IsValid && idBrigade != null && TempData["priseServis"] != null)
            {
                int priceServis = Convert.ToInt32(TempData["priseServis"].ToString());

                OrderCarpetWashingBll orderCarpetWashing = Mapper.Map<OrderCarpetWashingView, OrderCarpetWashingBll>(orderCarpetWashingView);

                int resultOrderServicesCarWash = await _order.OrderForCarpetCleaning(orderCarpetWashing, idPaymentState, priceServis);
                orderCarpetWashing.orderServicesCarWashId = resultOrderServicesCarWash;

                await _orderCarpetWashingServices.Insert(orderCarpetWashing);
                await _wageModules.Payroll(resultOrderServicesCarWash, idBrigade);

                return RedirectToAction("OrderCarpetWashing", "OrderCarpetWashing");
            }

            var brigadeForToday = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigadeForToday.GetDateTimeNow());
            var resultServisesPrice = Mapper.Map<DetailingsView>(await _detailings.GetId(87));

            TempData["priseServis"] = resultServisesPrice.S;

            ViewBag.Adninistrator = brigadeForToday.Where(x => x.StatusId < 3);
            ViewBag.Brigade = brigadeForToday.Where(x => x.StatusId == 3);
            ViewBag.Servises = resultServisesPrice;

            return View(orderCarpetWashingView);
        }

        // GET: OrderCarpetWashing/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderCarpetWashingView orderCarpetWashingView = Mapper.Map<OrderCarpetWashingView>(await _orderCarpetWashingServices.SelectId(id));
            if (orderCarpetWashingView == null)
            {
                return HttpNotFound();
            }

            return View(orderCarpetWashingView);
        }

        // POST: OrderCarpetWashing/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idOrderCarpetWashing,orderServicesCarWashId,Customer,telephone,area,orderDate,orderClosingDate,orderCompletionDate")] OrderCarpetWashingView orderCarpetWashingView)
        {
            if (ModelState.IsValid)
            {
                OrderCarpetWashingBll orderCarpetWashing = Mapper.Map<OrderCarpetWashingView, OrderCarpetWashingBll>(orderCarpetWashingView);
                await _orderCarpetWashingServices.Update(orderCarpetWashing);
                return RedirectToAction("Index");
            }
            return View(orderCarpetWashingView);
        }

    }
}
