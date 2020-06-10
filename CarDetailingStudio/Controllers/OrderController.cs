using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using CarDetailingStudio.Filters;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
    // [WorkShiftFilter]
    public class OrderController : Controller
    {
        private IOrderServicesCarWashServices _order;
        private IServisesCarWashOrderServices _servisesCarWash;
        private IBrigadeForTodayServices _brigade;
        private IOrderServices _orderServices;
        private IStatusOrder _statusOrder;
        private IWageModules _wageModules;

        public OrderController(IOrderServicesCarWashServices orderServices, IServisesCarWashOrderServices servises, IBrigadeForTodayServices brigade,
                               IOrderServices orderSer, IWageModules wageModules, IStatusOrder statusOrder)
        {
            _order = orderServices;
            _servisesCarWash = servises;
            _brigade = brigade;
            _orderServices = orderSer;
            _wageModules = wageModules;
            _statusOrder = statusOrder;
        }

        private double? Price;

        // GET: Order
        [WorkShiftFilter]
        [PreviousShiftStatusFilter]
        [MonitoringTheNumberOfEmployeesFilter]
        public async Task<ActionResult> Index()
        {
            var order = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.GetAll(1));
            return View(order.OrderByDescending(x => x.Id));
        }

        public async Task<ActionResult> ArxivOrder()
        {
            return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.GetAll(2, 1)));
        }

        public async Task<ActionResult> OrderTireStorage()
        {
            return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.GetOrderAllTireStorage()));
        }

        // GET: Order/Details/5
        public async Task<ActionResult> OrderInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var OrderInfo = Mapper.Map<OrderServicesCarWashView>(await _order.GetId(id));
            var info = Mapper.Map<IEnumerable<ServisesCarWashOrderView>>(await _servisesCarWash.GetAllId(id));

            ViewBag.ServisesInfo = info;
            ViewBag.DiscontClient = OrderInfo.ClientsOfCarWash.discont;
            ViewBag.Status = new SelectList(Mapper.Map<IEnumerable<StatusOrderView>>(await _statusOrder.GetTableAll()), "Id", "StatusOrder1");

            if (info.Any(x => x.Detailings.IdTypeService == 1))
            {
                ViewBag.ServisesId = 1;
                ViewBag.ServersKey = id;
            }
            else
            {
                ViewBag.ServisesId = 2;
                ViewBag.ServersKey = id;
            }


            if (OrderInfo == null)
            {
                return HttpNotFound();
            }

            if (TempData.ContainsKey("PageSettings"))
            {
                ViewBag.Brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigade.GetDateTimeNow());
            }

            return View(OrderInfo);
        }

        [HttpPost, ActionName("OrderInfo")]
        public async Task<ActionResult> ServicesDelete(int? idOrder, int? discont, int idServices = 0)
        {
            if (idServices != 0)
            {
                await _servisesCarWash.ServicesDelete(idServices, nameof(OrderController));
                await _order.RecountOrder(idOrder.Value, discont);
                return RedirectToAction("OrderInfo");
            }
            else
            {
                await _order.DeleteOrder(idOrder.Value);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> StatusOrder([Bind(Include = "StatusOrder")] OrderServicesCarWashView orderServices, int? OrderStatus)
        {
            if (orderServices.StatusOrder == 4 && OrderStatus != null)
            {
                await _order.StatusOrder(OrderStatus, orderServices.StatusOrder.Value);
                return View();
            }
            else if (orderServices.StatusOrder == 3 && OrderStatus != null)
            {
                await _order.DeleteOrder(OrderStatus.Value);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [WorkShiftFilter]
        [MonitoringTheNumberOfEmployeesFilter]
        public async Task<ActionResult> CloseOrder(int? id, bool selectionStatus = true)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Order = Mapper.Map<OrderServicesCarWashView>(await _order.GetId(id));
            var Services = Mapper.Map<IEnumerable<ServisesCarWashOrderView>>(await _servisesCarWash.GetAllId(id));
            var Brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigade.GetDateTimeNow());

            Price = Services.Sum(x => x.Price);

            ViewBag.Services = Services;
            ViewBag.Brigade = Brigade.Where(x => x.StatusId == 3);
            ViewBag.Price = Price;

            if (selectionStatus == false)
            {
                ViewBag.SelectionStatus = selectionStatus;
            }

            TempData["ServicesType"] = Services;

            if (Order.ClientsOfCarWash.discont > 0)
            {
                ViewBag.Total = _orderServices.Discont(Order.ClientsOfCarWash.discont, Price);
            }
            else
            {
                ViewBag.Total = Price;
            }

            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        [HttpPost]
        public async Task<ActionResult> CloseOrder(List<string> idBrigade, int idOrder, int idPaymentState, int idStatusOrder)
        {
            //var resultServices = TempData["ServicesType"] as IEnumerable<ServisesCarWashOrderView>;

            if (idBrigade != null && idPaymentState != 3 && idStatusOrder == 2)
            {
                await _wageModules.CloseOrder(idPaymentState, idOrder, idStatusOrder);
                await _wageModules.Payroll(idOrder, idBrigade);
            }
            else if (idBrigade != null && idPaymentState == 3 && idStatusOrder == 4)
            {
                await _wageModules.CloseOrder(idPaymentState, idOrder, idStatusOrder);
                await _wageModules.Payroll(idOrder, idBrigade);
            }
            else if (idStatusOrder == 2 && idPaymentState != 3)
            {
                await _wageModules.CloseOrder(idPaymentState, idOrder, idStatusOrder);
            }
            else if (idStatusOrder == 3)
            {
                await _wageModules.CloseOrder(idPaymentState, idOrder, idStatusOrder);
            }
            else
            {
                // return RedirectToAction("Index");
                return RedirectToAction("CloseOrder", "Order", new RouteValueDictionary(new
                {
                    id = idOrder,
                    selectionStatus = false
                }));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ReturnTheDifference(double prise, double customerAmount)
        {
            if (customerAmount > prise)
                return View(customerAmount - prise);

            return View();
        }

        public async Task<ActionResult> OrderReport()
        {
            var OrderAll = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.GetAll(2));
            //TempData["PageSettings"] = nameof(OrderReport);

            return View(OrderAll);
        }

        [HttpPost]
        public async Task<ActionResult> OrderReport(DateTime startDate, DateTime finalDate)
        {
            var OrderWhere = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.OrderReport(startDate, finalDate));

            ViewBag.Date = $"Выборка заказов за период с {startDate.ToString("dd.MM.yyyy")} по {finalDate.ToString("dd.MM.yyyy")} ";
            return View(OrderWhere);
        }
    }
}
