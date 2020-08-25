using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
    public class ItogOrderController : Controller
    {
        private ICloseShiftModule _closeShiftModule;
        private IDayResult _dayResult;
        private IOrderServicesCarWashServices _orderServices;
        private IOrderCarWashWorkersServices _orderCarWashWorker;
        private ICarWashWorkersServices _carWashWorkers;
        private ICashier _cashier;
       

        public ItogOrderController(IDayResult dayResult, ICloseShiftModule closeShiftModule,
                                    IOrderServicesCarWashServices orderServices, IOrderCarWashWorkersServices orderCarWashWorkers,
                                    ICarWashWorkersServices carWashWorkers, ICashier cashier)
        {
            _dayResult = dayResult;
            _closeShiftModule = closeShiftModule;
            _orderServices = orderServices;
            _orderCarWashWorker = orderCarWashWorkers;
            _carWashWorkers = carWashWorkers;
            _cashier = cashier;        
        }

        // GET: OrderInfoViewModels
        public async Task<ActionResult> DayResult()
        {
            var ItogSalary = Mapper.Map<IEnumerable<DayResultModelView>>(await _dayResult.DayResultViewInfo());
            var ItogSalarySum = ItogSalary.Sum(x => x.payroll);
            var orderServices = await _orderServices.GetDataClosing();

            ViewBag.NumberOfEmployees = ItogSalary.Sum(x => x.orderCount);
            ViewBag.OrderCount = orderServices.Count();
            ViewBag.Sum = ItogSalarySum;

            return View(ItogSalary);
        }

        [HttpPost]
        public async Task<ActionResult> DayResult(bool confirmation = false)
        {
            if (confirmation)
            {
                await _closeShiftModule.CurrentShift();
                await _cashier.EndDay();
                               
                return RedirectToAction("SummaryOfTheDay", "Analytics", new RouteValueDictionary(new
                {
                    CloseDay = true
                }));
            }

            return Redirect("DayResult");
        }

        [HttpGet]
        public async Task<ActionResult> CompletedOrdersOfOneEmployee(int? idEmploee)
        {
            if (idEmploee != null)
            {
                var carWashWorkersDayTotal = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWashWorker.SampleForPayroll(idEmploee.Value, DateTime.Now));

                ViewBag.CarWashWorker = Mapper.Map<CarWashWorkersView>(await _carWashWorkers.CarWashWorkersId(idEmploee));
                ViewBag.SumOrder = carWashWorkersDayTotal.Sum(x => x.Payroll);
                ViewBag.Sum = carWashWorkersDayTotal.Sum(x => x.OrderServicesCarWash.DiscountPrice);

                return View(carWashWorkersDayTotal);
            }

            return View();
        }
    }
}
