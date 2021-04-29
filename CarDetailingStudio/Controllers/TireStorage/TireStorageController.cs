using AutoMapper;
using CarDetailingStudio.BLL;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.TireStorage.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using CarDetailingStudio.BLL.Services.TireFitting.Module.Contract;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

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
        private ITireRadius _tireRadius;
        private ICreateOrderModule _createOrder;
        private IAdditionalTireStorageServices _additionalTireStorageServices;
        private IOrderServicesCarWashServices _orderServicesCarWash;

        public TireStorageController(
            ITireStorage tireStorage,
            IStorageFee storageFee,
            IReviwOrderModules reviwOrder,
            IBrigadeForTodayServices brigadeForToday,
            IClientsOfCarWashServices clientsOfCarWash,
            IOrder order,
            IWageModules wage,
            ITireRadius tireRadius,
            ICreateOrderModule createOrder,
            IAdditionalTireStorageServices additionalTireStorageServices,
            IOrderServicesCarWashServices orderServicesCarWash)
        {
            _tireStorage = tireStorage;
            _storageFee = storageFee;
            _reviwOrder = reviwOrder;
            _brigadeForToday = brigadeForToday;
            _clientsOfCarWash = clientsOfCarWash;
            _order = order;
            _wage = wage;
            _tireRadius = tireRadius;
            _createOrder = createOrder;
            _additionalTireStorageServices = additionalTireStorageServices;
            _orderServicesCarWash = orderServicesCarWash;
        }

        // GET: TireStorage
        //public async Task<ActionResult> OrderTireStorage() =>
        //    View(Mapper.Map<IEnumerable<TireStorageView>>(await _tireStorage.SelectOrderTireStorage()));

        IEnumerable<AdditionalTireStorageServicesView> getAdditionalTireStorageServices;
        [HttpGet]
        public async Task<ActionResult> InfoOrderTireStorage(int? idOrder)
        {
            if (idOrder == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = Mapper.Map<OrderServicesCarWashView>(await _orderServicesCarWash.GetId(idOrder));
            var tireStorage = Mapper.Map<TireStorageView>(await _tireStorage.GetOrderId(idOrder.Value));

            if (tireStorage != null && tireStorage.RelatedOrders != null)
                getAdditionalTireStorageServices = Mapper.Map<IEnumerable<AdditionalTireStorageServicesView>>(await _additionalTireStorageServices.GetOrderId(tireStorage.RelatedOrders.Value));

            var client = Mapper.Map<ClientsOfCarWashView>(await _clientsOfCarWash.GetId(order.IdClientsOfCarWash));

            ViewBag.Order = order;
            ViewBag.AdditionalTireStorageServices = getAdditionalTireStorageServices;
            ViewBag.Client = client;

            DateTime dateClose = tireStorage.dateOfAdoption.Value;
            double? sum = 0;

            if (getAdditionalTireStorageServices != null)
                sum = getAdditionalTireStorageServices.Sum(x => x.Price);

            ViewBag.PaymentState = order.PaymentState;
            ViewBag.StatusOrder = order.StatusOrder;

            ViewBag.CloseDate = dateClose.AddMonths(tireStorage.storageFee.storageTime.Value);
            ViewBag.Sum = tireStorage.storageFee.amount + sum + tireStorage.serviceCostTirePackages;
            ViewBag.SumAdditional = sum + tireStorage.serviceCostTirePackages;

            if (tireStorage == null)
            {
                return HttpNotFound();
            }

            return View(tireStorage);
        }

        [HttpPost]
        public async Task<ActionResult> InfoOrderTireStorage(int? idOrder, int? idStorageFee, int? idTireStorage, int? idStatusOrder, int? idPaymentState)
        {
            await _orderServicesCarWash.CloseOrder(idOrder, idStatusOrder, idPaymentState);

            return RedirectToAction("OrderTireStorage", "Order", new RouteValueDictionary(new
            {
                typeOfOrder = 5,
                statusOrder = 5
            }));

        }

        public async Task<ActionResult> RenewalOfOrder(int? idOrder, int? idStatusOrder, int? idPaymentState, int? stockNumber, int? Client)
        {
            await _orderServicesCarWash.CloseOrder(idOrder, idStatusOrder, idPaymentState);

            return RedirectToAction("InfoOrderTireStorage", "Order", new RouteValueDictionary(new
            {
                IdClient = Client,
                stockNumber = stockNumber
            }));
        }

        // GET: TireStorage/Create
        public async Task<ActionResult> CreateTireStorageOrder(int? IdClient, int? stockNumber = null)
        {
            if (IdClient != null)
            {
                ClientsOfCarWashView clientList = Mapper.Map<ClientsOfCarWashView>(await _clientsOfCarWash.GetId(IdClient));
                ViewBag.Client = clientList;
                ViewBag.StockNumber = stockNumber;

                ViewBag.Radius = Mapper.Map<IEnumerable<TireRadiusView>>(await _tireRadius.GetTableAll()).ToList();
                TempData["Client"] = clientList;

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
                                                    int customer, int? StockNumber)
        {
            if (ModelState.IsValid)
            {
                tireStorageView.dateOfAdoption = DateTime.Now;
                tireStorageView.ClientId = customer;
                tireStorageView.stockNumber = StockNumber;

                TempData["TireStorageOrder"] = tireStorageView;

                if (TempData.ContainsKey("Client"))
                    TempData.Keep("Client");

                return RedirectToAction("ReviewTireStorageOrder");
            }

            return View(tireStorageView);
        }

        public async Task<ActionResult> ReviewTireStorageOrder()
        {
            if (TempData.ContainsKey("TireStorageOrder"))
            {
                OrderTireStorageModelView pageCreateResult = new OrderTireStorageModelView();
                ClientsOfCarWashView clientList = new ClientsOfCarWashView();

                if (TempData.ContainsKey("Client") && TempData.ContainsKey("TireStorageOrder"))
                {
                    pageCreateResult = TempData["TireStorageOrder"] as OrderTireStorageModelView;
                    TempData.Keep("TireStorageOrder");

                    clientList = TempData["Client"] as ClientsOfCarWashView;
                    TempData.Keep("Client");
                }

                OrderTireStorageModelBll tireStorageBll = Mapper.Map<OrderTireStorageModelView, OrderTireStorageModelBll>(pageCreateResult);

                var reviwOrderModules = Mapper.Map<ReviwOrderModelBll, ReviwOrderModelView>(await _reviwOrder.ReviwOrder(tireStorageBll));
                var brigadeForToday = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigadeForToday.GetDateTimeNow());

                ViewBag.Client = clientList;
                ViewBag.Adninistrator = brigadeForToday.Where(x => x.StatusId < 3);
                ViewBag.Brigade = brigadeForToday.Where(x => x.StatusId == 3);
                ViewBag.CarWashWorkers = new SelectList(await _brigadeForToday.GetDateTimeNow(), "id", "IdCarWashWorkers");
                ViewBag.ReviwOrder = reviwOrderModules;
                ViewBag.Services = reviwOrderModules.tireStorageServices;
                ViewBag.Sum = reviwOrderModules.priceDisk + reviwOrderModules.priceNumberOfPackets +
                    reviwOrderModules.priceOfTire + reviwOrderModules.priceSilicone + reviwOrderModules.priceWheelWash;

                TempData["ReviwOrderMode"] = reviwOrderModules;
                TempData.Keep("Client");

                return View(pageCreateResult);
            }

            return RedirectToAction("CreateTireStorageOrder");
        }

        int? idOrderServices;
        [HttpPost]
        public async Task<ActionResult> ReviewTireStorageOrder(double? sum, int? idPaymentState, int? idStatusOrder, int? StockNumber)
        {
            if (StockNumber != null)
            {
                if (TempData.ContainsKey("TireStorageOrder") && TempData.ContainsKey("ReviwOrderMode"))
                {
                    var pageCreateResult = TempData["TireStorageOrder"] as OrderTireStorageModelView;
                    var reviwOrderModules = TempData["ReviwOrderMode"] as ReviwOrderModelView;

                    OrderTireStorageModelBll orderTireStorage = Mapper.Map<OrderTireStorageModelView, OrderTireStorageModelBll>(pageCreateResult);
                    ReviwOrderModelBll orderModelBll = Mapper.Map<ReviwOrderModelView, ReviwOrderModelBll>(reviwOrderModules);

                    int idOrder = await _createOrder.SaveOrderTireFitting(reviwOrderModules.priceOfTire, reviwOrderModules.priceOfTire, idPaymentState.Value, idStatusOrder.Value, orderTireStorage.ClientId.Value, 5);
                    int idStorageFee = await _order.CreateStorageFee(orderTireStorage.storageTime.Value, sum);

                    if (reviwOrderModules.priceSilicone != 0 || reviwOrderModules.priceWheelWash != 0)
                    {
                        idOrderServices = await _createOrder.SaveOrderTireFitting(sum.Value, sum.Value, idPaymentState.Value, 1, orderTireStorage.ClientId.Value, 2);

                        if (reviwOrderModules.priceSilicone != 0)
                        {
                            var AdditionalTireStorageServicesEntity = new AdditionalTireStorageServicesView
                            {
                                clientsOfCarWashId = orderTireStorage.ClientId.Value,
                                orderServicesCarWashId = idOrderServices.Value,
                                tireStorageServicesd = orderModelBll.IdpriceSilicone,
                                Price = orderModelBll.priceSilicone
                            };

                            await _additionalTireStorageServices.Insert(TransformAnEntity(AdditionalTireStorageServicesEntity));
                        }

                        if (reviwOrderModules.priceWheelWash != 0)
                        {
                            var AdditionalTireStorageServicesEntity = new AdditionalTireStorageServicesView
                            {
                                clientsOfCarWashId = orderTireStorage.ClientId.Value,
                                orderServicesCarWashId = idOrderServices.Value,
                                tireStorageServicesd = orderModelBll.IdWheelWash,
                                Price = orderModelBll.priceWheelWash
                            };

                            await _additionalTireStorageServices.Insert(TransformAnEntity(AdditionalTireStorageServicesEntity));
                        }
                    }

                    var tireStorage = new TireStorageBll
                    {
                        serviceCostTirePackages = orderModelBll.priceNumberOfPackets,
                        dateOfAdoption = orderTireStorage.dateOfAdoption,
                        quantity = orderTireStorage.quantity,
                        radius = orderTireStorage.radius,
                        firm = orderTireStorage.firm,
                        discAvailability = orderTireStorage.discAvailability,
                        storageFeeId = idStorageFee,
                        tireStorageBags = orderTireStorage.tireStorageBags,
                        wheelWash = orderTireStorage.wheelWash,
                        IdOrderServicesCarWash = idOrder,
                        silicone = orderTireStorage.silicone,
                        RelatedOrders = idOrderServices,
                        stockNumber = StockNumber
                    };

                    await _tireStorage.Insert(tireStorage);
                    await _wage.AdminWageTireStorage(idOrder, pageCreateResult.quantity.Value);
                }

                return RedirectToAction("OrderTireStorage", "Order", new RouteValueDictionary(new
                {
                    typeOfOrder = 5,
                    statusOrder = 5
                }));
            }
            else
            {
                if (TempData.ContainsKey("Client"))
                    TempData.Keep("Client");

                if (TempData.ContainsKey("TireStorageOrder"))
                    TempData.Keep("TireStorageOrder");

                TempData.Keep("ReviwOrderMode");

                return RedirectToAction("ReviewTireStorageOrder");
            }
        }

        private AdditionalTireStorageServicesBll TransformAnEntity(AdditionalTireStorageServicesView entity) =>
                Mapper.Map<AdditionalTireStorageServicesView, AdditionalTireStorageServicesBll>(entity);
    }
}