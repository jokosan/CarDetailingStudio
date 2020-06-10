using AutoMapper;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.TireStorage.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers.TireStorage
{
    public class TireStorageController : Controller
    {
        private ITireStorage _tireStorage;
        private IStorageFee _storageFee;
        private IReviwOrderModules _reviwOrder;
        private IBrigadeForTodayServices _brigadeForToday;
        private IClientsOfCarWashServices _clientsOfCarWash;
        private IOrder _order;
        private IWageModules _wage;

        public TireStorageController(ITireStorage tireStorage, IStorageFee storageFee, IReviwOrderModules reviwOrder,
                                     IBrigadeForTodayServices brigadeForToday, IClientsOfCarWashServices clientsOfCarWash,
                                     IOrder order, IWageModules wage)
        {
            _tireStorage = tireStorage;
            _storageFee = storageFee;
            _reviwOrder = reviwOrder;
            _brigadeForToday = brigadeForToday;
            _clientsOfCarWash = clientsOfCarWash;
            _order = order;
            _wage = wage;
        }

        // GET: TireStorage
        public async Task<ActionResult> OrderTireStorage()
        {
            return View(Mapper.Map<IEnumerable<TireStorageView>>(await _tireStorage.GetTableAll()));
        }

        // GET: TireStorage/Create
        public async Task<ActionResult> CreateTireStorageOrder(int? IdClient)
        {
            if (IdClient != null)
            {
                ClientsOfCarWashView clientList = Mapper.Map<ClientsOfCarWashView>(await _clientsOfCarWash.GetId(IdClient));
                ViewBag.Client = clientList;

                return View();
            }

            return RedirectToRoute(new { controller = "Order", action = "Index" });

        }

        // POST: TireStorage/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTireStorageOrder([Bind(Include = "Id,ClientId,carWashWorkersId,dateOfAdoption,quantity,radius,firm,discAvailability,storageFeeId,tireStorageBags,wheelWash,IdOrderServicesCarWash,silicone,storageTime")] OrderTireStorageModelView tireStorageView,
                                                    int customer)
        {
            if (ModelState.IsValid)
            {
                tireStorageView.dateOfAdoption = DateTime.Now;
                tireStorageView.carWashWorkersId = customer;

                TempData["TireStorageOrder"] = tireStorageView;

                return RedirectToAction("ReviewTireStorageOrder");
            }

            return View(tireStorageView);
        }

        public async Task<ActionResult> ReviewTireStorageOrder()
        {
            if (TempData.ContainsKey("TireStorageOrder"))
            {
                OrderTireStorageModelView pageCreateResult = new OrderTireStorageModelView();

                pageCreateResult = TempData["TireStorageOrder"] as OrderTireStorageModelView;
                TempData.Keep("TireStorageOrder");

                OrderTireStorageModelBll tireStorageBll = Mapper.Map<OrderTireStorageModelView, OrderTireStorageModelBll>(pageCreateResult);
                var reviwOrderModules = Mapper.Map<ReviwOrderModelBll, ReviwOrderModelView>(await _reviwOrder.ReviwOrder(tireStorageBll));

                var brigadeForToday = Mapper.Map<IEnumerable<BrigadeForTodayView>>(_brigadeForToday.GetDateTimeNow());

                ViewBag.Adninistrator = brigadeForToday.Where(x => x.StatusId < 3);
                ViewBag.Brigade = brigadeForToday.Where(x => x.StatusId == 3);
                ViewBag.CarWashWorkers = new SelectList(await _brigadeForToday.GetDateTimeNow(), "id", "IdCarWashWorkers");
                ViewBag.ReviwOrder = reviwOrderModules;
                ViewBag.Services = reviwOrderModules.tireStorageServices;
                ViewBag.Sum = reviwOrderModules.priceDisk + reviwOrderModules.priceNumberOfPackets +
                    reviwOrderModules.priceOfTire + reviwOrderModules.priceSilicone + reviwOrderModules.priceWheelWash;

                TempData["ReviwOrderMode"] = reviwOrderModules;

                return View(pageCreateResult);
            }

            return RedirectToAction("CreateTireStorageOrder");
        }

        [HttpPost]
        public async Task<ActionResult> ReviewTireStorageOrder(string idBrigade, double? sum, int? idPaymentState, string idAdmin)
        {
            if (TempData.ContainsKey("TireStorageOrder") && TempData.ContainsKey("ReviwOrderMode"))
            {
                var pageCreateResult = TempData["TireStorageOrder"] as OrderTireStorageModelView;
                var reviwOrderModules = TempData["ReviwOrderMode"] as ReviwOrderModelView;

                OrderTireStorageModelBll orderTireStorage = Mapper.Map<OrderTireStorageModelView, OrderTireStorageModelBll>(pageCreateResult);
                ReviwOrderModelBll orderModelBll = Mapper.Map<ReviwOrderModelView, ReviwOrderModelBll>(reviwOrderModules);

                int idOrder = await _order.Chekout(orderTireStorage, sum, idPaymentState);

                await _wage.AdminWageTireStorage(Convert.ToInt32(idAdmin), idOrder, pageCreateResult.quantity.Value);

                if (reviwOrderModules.priceSilicone != 0 || reviwOrderModules.priceWheelWash != 0)
                {
                    await _wage.Payroll(idOrder, Convert.ToInt32(idBrigade), Convert.ToInt32(idAdmin), orderModelBll);
                }

                pageCreateResult.carWashWorkersId = Convert.ToInt32(idAdmin);
            }

            return Redirect("/Order/Index");
        }
    }
}
