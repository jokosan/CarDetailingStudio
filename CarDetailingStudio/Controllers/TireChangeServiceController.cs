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
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using AutoMapper;
using System.Security.Cryptography;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.TireFitting.Module.Contract;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using System.Web.Routing;
using System.Web.UI.WebControls;
using CarDetailingStudio.BLL.Services.TireFitting;

namespace CarDetailingStudio.Controllers
{
    public class TireChangeServiceController : Controller
    {
        private ITireChangeService _tireChangeService;
        private IPriceListTireFitting _priceListTireFitting;
        private IPriceTireFittingAdditionalServices _priceTireFittingAdditionalServices;
        private ITireRadius _tireRadius;
        private ITypeOfCars _typeOfCars;
        private IClientsOfCarWashServices _clientsOfCarWash;
        private ICreateOrderModule _createOrder;
        private IBrigadeForTodayServices _brigade;
        private IWageModules _wageModules;
        private IOrderServicesCarWashServices _orderServices;
        private ITireService _tireService;
        private IOrderCarWashWorkersServices _orderCarWash;

        private double discontClient;

        public TireChangeServiceController(
            ITireChangeService tireChangeService,
            IPriceListTireFitting priceListTireFitting,
            IClientsOfCarWashServices clientsOfCarWash,
            IPriceTireFittingAdditionalServices priceTireFittingAdditionalServices,
            ITireRadius tireRadius,
            ITypeOfCars typeOfCars,
            ICreateOrderModule createOrder,
            IBrigadeForTodayServices brigade,
            IWageModules wageModules,
            IOrderServicesCarWashServices orderServices,
            ITireService tireService,
            IOrderCarWashWorkersServices orderCarWash)
        {
            _tireChangeService = tireChangeService;
            _priceListTireFitting = priceListTireFitting;
            _priceTireFittingAdditionalServices = priceTireFittingAdditionalServices;
            _tireRadius = tireRadius;
            _typeOfCars = typeOfCars;
            _clientsOfCarWash = clientsOfCarWash;
            _createOrder = createOrder;
            _brigade = brigade;
            _wageModules = wageModules;
            _orderServices = orderServices;
            _tireService = tireService;
            _orderCarWash = orderCarWash;
        }

