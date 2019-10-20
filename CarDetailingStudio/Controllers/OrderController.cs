using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;
using CarDetailingStudio.Filters;
using System.Threading.Tasks;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.BLL.Services.Contract;

namespace CarDetailingStudio.Controllers
{
    // [WorkShiftFilter]
    public class OrderController : Controller
    {
        private IOrderServicesCarWashServices _order;
        private IServisesCarWashOrderServices _servisesCarWash;
        private IBrigadeForTodayServices _brigade;
        private IOrderServices _orderServices;

        public OrderController(IOrderServicesCarWashServices orderServices, IServisesCarWashOrderServices servises, IBrigadeForTodayServices brigade, OrderServices orderSer)
        {
            _order = orderServices;
            _servisesCarWash = servises;
            _brigade = brigade;
            _orderServices = orderSer;
        }

        private double? Price;

        // GET: Order
        [WorkShiftFilter]
        [MonitoringTheNumberOfEmployeesFilter]
        public ActionResult Index()
        {
            var RedirectModel = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(_order.GetAll(1));
            return View(RedirectModel);
        }
              
        // GET: Order/Details/5
        public ActionResult OrderInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var OrderOnfo = Mapper.Map<OrderServicesCarWashView>(_order.GetId(id));
            var info = Mapper.Map<IEnumerable<ServisesCarWashOrderView>>(_servisesCarWash.GetAllId(id));

            ViewBag.ServisesInfo = info;

            if (OrderOnfo == null)
            {
                return HttpNotFound();
            }
            return View(OrderOnfo);
        }

        [HttpPost, ActionName("OrderInfo")]
        //[ValidateAntiForgeryToken]
        public ActionResult ServicesDelete( int idOrder, int idServices = 0)
        {
            if (idServices != 0)
            {
                _servisesCarWash.ServicesDelete(idServices, nameof(OrderController));
                _order.RecountOrder(idOrder);
                return RedirectToAction("OrderInfo");
            }
            else
            {
                _order.DeleteOrder(idOrder);
                return RedirectToAction("Index");
            }
        }
        
        [HttpGet]
        [WorkShiftFilter]
        [MonitoringTheNumberOfEmployeesFilter]
        public ActionResult CloseOrder(int? id)
        {
            if (id == null)
            {
                return  new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }                    

            var Order =  Mapper.Map<OrderServicesCarWashView>(_order.GetId(id));
            var Services = Mapper.Map<IEnumerable<ServisesCarWashOrderView>>(_servisesCarWash.GetAllId(id));
            var Brigade = Mapper.Map<IEnumerable<BrigadeForTodayView>>(_brigade.GetDateTimeNow());            

            Price = Services.Sum(x => x.Price);
                  
            ViewBag.Services = Services;
            ViewBag.Brigade = Brigade;
            ViewBag.Price = Price;

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
        public ActionResult CloseOrder(List<string> idBrigade, int idOrder, int idPaymentState)
        {
            if (idBrigade != null)
            {
                _order.CloseOrder(idPaymentState, idOrder, idBrigade);
            }
            else
            {
                return RedirectToAction("Index");
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

        public ActionResult OrderReport()
        {
            var OrderAll = Mapper.Map<IEnumerable<OrderServicesCarWashView>>(_order.GetAll(2));

            return View(OrderAll);
        }

        [HttpPost]
        public ActionResult OrderReport(DateTime startDate, DateTime finalDate)
        {
            var OrderWhere = Mapper.Map <IEnumerable<OrderServicesCarWashView>>(_order.OrderReport(startDate, finalDate));

            ViewBag.Date = $"Выборка заказов за период с {startDate.ToString("dd.MM.yyyy")} по {finalDate.ToString("dd.MM.yyyy")} ";
            return View(OrderWhere);
        }

     

        #region
        //// GET: Order/Details/5
        //public ActionResult Details(int? id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //OrderServicesCarWashView orderServicesCarWashView = db.OrderServicesCarWash.Find(id);
        //    //if (orderServicesCarWashView == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //return View(orderServicesCarWashView);
        //    throw new NotImplementedException();
        //}

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdClientsOfCarWash,IdServisesCarWashOrder,idCarWashWorkers,discount,StatusOrder,PaymentState,OrderDate,ExecutionTime,ClosingData,ClosingTime,TotalCostOfAllServices")] OrderServicesCarWashView orderServicesCarWashView)
        {
            //    if (ModelState.IsValid)
            //    {
            //        db.OrderServicesCarWash.Add(orderServicesCarWashView);
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }

            //    return View(orderServicesCarWashView);
            throw new NotImplementedException();
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OrderServicesCarWashView orderServicesCarWashView = db.OrderServicesCarWash.Find(id);
            //if (orderServicesCarWashView == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(orderServicesCarWashView);
            throw new NotImplementedException();
        }

        // POST: Order/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdClientsOfCarWash,IdServisesCarWashOrder,idCarWashWorkers,discount,StatusOrder,PaymentState,OrderDate,ExecutionTime,ClosingData,ClosingTime,TotalCostOfAllServices")] OrderServicesCarWashView orderServicesCarWashView)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(orderServicesCarWashView).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(orderServicesCarWashView);
            throw new NotImplementedException();
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OrderServicesCarWashView orderServicesCarWashView = db.OrderServicesCarWash.Find(id);
            //if (orderServicesCarWashView == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(orderServicesCarWashView);
            throw new NotImplementedException();
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //OrderServicesCarWashView orderServicesCarWashView = db.OrderServicesCarWash.Find(id);
            //db.OrderServicesCarWash.Remove(orderServicesCarWashView);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            throw new NotImplementedException();
        }

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
