using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Checkout;
using CarDetailingStudio.BLL.Services.Checkout.CheckoutContract;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.JoinModel.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Internal;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CarDetailingStudio.Controllers
{
    public class OrderCarpetWashingController : Controller
    {
        private IOrderCarpetWashingServices _orderCarpetWashingServices;
        private IBrigadeForTodayServices _brigadeForToday;
        private IDetailingsServises _detailings;
        private IOrder _order;
        private IWageModules _wageModules;
        private IClientInfoServices _clientInfo;
        private IClientJoinOrderCarpetWashing _orderCarpetWashing;
        private IOrderServicesCarWashServices _orderServicesCarWash;
        private IPaymentState _paymentState;

        public OrderCarpetWashingController(IOrderCarpetWashingServices orderCarpetWashing, IBrigadeForTodayServices brigadeForTodayServices,
                                            IDetailingsServises detailings, IOrder order, IWageModules wageModules, IClientInfoServices clientInfo,
                                            IClientJoinOrderCarpetWashing clientJoinOrderCarpetWashing, IOrderServicesCarWashServices orderServicesCarWash,
                                            IPaymentState paymentState)
        {
            _orderCarpetWashingServices = orderCarpetWashing;
            _brigadeForToday = brigadeForTodayServices;
            _detailings = detailings;
            _order = order;
            _wageModules = wageModules;
            _clientInfo = clientInfo;
            _orderCarpetWashing = clientJoinOrderCarpetWashing;
            _orderServicesCarWash = orderServicesCarWash;
            _paymentState = paymentState;
        }

        // GET: OrderCarpetWashing
        public async Task<ActionResult> OrderCarpetWashing()
        {
            var orderCarpet = Mapper.Map<IEnumerable<ClientJoinCarpetWashingModelView>>(await _orderCarpetWashing.ActiveOrdersWashingCarpets());
            return View(orderCarpet.OrderByDescending(x => x.idOrderCarpetWashing));
        }

        public async Task<ActionResult> ArxivOrderCarpet()
        {
            var orderCarpet = Mapper.Map<IEnumerable<ClientJoinCarpetWashingModelView>>(await _orderCarpetWashing.ActiveOrdersCarpets());
            return View(orderCarpet.OrderByDescending(x => x.idOrderCarpetWashing));
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
        public async Task<ActionResult> AddOrderCarpetWashing(int? idClient)
        {
            if (idClient == null)
            {
                return Redirect("/ClientsOfCarWashViews/ClientCarpetWashing");
            }

            var client = Mapper.Map<ClientInfoView>(await _clientInfo.ClientInfoGetId(idClient.Value));
            var brigadeForToday = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigadeForToday.GetDateTimeNow());
            var resultServisesPrice = Mapper.Map<DetailingsView>(await _detailings.GetId(87));

            TempData["priseServis"] = resultServisesPrice.S;

            ViewBag.Client = client;
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
        public async Task<ActionResult> AddOrderCarpetWashing([Bind(Include = "idOrderCarpetWashing,orderServicesCarWashId,clientId,area,orderDate,orderClosingDate,orderCompletionDate")] OrderCarpetWashingView orderCarpetWashingView,
                                                               List<string> idBrigade, int? idPaymentState, int? clientId, bool chooseEmployee = false)
        {
            if (chooseEmployee == true)
            {
                //if (ModelState.IsValid && idBrigade != null && TempData["priseServis"] != null && clientId != null)
                //{
                //    int priceServis = Convert.ToInt32(TempData["priseServis"].ToString());
                //    orderCarpetWashingView.clientId = clientId;

                //    OrderCarpetWashingBll orderCarpetWashing = Mapper.Map<OrderCarpetWashingView, OrderCarpetWashingBll>(orderCarpetWashingView);

                //    int resultOrderServicesCarWash = await _order.OrderForCarpetCleaning(orderCarpetWashing, idPaymentState, priceServis, clientId.Value);
                //    orderCarpetWashing.orderServicesCarWashId = resultOrderServicesCarWash;

                //    await _orderCarpetWashingServices.Insert(orderCarpetWashing);
                //    await _wageModules.Payroll(resultOrderServicesCarWash, idBrigade);

                //    return RedirectToAction("OrderCarpetWashing", "OrderCarpetWashing");
                //}
            }
            else
            {
                if (clientId != null && TempData["priseServis"] != null)
                {
                    int priceServis = Convert.ToInt32(TempData["priseServis"].ToString());
                    orderCarpetWashingView.clientId = clientId;

                    OrderCarpetWashingBll orderCarpetWashing = Mapper.Map<OrderCarpetWashingView, OrderCarpetWashingBll>(orderCarpetWashingView);

                    int resultOrderServicesCarWash = await _order.OrderForCarpetCleaning(orderCarpetWashing, idPaymentState, priceServis, clientId.Value);
                    orderCarpetWashing.orderServicesCarWashId = resultOrderServicesCarWash;

                    await _orderCarpetWashingServices.Insert(orderCarpetWashing);

                    return RedirectToAction("OrderCarpetWashing", "OrderCarpetWashing");
                }
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
        public async Task<ActionResult> AboutOrder(int? idOrder, int? idClient, int? idPage)
        {
            if (idClient == null && idOrder == null)
            {
                var httpstatusCode = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("OrderCarpetWashing");
            }

            OrderCarpetWashingView orderCarpetWashingView = Mapper.Map<OrderCarpetWashingView>(await _orderCarpetWashingServices.SelectId(idOrder));
            ClientInfoView clientInfoView = Mapper.Map<ClientInfoView>(await _clientInfo.ClientInfoGetId(idClient));

            var brigadeForToday = Mapper.Map<IEnumerable<BrigadeForTodayView>>(await _brigadeForToday.GetDateTimeNow());
            var orderServices = Mapper.Map<OrderServicesCarWashView>(await _orderServicesCarWash.GetId(orderCarpetWashingView.orderServicesCarWashId));
            var test = orderServices.OrderCarWashWorkers.FirstOrDefault(x => x.IdOrder == orderCarpetWashingView.orderServicesCarWashId);

            ViewBag.Adninistrator = brigadeForToday.Where(x => x.StatusId < 3);
            ViewBag.Brigade = brigadeForToday.Where(x => x.StatusId == 3);
            ViewBag.Sum = orderServices.DiscountPrice;
            ViewBag.BrigateOrder = test;
            ViewBag.ResultPay = orderServices.PaymentState;


            ViewBag.IdPage = idPage;
            ViewBag.ClientInfo = clientInfoView;

            var paymentSate = Mapper.Map<IEnumerable<PaymentStateView>>(await _paymentState.GetTableAll());
            ViewBag.PaymentState = new SelectList(paymentSate, "Id", "PaymentState1", orderServices.PaymentState);
            ViewBag.PaymentStateResult = orderServices.PaymentState;

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
        public async Task<ActionResult> AboutOrder([Bind(Include = "orderCompletionDate")] OrderCarpetWashingView orderCarpetWashingView, int idOrderServis,
                                                   int idOrder, int? idPage, int? PaymentStateList, List<string> idBrigade = null, int? idAdmin = null)
        {
            if (idPage == 2)
            {
                if (PaymentStateList == 3)
                {
                    if (idBrigade != null && idAdmin != null)
                    {
                        int idOrderServices = await EditOrderCarpetWashing(idOrder, idPage.Value, PaymentStateList.Value);
                        await _wageModules.Payroll(idOrderServices, idBrigade);
                        OrderServicesCarWashView orderServices = Mapper.Map<OrderServicesCarWashView>(await _orderServicesCarWash.GetId(idOrderServices));
                        orderServices.PaymentState = PaymentStateList;
                        orderServices.StatusOrder = 4;

                        if (orderServices.ClosingData == null)
                            orderServices.ClosingData = DateTime.Now;

                        await SaveOrderServices(orderServices);
                    }
                    else
                    {
                        int idOrderServices = await EditOrderCarpetWashing(idOrder, idPage.Value, PaymentStateList.Value);
                        OrderServicesCarWashView orderServices = Mapper.Map<OrderServicesCarWashView>(await _orderServicesCarWash.GetId(idOrderServices));
                        orderServices.PaymentState = PaymentStateList;
                        orderServices.StatusOrder = 4;

                        if (orderServices.ClosingData == null)
                            orderServices.ClosingData = DateTime.Now;

                        await SaveOrderServices(orderServices);
                    }
                }
                else if (PaymentStateList == 1 || PaymentStateList == 2)
                {
                    // if (idBrigade != null && idAdmin != null)
                    if (idBrigade != null)
                    {
                        int idOrderServices = await EditOrderCarpetWashing(idOrder, idPage.Value, PaymentStateList.Value);
                        await _wageModules.Payroll(idOrderServices, idBrigade);
                        OrderServicesCarWashView orderServices = Mapper.Map<OrderServicesCarWashView>(await _orderServicesCarWash.GetId(idOrderServices));
                        orderServices.PaymentState = PaymentStateList;
                        orderServices.StatusOrder = 2;

                        if (orderServices.ClosingData == null)
                            orderServices.ClosingData = DateTime.Now;

                        await SaveOrderServices(orderServices);
                    }
                    else
                    {
                        int idOrderServices = await EditOrderCarpetWashing(idOrder, idPage.Value, PaymentStateList.Value);
                        OrderServicesCarWashView orderServices = Mapper.Map<OrderServicesCarWashView>(await _orderServicesCarWash.GetId(idOrderServices));
                        orderServices.PaymentState = PaymentStateList;
                        orderServices.StatusOrder = 2;

                        if (orderServices.ClosingData == null)
                            orderServices.ClosingData = DateTime.Now;

                        await SaveOrderServices(orderServices);
                    }

                }
            }
          
            return RedirectToAction("OrderCarpetWashing");
                        
        }

        private async Task SaveOrderServices(OrderServicesCarWashView orderServices)
        {
            OrderServicesCarWashBll orderServicesBll = Mapper.Map<OrderServicesCarWashView, OrderServicesCarWashBll>(orderServices);
            await _orderServicesCarWash.SaveOrder(orderServicesBll);
        }

        private async Task<int> EditOrderCarpetWashing(int id, int idPage, int PaymentState)
        {
            OrderCarpetWashingView orderCarpetWashingGet = Mapper.Map<OrderCarpetWashingView>(await _orderCarpetWashingServices.SelectId(id));

            if (PaymentState == 3)
            {
                orderCarpetWashingGet.orderCompletionDate = DateTime.Now;
            }
            else
            {
                orderCarpetWashingGet.orderCompletionDate = DateTime.Now;
                orderCarpetWashingGet.orderClosingDate = DateTime.Now;
            }

            OrderCarpetWashingBll orderCarpet = Mapper.Map<OrderCarpetWashingView, OrderCarpetWashingBll>(orderCarpetWashingGet);
            await _orderCarpetWashingServices.Update(orderCarpet);

            return orderCarpetWashingGet.orderServicesCarWashId;
        }
    }
}