        //// GET: TireChangeService
        public async Task<ActionResult> CreateOrder(int IdClient, bool Error = false)
        {
            if (Error)
            {
                ViewBag.Error = Error;
            }


            var priceListTireFitting = Mapper.Map<IEnumerable<PriceListTireFittingView>>(await _priceListTireFitting.GetTableAll());
            ClientsOfCarWashView clientList = Mapper.Map<ClientsOfCarWashView>(await _clientsOfCarWash.GetId(IdClient));

            ViewBag.Client = clientList;
            ViewBag.TypeOfCars = Mapper.Map<IEnumerable<TypeOfCarsView>>(await _typeOfCars.GetTableAll());

            ViewBag.PriceListTireFittingOne = priceListTireFitting.Where(x => x.TypeOfCarsId == 1);
            ViewBag.PriceListTireFittingTwo = priceListTireFitting.Where(x => x.TypeOfCarsId == 2);

            ViewBag.PriceTireFittingAdditionalServices = Mapper.Map<IEnumerable<PriceListTireFittingAdditionalServicesView>>(await _priceTireFittingAdditionalServices.GetTableAll());

            ViewBag.RadiusOne = priceListTireFitting.Where(r => r.TypeOfCarsId == 1).GroupBy(x => x.TireRadiusId)
                                                 .Select(y => new TireRadiusView
                                                 {
                                                     idTireRadius = y.First().TireRadius.idTireRadius,
                                                     radius = y.First().TireRadius.radius
                                                 });

            ViewBag.RadiusTwo = priceListTireFitting.Where(r => r.TypeOfCarsId == 2).GroupBy(x => x.TireRadiusId)
                                               .Select(y => new TireRadiusView
                                               {
                                                   idTireRadius = y.First().TireRadius.idTireRadius,
                                                   radius = y.First().TireRadius.radius
                                               });

            TempData.Keep("CreateOrder");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrder(int? Client, int? NumberOfTires, List<int> TireRadius, List<int> Services,
                                        List<int> AdditionalServices, List<int> key, List<int> AdditionalServicesQuantity, int Type)
        {
            CreateOrderView createOrderView = new CreateOrderView();
                        
            if (Client != null && NumberOfTires != null && (TireRadius != null || Services != null))
            {
                if (Services != null)
                {
                    createOrderView.priceListTireFittings = Mapper.Map<IEnumerable<PriceListTireFittingView>>(await _priceListTireFitting.SelectValueFromThePriceList(Services)).ToList();
                }

                if (TireRadius != null)
                {
                    var resultRadius = Mapper.Map<IEnumerable<PriceListTireFittingView>>(await _priceListTireFitting.SelectRadius(TireRadius, Type)).ToList();

                    if (Services != null)
                        createOrderView.priceListTireFittings = (List<PriceListTireFittingView>)createOrderView.priceListTireFittings.Concat(resultRadius);
                    else
                        createOrderView.priceListTireFittings = resultRadius;
                }

                if (AdditionalServices != null)
                {
                    if (key.Count != 0 && AdditionalServicesQuantity.Count != 0)
                    {
                        var priceAdditionalServices = _createOrder.SaveOrder(key, AdditionalServicesQuantity);

                        createOrderView.priceListTireFittingAdditionals = Mapper.Map<List<PriceListTireFittingAdditionalServicesView>>(await _createOrder.SelectPriceServise(priceAdditionalServices, AdditionalServices));
                    }
                }

                createOrderView.client = Client;
                createOrderView.numberOfTires = NumberOfTires;

                TempData["CreateOrder"] = createOrderView;

                return RedirectToAction("OrderPreview");
            }

            return RedirectToAction("CreateOrder", "TireChangeService", new RouteValueDictionary(new
            {
                IdClient = Client,
                Error = true
            }));
        }


        [HttpGet]
        public async Task<ActionResult> OrderPreview()
        {
            CreateOrderView createOrderView = new CreateOrderView();

            if (TempData.ContainsKey("CreateOrder"))
            {
                createOrderView = (CreateOrderView)TempData["CreateOrder"];

                var brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigade.GetDateTimeNow());
                ViewBag.Brigade = brigade.Where(x => x.StatusId == 3);

                var client = Mapper.Map<ClientsOfCarWashView>(await _clientsOfCarWash.GetId(createOrderView.client));

                if (client.discont == null)
                {
                    discontClient = 0;
                }
                else
                {
                    discontClient = client.discont.Value;
                }

                ViewBag.Client = client;

                ViewBag.NumberOfTires = createOrderView.numberOfTires;
                ViewBag.PriceListTireFittings = createOrderView.priceListTireFittings;

                var priceListTireFittingsSum = SumTireChangeService(createOrderView.priceListTireFittings, createOrderView.numberOfTires.Value);

                double riceListTireFittingAdditionalsSum = TireServices(createOrderView.priceListTireFittingAdditionals);

                ViewBag.PriceListTireFittingsSum = priceListTireFittingsSum;

                double total = (priceListTireFittingsSum + riceListTireFittingAdditionalsSum) / 100 * discontClient;
                ViewBag.Total = priceListTireFittingsSum + riceListTireFittingAdditionalsSum - total;

                TempData.Keep("CreateOrder");
            }

            return View();
        }

        public double SumTireChangeService(List<PriceListTireFittingView> x, int y) => x.Sum(s => s.TheCost).Value * y;


