﻿using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    public class ItogOrderController : Controller
    {
        private ICloseShiftModule _closeShiftModule;
        private IDayResult _dayResult;
        private IOrderServicesCarWashServices _orderServices;
        private IOrderCarWashWorkersServices _orderCarWashWorker;
        private ICarWashWorkersServices _carWashWorkers;

        public ItogOrderController(IDayResult dayResult, ICloseShiftModule closeShiftModule,
                                    IOrderServicesCarWashServices orderServices, IOrderCarWashWorkersServices orderCarWashWorkers,
                                    ICarWashWorkersServices carWashWorkers)
        {
            _dayResult = dayResult;
            _closeShiftModule = closeShiftModule;
            _orderServices = orderServices;
            _orderCarWashWorker = orderCarWashWorkers;
            _carWashWorkers = carWashWorkers;
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
        public ActionResult CompletedOrdersOfOneEmployee(int? idEmploee)
        {
            if (idEmploee != null)
            {
                var carWashWorkersDayTotal = Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(_orderCarWashWorker.SampleForPayroll(idEmploee.Value, DateTime.Now));

                ViewBag.CarWashWorker = Mapper.Map<CarWashWorkersView>(_carWashWorkers.CarWashWorkersId(idEmploee));
                ViewBag.SumOrder = carWashWorkersDayTotal.Sum(x => x.Payroll);
                ViewBag.Sum = carWashWorkersDayTotal.Sum(x => x.OrderServicesCarWash.DiscountPrice);

                return View(carWashWorkersDayTotal);
            }

            return View();
        }
    }
}
