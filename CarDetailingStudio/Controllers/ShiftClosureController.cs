using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
    public class ShiftClosureController : Controller
    {
        private IWagesForDaysWorkedGroup _wagesForDays;
        private ICloseShiftModule _closeShiftModule;
        private IDayResult _dayResult;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private ISalaryBalanceService _salaryBalance;
        private ICarWashWorkersServices _carWashWorkers;

        public ShiftClosureController(IDayResult dayResult, IWagesForDaysWorkedGroup wagesForDays,
                                      ICloseShiftModule closeShiftModule, IOrderCarWashWorkersServices orderCarWashWorkers,
                                      ISalaryBalanceService salaryBalance, ICarWashWorkersServices carWashWorkers)
        {
            _wagesForDays = wagesForDays;
            _closeShiftModule = closeShiftModule;
            _dayResult = dayResult;
            _orderCarWashWorkers = orderCarWashWorkers;
            _salaryBalance = salaryBalance;
            _carWashWorkers = carWashWorkers;
        }

        public ActionResult ShiftInformation()
        {
            return View(Mapper.Map<IEnumerable<DayResultModelView>>(_dayResult.TotalForEachEmployee()));
        }

        #region Выплаты за дни 

        public ActionResult PaymentForSpecificDays(int? idCarWash, bool message = true, bool messageClose = true)
        {
            if (idCarWash != null)
            {
                var viewResult = Mapper.Map<IEnumerable<WagesForDaysWorkedView>>(_wagesForDays.DayOrderResult(idCarWash));
                var payouts = Mapper.Map<IEnumerable<SalaryBalanceView>>(_salaryBalance.SelectIdToDate(idCarWash, DateTime.Now));
                var salaryBalance = Mapper.Map<SalaryBalanceView>(_salaryBalance.LastMonthBalance(idCarWash));

                double monthlySalary = viewResult.Sum(s => s.payroll).Value;
                double paidPerMonth = payouts.Sum(s => s.payoutAmount).Value;
                double lastMonthBalance = 0;

                if (salaryBalance != null)
                {
                    lastMonthBalance = salaryBalance.accountBalance.Value - salaryBalance.payoutAmount.Value;
                }

                ViewBag.IdCarWash = idCarWash;
                ViewBag.MonthlySalary = monthlySalary;
                ViewBag.PaidPerMonth = paidPerMonth;
                ViewBag.LastMonth = lastMonthBalance;
                ViewBag.TotalPayable = monthlySalary - paidPerMonth + lastMonthBalance;
                ViewBag.Employee = Mapper.Map<CarWashWorkersView>(_carWashWorkers.CarWashWorkersId(idCarWash));
                ViewBag.Payouts = payouts;

                if (message == false)
                    ViewBag.Message = "Данная запись не соответствует типу double";

                if (messageClose == false)
                    ViewBag.MessageClose = "Данная запись не соответствует типу double";

                return View(viewResult);
            }

            return RedirectToAction("ShiftInformation");
        }

        [HttpPost]
        public ActionResult PaymentForSpecificDays(string payout, int? id, double totalPayable)
        {
            try
            {
                double? payoutAmount = Convert.ToDouble(payout);

                if (id != null)
                {
                    _wagesForDays.PaymentOfPartOfTheSalary(id, payoutAmount.Value, totalPayable, true);
                }

                return RedirectToAction("PaymentForSpecificDays", "ShiftClosure", new RouteValueDictionary(new
                {
                    idCarWash = id
                }));
            }
            catch
            {
                return RedirectToAction("PaymentForSpecificDays", "ShiftClosure", new RouteValueDictionary(new
                {
                    idCarWash = id,
                    messageClose = false
                }));

            }
        }

        #endregion

        #region Выплаты за конкретный заказ

        public ActionResult PaymentForTheCompletedOrder(int? idCarWash, DateTime? date)
        {
            if (idCarWash != null && date != null)
            {
                return View(Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(_orderCarWashWorkers.GetClosedDay(idCarWash, date)));
            }

            return RedirectToAction("ShiftInformation");
        }

        #endregion


        [HttpPost]
        public ActionResult PartialPayout(string payout, int? id, double totalPayable, bool NegativeBalance = false)
        {
            try
            {
                string stringReplace = payout.Replace(".", ",");
                double payoutAmount = Convert.ToDouble(stringReplace);

                if (id != null)
                {
                    _wagesForDays.PaymentOfPartOfTheSalary(id, payoutAmount, totalPayable, false, NegativeBalance);
                }

                return RedirectToAction("PaymentForSpecificDays", "ShiftClosure", new RouteValueDictionary(new
                {
                    idCarWash = id
                }));
            }
            catch
            {
                return RedirectToAction("PaymentForSpecificDays", "ShiftClosure", new RouteValueDictionary(new
                {
                    idCarWash = id,
                    message = false
                }));

            }

        }

    }
}

