using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.Filters;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
    public class ClientsOfCarWashViewsController : Controller
    {
        private IClientsOfCarWashServices _services;
        private IDetailingsServises _detailings;
        private ICarBodyServices _carBody;
        private IOrderServices _orderServices;
        private IOrderServicesCarWashServices _orderServicesInsert;
        private ICarMarkServices _carMark;
        private IClientsGroupsServices _clientsGroups;
        private IClientInfoServices _clientInfo;
        private IGroupWashServices _groupWashServices;
        private IBrigadeForTodayServices _brigade;
        private IServisesCarWashOrderServices _servisesCarWash;


        public ClientsOfCarWashViewsController(IClientsOfCarWashServices clients, IDetailingsServises detailingsView,
                                               IOrderServices order, IOrderServicesCarWashServices orderServices,
                                               ICarMarkServices carMark, ICarBodyServices carBody, IClientsGroupsServices clientsGroupsServices,
                                               IClientInfoServices clientInfo, IGroupWashServices groupWashServices, IBrigadeForTodayServices brigade,
                                                IServisesCarWashOrderServices servisesCarWash)
        {
            _services = clients;
            _detailings = detailingsView;
            _orderServices = order;
            _orderServicesInsert = orderServices;
            _carMark = carMark;
            _carBody = carBody;
            _clientsGroups = clientsGroupsServices;
            _clientInfo = clientInfo;
            _groupWashServices = groupWashServices;
            _brigade = brigade;
            _servisesCarWash = servisesCarWash;
        }

        IEnumerable<DetailingsView> Price { get; set; }
        private int? ServicesId;

        // GET: ClientsOfCarWashViews
        //public ActionResult Client(string search, string cansel, int? i)
        //{
        //    var RedirectModel = ClientSearch(search, cansel);
        //    return View(RedirectModel.ToList().ToPagedList(i ?? 1, 15));
        //}

        public ActionResult Client(int idPaymentState = 1)
        {
            if (idPaymentState == 1)
            {
                return View(ClientSearch());
            }
            else
            {
                return RedirectToAction("ClientInfo", "ClientsOfCarWashViews", new RouteValueDictionary(new { idPaymentState = 2 }));
            }
        }

        public ActionResult ClientInfo(int idPaymentState)
        {
            if (idPaymentState == 2)
            {
                return View(Mapper.Map<IEnumerable<ClientInfoView>>(_clientInfo.ClientInfoAll().Where(x => x.DateRegistration != null)));
            }
            else
            {
                return RedirectToAction("Client", "ClientsOfCarWashViews", new RouteValueDictionary(new { idPaymentState = 1 }));
            }
        }

        public ActionResult Checkout(int? services)
        {

            if (services != null)
            {
                ServicesId = services;
                ViewBag.Service = ServicesId;

                return View(ClientSearch());
            }

            return Redirect("/Order/Index");
        }

        private IEnumerable<ClientsOfCarWashView> ClientSearch()
        {
            return Mapper.Map<IEnumerable<ClientsOfCarWashView>>(_services.GetAll());
        }

        // GET: ClientsOfCarWashViews/Details/5
        public ActionResult NewOrder(int? id, string body, int? Services, int? idOrderServices = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (idOrderServices != null)
            {
                TempData["OrderServices"] = idOrderServices;
            }

            _orderServices.ClearListOrder();

            OrderServices.idClient = id;
            OrderServices.body = body;

            var CustomerOrders = Mapper.Map<ClientsOfCarWashView>(_services.GetId(id));

            Price = Mapper.Map<IEnumerable<DetailingsView>>(_detailings.Converter());

            var tablePriceResult = Price.Where(x => x.IdTypeService == Services);

            ViewBag.Detailings = tablePriceResult;
            ViewBag.DetailingsGrup = _groupWashServices.GetIdAll(Services);
            //ViewBag.WashServises = Price.Where(x => x.IdTypeService == 2);

            if (CustomerOrders == null)
            {
                return HttpNotFound();
            }

            return View(CustomerOrders);
        }

        [HttpPost]
        [WorkShiftFilter]
        public ActionResult NewOrder(FormCollection form, List<int> chkRow, List<int> countService)
        {
            _orderServices.IdOrderServices(chkRow);

            _orderServices.OrderPreview();
            return RedirectToRoute(new { controller = "ClientsOfCarWashViews", action = "OrderPreview" });
        }

        public ActionResult OrderPreview()
        {
            if (OrderServices.OrderList.Count() > 0)
            {
                double orederSum = 0;
                if (TempData.ContainsKey("OrderServices"))
                {
                    var orderServisesResult = Mapper.Map<IEnumerable<ServisesCarWashOrderView>>(_servisesCarWash.GetAllId(int.Parse(TempData["OrderServices"].ToString())));
                    orederSum = orderServisesResult.Sum(x => x.Price).Value;

                    ViewBag.OrderServices = orderServisesResult;
                    ViewBag.idOrder = int.Parse(TempData["OrderServices"].ToString());

                }

                var CustomerOrders = Mapper.Map<ClientsOfCarWashView>(_services.GetId(OrderServices.idClient));

                var sum = _orderServices.OrderPrice();
                var sumResult = sum + orederSum;

                ViewBag.PriceList = OrderServices.OrderList;
                ViewBag.SumOrder = sumResult;
                ViewBag.Brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(_brigade.GetDateTimeNow().Where(x => x.StatusId == 3));

                if (CustomerOrders.discont > 0)
                {
                    ViewBag.Total = _orderServices.Discont(CustomerOrders.discont, sumResult);
                }
                else
                {
                    ViewBag.Total = sumResult;
                }

                return View(CustomerOrders);
            }

            return RedirectToAction("Checkout");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPreview(List<double> carBody, List<int> id, List<int> sum, List<string> idBrigade, double total, int? idOrder = null)
        {
            if (idOrder == null)
            {
                _orderServicesInsert.InsertOrders(carBody, id, sum, total);
            }
            else
            {
                _orderServicesInsert.InsertOrders(carBody, id, sum, total, idOrder);
            }

            return RedirectToAction("Index", "Order");
        }

        public ActionResult EditClient(int? id, int? idCar)
        {
            if (id == null && idCar == null)
            {
                var infoLog = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Client");
            }

            ClientInfoView clientInfo = Mapper.Map<ClientInfoView>(_clientInfo.ClientInfoGetId(id));

            ViewBag.Group = new SelectList(_clientsGroups.GetClientsGroups(), "Id", "Name");
            ViewBag.IdClient = id;
            ViewBag.IdCar = idCar;

            if (clientInfo == null)
            {
                return HttpNotFound();
            }

            return View(clientInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient([Bind(Include = "id,Surname,Name,PatronymicName,Phone,DateRegistration,Email,barcode,note")] ClientInfoView client, int IdClient, int idCar, int? SelectGroupClient)
        {
            if (ModelState.IsValid)
            {
                ClientInfoView clientInfo = Mapper.Map<ClientInfoView>(_clientInfo.ClientInfoGetId(IdClient));
                clientInfo = client;

                ClientInfoBll clientInfoBll = Mapper.Map<ClientInfoView, ClientInfoBll>(client);
                var clientsOfCarWashView = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(_services.GetAll(idCar));

                foreach (var item in clientsOfCarWashView)
                {
                    item.IdClientsGroups = SelectGroupClient;

                    ClientsOfCarWashBll clients = Mapper.Map<ClientsOfCarWashView, ClientsOfCarWashBll>(item);
                    _services.ClientCarUpdate(clients);
                }
                _clientInfo.ClientInfoEdit(clientInfoBll);

                return RedirectToAction("Info", "ClientsOfCarWashViews", new RouteValueDictionary(new
                {
                    idClientInfo = IdClient,
                    idClient = idCar
                }));
            }

            ViewBag.Group = new SelectList(_clientsGroups.GetClientsGroups(), "Id", "Name");
            return View(client);
        }

        // GET: ClientsOfCarWashViews/Edit/5
        public ActionResult EditCarClient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientsOfCarWashView clientsOfCarWashView = Mapper.Map<ClientsOfCarWashView>(_services.GetId(id));

            if (clientsOfCarWashView == null)
            {
                return HttpNotFound();
            }

            ViewBag.Body = new SelectList(_carBody.GetTableAll(), "Id", "Name");
            ViewBag.Group = new SelectList(_clientsGroups.GetClientsGroups(), "Id", "Name");
            ViewBag.IdCar = id;
            return View(clientsOfCarWashView);
        }

        // POST: ClientsOfCarWashViews/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCarClient([Bind(Include = "id,IdClientsGroups,IdMark,IdModel,IdBody,IdInfoClient,NumberCar,discont")] ClientsOfCarWashView clientsOfCarWashView, int idCar)
        {
            if (ModelState.IsValid)
            {
                ClientsOfCarWashView clientsOfCarWash = Mapper.Map<ClientsOfCarWashView>(_services.GetId(idCar));
                //clientsOfCarWash = clientsOfCarWashView;

                clientsOfCarWashView.id = clientsOfCarWash.id;
                clientsOfCarWashView.IdInfoClient = clientsOfCarWash.IdInfoClient;
                clientsOfCarWashView.IdMark = clientsOfCarWash.IdMark;
                clientsOfCarWashView.IdModel = clientsOfCarWash.IdModel;
                clientsOfCarWashView.IdClientsGroups = clientsOfCarWash.IdClientsGroups;
                clientsOfCarWashView.arxiv = clientsOfCarWash.arxiv;

                ClientsOfCarWashBll clients = Mapper.Map<ClientsOfCarWashView, ClientsOfCarWashBll>(clientsOfCarWashView);
                _services.ClientCarUpdate(clients);

                return RedirectToAction("Info", "ClientsOfCarWashViews", new RouteValueDictionary(new
                {
                    idClientInfo = clientsOfCarWash.IdInfoClient,
                    idClient = clientsOfCarWash.id
                }));
            }

            ViewBag.Body = new SelectList(_carBody.GetTableAll(), "Id", "Name");
            ViewBag.Group = new SelectList(_clientsGroups.GetClientsGroups(), "Id", "Name");

            return View(clientsOfCarWashView);
        }

        public ActionResult AddCar(int? IdInfoClient, int? ClientsGroups)
        {
            if (IdInfoClient == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<int> client = new List<int>() { IdInfoClient.Value, ClientsGroups.Value };
            TempData["IdClient"] = client;

            ViewBag.Body = new SelectList(_carBody.GetTableAll(), "Id", "Name");
            ViewBag.Group = new SelectList(_clientsGroups.GetClientsGroups(), "Id", "Name");

            return View();
        }

        // POST: ClientsOfCarWashViews/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar([Bind(Include = "id,IdClientsGroups,IdMark,IdModel,IdBody,IdInfoClient,NumberCar,discont")] ClientsOfCarWashView clientsOfCarWashView)
        {
            if (TempData.ContainsKey("Mark") == true && TempData.ContainsKey("Model") == true)
            {
                clientsOfCarWashView.IdMark = Int32.Parse(TempData["Mark"].ToString());
                clientsOfCarWashView.IdModel = Int32.Parse(TempData["Model"].ToString());

                var itemList = TempData["IdClient"] as List<int>;

                clientsOfCarWashView.IdInfoClient = itemList[0];
                clientsOfCarWashView.IdClientsGroups = itemList[1];
                clientsOfCarWashView.arxiv = true;

                if (ModelState.IsValid)
                {
                    ClientsOfCarWashBll clients = Mapper.Map<ClientsOfCarWashView, ClientsOfCarWashBll>(clientsOfCarWashView);
                    _services.Insert(clients);

                    return RedirectToAction("Client");
                }
            }

            ViewBag.Body = new SelectList(_carBody.GetTableAll(), "Id", "Name");
            ViewBag.Group = new SelectList(_clientsGroups.GetClientsGroups(), "Id", "Name");

            return View(clientsOfCarWashView);
        }

        // GET: ClientsOfCarWashViews/Details/5    
        public ActionResult Info(int? idClientInfo, int? idClient, bool? statusPage = true)
        {
            if (idClient == null && idClientInfo == null)
            {
                var logo = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Client");
            }

            if (statusPage != null)
                ViewBag.Page = statusPage;

            //ClientsOfCarWashView clientsOfCarWashView = Mapper.Map<ClientsOfCarWashView>(_services.GetId(idClient));
            var ClientWhare = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(_services.GetAll(idClientInfo));
            var singlClien = ClientWhare.FirstOrDefault(x => x.IdInfoClient == idClientInfo);

            ClientsOfCarWashView clientsOfCarWashView = Mapper.Map<ClientsOfCarWashView>(_services.GetId(singlClien.id));

            if (idClient != null && idClientInfo != null)
            {
                ViewBag.ClientInfo = ClientWhare.Where(x => x.arxiv == true);
            }
            else if (idClient == null && idClientInfo != null)
            {
                ViewBag.ClientInfo = ClientWhare;
            }


            if (clientsOfCarWashView == null)
            {
                return HttpNotFound();
            }

            return View(clientsOfCarWashView);
        }

        [HttpPost]
        public ActionResult CarArxiv(int carId)
        {
            _services.ClientCarArxiv(carId, false);
            return RedirectToAction("Info");
        }

        [HttpPost]
        public ActionResult ReturnFromArchive(int carId)
        {
            _services.ClientCarArxiv(carId, true);
            return RedirectToAction("Info");
        }

        [HttpPost]
        public ActionResult RemoveClient(int ClientId)
        {
            _services.RemoveClient(ClientId);
            return RedirectToAction("Info");
        }
    }
}
