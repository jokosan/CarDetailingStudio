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
using CarDetailingStudio.BLL.Services.Modules;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;

namespace CarDetailingStudio.Controllers
{
    public class ItogOrderController : Controller
    {
        private ICloseShiftModule _closeShiftModule;
        private IDayResult _dayResult;
        private IOrderServicesCarWashServices _orderServices;

        public ItogOrderController(IDayResult dayResult, ICloseShiftModule closeShiftModule,
                                    IOrderServicesCarWashServices orderServices, IOrderCarWashWorkersServices orderCarWashWorkers)
        {
            _dayResult = dayResult;
            _closeShiftModule = closeShiftModule;
            _orderServices = orderServices;

        }

        // GET: OrderInfoViewModels
        public ActionResult DayResult()
        {
            var ItogSalary = Mapper.Map<IEnumerable<DayResultModelView>>(_dayResult.DayResultViewInfo());
            var ItogSalarySum = ItogSalary.Sum(x => x.payroll);

            ViewBag.NumberOfEmployees = ItogSalary.Sum(x => x.orderCount);
            ViewBag.OrderCount = _orderServices.GetDataClosing().Count();
            ViewBag.Sum = ItogSalarySum;

            return View(ItogSalary);
        }

        [HttpPost]
        public ActionResult DayResult(bool confirmation = false)
        {
            if (confirmation)
            {
                _closeShiftModule.CurrentShift();
                return RedirectToAction("Index", "Order");
            }

            return Redirect("DayResult");
        }

        [HttpGet]
        public ActionResult CompletedOrdersOfOneEmployee(string idEmploee)
        {
            if (idEmploee != null)
            {
                int id = Convert.ToInt32(idEmploee);
                return View(Mapper.Map<IEnumerable<OrderServicesCarWashView>>(_dayResult.DayResultInfoOneEmployee(id)));
            }

            return View();
        }

    }
}
