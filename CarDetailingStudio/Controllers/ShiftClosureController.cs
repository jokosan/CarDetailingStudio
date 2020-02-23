using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;

namespace CarDetailingStudio.Controllers
{
    public class ShiftClosureController : Controller
    {
        private IWagesForDaysWorkedGroup _wagesForDays;
        private ICloseShiftModule _closeShiftModule;
        private IDayResult _dayResult;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;

        public ShiftClosureController(IDayResult dayResult, IWagesForDaysWorkedGroup wagesForDays,
                                      ICloseShiftModule closeShiftModule, IOrderCarWashWorkersServices orderCarWashWorkers)
        {
            _wagesForDays = wagesForDays;
            _closeShiftModule = closeShiftModule;
            _dayResult = dayResult;
            _orderCarWashWorkers = orderCarWashWorkers;
        }

        public ActionResult ShiftInformation()
        {
            return View(Mapper.Map<IEnumerable<DayResultModelView>>(_dayResult.TotalForEachEmployee()));
        }

        #region Выплаты за дни 

        public ActionResult PaymentForSpecificDays(int? idCarWash)
        {
            if (idCarWash != null)
            {
                return View(Mapper.Map<IEnumerable<WagesForDaysWorkedView>>(_wagesForDays.DayOrderResult(idCarWash)));
            }

            return RedirectToAction("ShiftInformation");
        }

        [HttpPost]
        public ActionResult PaymentForSpecificDays(List<string> closingData, List<string> carWashWorkers)
        {
            if (closingData != null && carWashWorkers != null)
            {
                _wagesForDays.PayrollForDaysWorked(closingData, carWashWorkers);
                return RedirectToAction("ShiftInformation");
            }

            return View();
        }

        #endregion

        #region Выплаты за конкретный заказ

        public ActionResult PaymentForTheCompletedOrder(int? idCarWash)
        {
            if (idCarWash != null)
            {
                return View(Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(_orderCarWashWorkers.GetClosedDay(idCarWash)));

            }

            return RedirectToAction("ShiftInformation");
        }

        [HttpPost]
        public ActionResult PaymentForTheCompletedOrder(List<string> idShiftClosure)
        {
            if (idShiftClosure != null)
            {
                _wagesForDays.PartPayroll(idShiftClosure);
                return RedirectToAction("ShiftInformation");
            }

            return View();
        }

        #endregion

        #region Закрыть все отработанные дни

        [HttpPost, ActionName("ShiftInformation")]
        public ActionResult AllDay(int? id)
        {
            if (id != null)
            {
                _wagesForDays.PayWagesForAllDays(id);
            }

            return RedirectToAction("ShiftInformation");
        }

        #endregion
    }
}