        public double TireServices(List<PriceListTireFittingAdditionalServicesView> priceList)
        {
            if (priceList != null)
            {
                var riceListTireFittingAdditionalsSum = priceList.Sum(x => x.TheCost).Value;
                ViewBag.RiceListTireFittingAdditionalsSum = riceListTireFittingAdditionalsSum;
                ViewBag.riceListTireFittingAdditionals = priceList;

                return riceListTireFittingAdditionalsSum;
            }

            return 0;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OrderPreview(List<string> idBrigade, double Total, int idPaymentState, int idStatusOrder)
        {
            CreateOrderView createOrderView = new CreateOrderView();

            if (TempData.ContainsKey("CreateOrder"))
            {
                createOrderView = (CreateOrderView)TempData["CreateOrder"];

                double priceListTireFittingAdditionalsSum = 0;

                if (createOrderView.priceListTireFittingAdditionals != null)
                {
                    priceListTireFittingAdditionalsSum = createOrderView.priceListTireFittingAdditionals.Sum(x => x.TheCost).Value;
                }

                double TotalSum = SumTireChangeService(createOrderView.priceListTireFittings, createOrderView.numberOfTires.Value) + priceListTireFittingAdditionalsSum;

                int idOrder = await _createOrder.SaveOrderTireFitting(TotalSum, Total, idPaymentState, idStatusOrder, createOrderView.client.Value, 4);

                if (createOrderView.priceListTireFittingAdditionals != null)
                {
                    List<PriceListTireFittingAdditionalServicesBll> priceListTireFittings = new List<PriceListTireFittingAdditionalServicesBll>();
                    priceListTireFittings = Mapper.Map<List<PriceListTireFittingAdditionalServicesView>, List<PriceListTireFittingAdditionalServicesBll>>(createOrderView.priceListTireFittingAdditionals);

                    await _createOrder.SeveTireService(createOrderView.client.Value, idOrder, priceListTireFittings);
                }

                if (createOrderView.priceListTireFittings != null)
                {
                    List<PriceListTireFittingBll> listTireFittingBlls = new List<PriceListTireFittingBll>();
                    listTireFittingBlls = Mapper.Map<List<PriceListTireFittingView>, List<PriceListTireFittingBll>>(createOrderView.priceListTireFittings);

                    await _createOrder.SaveTireChangeService(idOrder, createOrderView.numberOfTires.Value, listTireFittingBlls);
                }

                if (idBrigade != null)
                {
                    await _wageModules.Payroll(idOrder, idBrigade, 5);
                }

                TempData.Remove("CreateOrder");

                return RedirectToAction("OrderTireStorage", "Order", new RouteValueDictionary(new
                {
                    typeOfOrder = 4,
                    statusOrder = 1
                }));
            }

            TempData.Keep("CreateOrder");

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CloseOrder(int OrderId)
        {
            var tireService = Mapper.Map<IEnumerable<TireServiceView>>(await _tireService.SelectTireServices(OrderId));
            var tireChangeService = Mapper.Map<IEnumerable<TireChangeServiceView>>(await _tireChangeService.SelectTireService(OrderId));
            var orderCarWash = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(await _orderCarWash.GetTableInclud(OrderId));
            var orderService = Mapper.Map<OrderServicesCarWashView>(await _orderServices.GetId(OrderId));
            var client = Mapper.Map<ClientsOfCarWashView>(await _clientsOfCarWash.GetId(orderService.IdClientsOfCarWash));

            double tireServiceSum = 0;
            double tireChangeServiceSum = 0;

            foreach (var item in tireChangeService)
            {
                tireChangeServiceSum += item.price.Value * item.numberOfTires.Value;
            }
            
            if (orderCarWash == null || orderCarWash.Count() == 0)
            {
                var brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigade.GetDateTimeNow());
                ViewBag.Brigade = brigade;
                ViewBag.BrigadeOrder = 0;
            }
            else
            {
                ViewBag.Brigade = orderCarWash;
                ViewBag.BrigadeOrder = 1;
            }

            if (tireService != null)
            {
                tireServiceSum = tireService.Sum(x => x.priceTireFitting).Value;
                ViewBag.TireService = tireService;
                ViewBag.TireServiceSum = tireServiceSum;
            }

            ViewBag.TireChangeServiceSum = tireChangeServiceSum;
            ViewBag.TireChangeService = tireChangeService;
            ViewBag.OrderService = orderService;
            ViewBag.OrderServiceID = OrderId;
            ViewBag.Client = client;
            ViewBag.Discont = client.discont;

            double totalDiscont = ((tireChangeServiceSum + tireServiceSum) / 100) * client.discont.Value;
            ViewBag.Total = (tireChangeServiceSum + tireServiceSum) - totalDiscont;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CloseOrder(List<string> idBrigade, int BrigadeOrder, int? idPaymentState, int? idStatusOrder, int? idOrder)
        {


            if (BrigadeOrder == 0 && idBrigade != null)
            {
                if (idStatusOrder != 1 && idPaymentState != 3 || idStatusOrder == 4 && idPaymentState == 3)
                {
                    await _wageModules.CloseOrder(idPaymentState.Value, idOrder.Value, idStatusOrder.Value);
                    await _wageModules.Payroll(idOrder.Value, idBrigade, 5);
                }
               

                return RedirectToAction("OrderTireStorage", "Order", new RouteValueDictionary(new
                {
                    typeOfOrder = 4,
                    statusOrder = 1
                }));
            }
            else if ( BrigadeOrder == 1)
            {
                if (idStatusOrder != 1 && idPaymentState != 3 || idStatusOrder == 4 && idPaymentState == 3)
                {
                    await _wageModules.CloseOrder(idPaymentState.Value, idOrder.Value, idStatusOrder.Value);
                }
                   
                return RedirectToAction("OrderTireStorage", "Order", new RouteValueDictionary(new
                {
                    typeOfOrder = 4,
                    statusOrder = 1
                }));
            }

            return View();
        }


    }
}
