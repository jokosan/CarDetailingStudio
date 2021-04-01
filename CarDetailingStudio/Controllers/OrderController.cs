using AutoMapper;
using CarDetailingStudio.BLL.EmployeesModules.Contract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.Enums;
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
        private readonly IOrderServicesCarWashServices _order;
        private readonly IServisesCarWashOrderServices _servisesCarWash;
        private readonly IBrigadeForTodayServices _brigade;
        private readonly IOrderServices _orderServices;
        private readonly IStatusOrder _statusOrder;
        private readonly IWageModules _wageModules;
        private readonly IOrderCarWashWorkersServices _orderCarWashWorkers;
        private readonly IAdditionalTireStorageServices _additionalTireStorageServices;
        private readonly IEmployeesFacade _employeesFacade;
        private readonly ITireChangeService _tireChangeService;
        private readonly ITireService _tireService;

        public OrderController(
            IOrderServicesCarWashServices orderServices,
            IServisesCarWashOrderServices servises,
            IBrigadeForTodayServices brigade,
            IOrderServices orderSer,
            IWageModules wageModules,
            IStatusOrder statusOrder,
            IOrderCarWashWorkersServices orderCarWashWorkers,
            IAdditionalTireStorageServices additionalTireStorageServices,
            IEmployeesFacade employeesFacade,
            ITireService tireService,
            ITireChangeService tireChangeService)
        {
            _order = orderServices;
            _servisesCarWash = servises;
            _brigade = brigade;
            _orderServices = orderSer;
            _wageModules = wageModules;
            _statusOrder = statusOrder;
            _orderCarWashWorkers = orderCarWashWorkers;
            _additionalTireStorageServices = additionalTireStorageServices;
            _employeesFacade = employeesFacade;
            _tireService = tireService;
            _tireChangeService = tireChangeService;
        }

        private double? Price;

        // GET: Order
        [WorkShiftFilter]
        [PreviousShiftStatusFilter]
        [MonitoringTheNumberOfEmployeesFilter]
        [InitialAmountFilter]
        public async Task<ActionResult> Index()
        {
            var order = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.GetAll(1));
            return View(order.OrderByDescending(x => x.Id));
        }

        public async Task<ActionResult> ArxivOrder(int? typeOfOrder, int? statusOrder, int? idClient)
        {
            if (typeOfOrder != null && statusOrder != null)
            {
                var arxivOrder = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.ArxivOrder(typeOfOrder.Value, statusOrder.Value));
                return View(arxivOrder.OrderByDescending(x => x.Id));
            }
            else
            {
                return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.AllCustomerOrders(idClient.Value)));
            }
               
        }

        public async Task<ActionResult> OrderTireStorage(int typeOfOrder, int statusOrder)
        {
            ViewBag.Services = typeOfOrder;
            return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(await _order.GetOrderAllTireStorage(typeOfOrder, statusOrder)).OrderByDescending(x => x.Id));
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
        public async Task<ActionResult> OrderDelete(int? idOrder)
        {
            await _order.DeleteOrder(idOrder.Value);
            return RedirectToAction("ArxivOrder");
        }

        public async Task<ActionResult> CompletedOrders(int? idOrder)
        {
            if (idOrder == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = Mapper.Map<OrderServicesCarWashView>(await _order.GetId(idOrder));
            var services = Mapper.Map<IEnumerable<ServisesCarWashOrderView>>(await _servisesCarWash.GetAllId(idOrder));
            var employee = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWashWorkers.GetTableInclud(idOrder));

            if (order.typeOfOrder == (int)TypeOfOrder.TireFitting)
            {
                ViewBag.TireService = Mapper.Map<IEnumerable<TireServiceView>>(await _tireService.SelectTireServices(idOrder.Value));
                ViewBag.TireChangeService = Mapper.Map<IEnumerable<TireChangeServiceView>>(await _tireChangeService.SelectTireService(idOrder.Value)); 
            }

            ViewBag.ServicesOrder = services;
            ViewBag.Employee = employee;

            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> StatusOrder([Bind(Include = "StatusOrder")] OrderServicesCarWashView orderServices, int? OrderStatus)
        {
            if (orderServices.StatusOrder <= 4 && OrderStatus != null)
            {
                await _order.StatusOrder(OrderStatus, orderServices.StatusOrder.Value);
                return RedirectToAction("Index");
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
        public async Task<ActionResult> CloseOrder(int? id, bool selectionStatus = true, bool AdminError = true)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Order = Mapper.Map<OrderServicesCarWashView>(await _order.GetId(id));
            var Services = Mapper.Map<IEnumerable<ServisesCarWashOrderView>>(await _servisesCarWash.GetAllId(id));

            ViewBag.Brigade = Mapper.Map<IEnumerable<CarWashWorkersView>>(await _employeesFacade.ListOfEmployeesForService(Order.typeOfOrder.Value));

            Price = Services.Sum(x => x.Price);
            var typeServese = Services.FirstOrDefault();

            ViewBag.Services = Services;
            ViewBag.CauntServices = Services.Count();

            if (AdminError == false)
            {
                ViewBag.AdminError = AdminError;
            }

            if (typeServese != null)
            {
                ViewBag.TypeService = typeServese.Detailings.IdTypeService;
            }
            else
            {
                ViewBag.TireServices = Mapper.Map<IEnumerable<AdditionalTireStorageServicesView>>(await _additionalTireStorageServices.GetOrderId(id.Value));
                ViewBag.TypeService = 2;
            }

            // ViewBag.Brigade = Brigade.Where(x => x.StatusId == 3);
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
        public async Task<ActionResult> CloseOrder(List<string> idBrigade, int idOrder, int idPaymentState, int idStatusOrder, int? typeServese)
        {
            //var resultServices = TempData["ServicesType"] as IEnumerable<ServisesCarWashOrderView>;

            var brigadeAdmin = await _brigade.AdminTrue(DateTime.Now, 2);

            if (brigadeAdmin)
            {
                if (idBrigade != null && idPaymentState != 3 && idStatusOrder == 2) //Выполнен
                {
                    await _wageModules.CloseOrder(idPaymentState, idOrder, idStatusOrder);
                    await _wageModules.Payroll(idOrder, idBrigade, typeServese.Value);
                }
                else if (idBrigade != null && idStatusOrder == 4) //Ожидает оплаты
                {
                    await _wageModules.CloseOrder(idPaymentState, idOrder, idStatusOrder);
                    await _wageModules.Payroll(idOrder, idBrigade, typeServese.Value);
                }
                else if (idStatusOrder == 5 && idPaymentState != 3)
                {
                    var StatusBrigade = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWashWorkers.GetTableInclud(idOrder));

                    if (StatusBrigade.Any(x => x.IdOrder == idOrder))
                    {
                        idStatusOrder = 2;
                        await _wageModules.CloseOrder(idPaymentState, idOrder, idStatusOrder);
                    }
                    else
                    {
                        return RedirectToAction("CloseOrder", "Order", new RouteValueDictionary(new
                        {
                            id = idOrder,
                            selectionStatus = false
                        }));
                    }

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
            }
            else
            {
                return RedirectToAction("CloseOrder", "Order", new RouteValueDictionary(new
                {
                    id = idOrder,
                    AdminError = false
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
