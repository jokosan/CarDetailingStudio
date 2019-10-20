using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Filters;

namespace CarDetailingStudio.Controllers
{
    public class ClientsOfCarWashViewsController : Controller
    {
        private IClientsOfCarWashServices _services;
        private IDetailingsServises _detailings;
        private IOrderServices _orderServices;
        private IOrderServicesCarWashServices _orderServicesInsert;

        public ClientsOfCarWashViewsController(IClientsOfCarWashServices clients, IDetailingsServises detailingsView, IOrderServices order, IOrderServicesCarWashServices orderServices)
        {
            _services = clients;
            _detailings = detailingsView;
            _orderServices = order;
            _orderServicesInsert = orderServices;

        }

        IEnumerable<DetailingsView> Price { get; set; }

        // GET: ClientsOfCarWashViews
        public ActionResult Client()
        {
            var RedirectModel = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(_services.GetAll());
            return View(RedirectModel);
        }

        public ActionResult Checkout()
        {
            var RedirectModel = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(_services.GetAll());
            return View(RedirectModel);
        }

        // GET: ClientsOfCarWashViews/Details/5
        public ActionResult NewOrder(int? id, string body)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _orderServices.ClearListOrder();

            OrderServices.idClient = id;
            OrderServices.body = body;

            var CustomerOrders = Mapper.Map<ClientsOfCarWashView>(_services.GetId(id));

            Price = Mapper.Map<IEnumerable<DetailingsView>>(_detailings.Converter());

            ViewBag.Detailings = Price.Where(x => x.IdTypeService == 1);
            ViewBag.WashServises = Price.Where(x => x.IdTypeService == 2);

            if (CustomerOrders == null)
            {
                return HttpNotFound();
            }

            return View(CustomerOrders);
        }

        [HttpPost]
        [WorkShiftFilter]
        public ActionResult NewOrder(FormCollection form)
        {
            _orderServices.IdOrderServices(form);

            _orderServices.OrderPreview();

            return RedirectToRoute(new { controller = "ClientsOfCarWashViews", action = "OrderPreview" });
        }

 
        public ActionResult OrderPreview()
        {
            var CustomerOrders = Mapper.Map<ClientsOfCarWashView>(_services.GetId(OrderServices.idClient));

            var sum = _orderServices.OrderPrice();

            ViewBag.PriceList = OrderServices.OrderList;
            ViewBag.SumOrder = sum;

            if (CustomerOrders.discont > 0)
            {
                ViewBag.Total = _orderServices.Discont(CustomerOrders.discont, sum);
            }
            else
            {
                ViewBag.Total = sum;
            }

            return View(CustomerOrders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPreview(List<double> carBody, List<int> id, List<int> sum)
        {
            _orderServicesInsert.InsertOrders(carBody, id, sum);

            return RedirectToAction("Index", "Order");
        }

        // GET: ClientsOfCarWashViews/Create
        public ActionResult AddClient(string id)
        {
            
            ViewBag.NewAction = id;

            return View();
        }      

        // POST: ClientsOfCarWashViews/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClient([Bind(Include = "ib,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumderCar,IdClientsGroups,Idmark,Idmodel,IdBody,note,barcode")] ClientsOfCarWashView clientsOfCarWashView, bool AddClient)
        {

            if (ModelState.IsValid)
            {
                ClientsOfCarWashBll clientsOfCarWash = Mapper.Map<ClientsOfCarWashView, ClientsOfCarWashBll>(clientsOfCarWashView);
                _services.Insert(clientsOfCarWash);

                if(AddClient)
                {
                    return RedirectToAction("AddClient");
                }
                else
                {
                    return RedirectToAction("NewOrder");
                }
                
            }

            return View(clientsOfCarWashView);
        }

        #region
        //// GET: ClientsOfCarWashViews/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClientsOfCarWashView clientsOfCarWashView = db.ClientsOfCarWash.Find(id);
        //    if (clientsOfCarWashView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clientsOfCarWashView);
        //}

        //// GET: ClientsOfCarWashViews/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ClientsOfCarWashViews/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ib,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumderCar,IdClientsGroups,Idmark,Idmodel,IdBody,note,barcode")] ClientsOfCarWashView clientsOfCarWashView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ClientsOfCarWash.Add(clientsOfCarWashView);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(clientsOfCarWashView);
        //}

        //// GET: ClientsOfCarWashViews/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClientsOfCarWashView clientsOfCarWashView = db.ClientsOfCarWash.Find(id);
        //    if (clientsOfCarWashView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clientsOfCarWashView);
        //}

        //// POST: ClientsOfCarWashViews/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ib,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumderCar,IdClientsGroups,Idmark,Idmodel,IdBody,note,barcode")] ClientsOfCarWashView clientsOfCarWashView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(clientsOfCarWashView).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(clientsOfCarWashView);
        //}

        //// GET: ClientsOfCarWashViews/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClientsOfCarWashView clientsOfCarWashView = db.ClientsOfCarWash.Find(id);
        //    if (clientsOfCarWashView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clientsOfCarWashView);
        //}

        //// POST: ClientsOfCarWashViews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ClientsOfCarWashView clientsOfCarWashView = db.ClientsOfCarWash.Find(id);
        //    db.ClientsOfCarWash.Remove(clientsOfCarWashView);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        #endregion
    }
}
