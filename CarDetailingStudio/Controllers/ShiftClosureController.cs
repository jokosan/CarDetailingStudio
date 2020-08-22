using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
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
    public class ShiftClosureController : Controller
    {
        private IWagesForDaysWorkedGroup _wagesForDays;
        private ICloseShiftModule _closeShiftModule;
        private IDayResult _dayResult;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private ISalaryBalanceService _salaryBalance;
        private ICarWashWorkersServices _carWashWorkers;
        private IBonusToSalary _bonusToSalary;

        public ShiftClosureController(IDayResult dayResult, IWagesForDaysWorkedGroup wagesForDays,
                                      ICloseShiftModule closeShiftModule, IOrderCarWashWorkersServices orderCarWashWorkers,
                                      ISalaryBalanceService salaryBalance, ICarWashWorkersServices carWashWorkers, IBonusToSalary bonusToSalary)
        {
            _wagesForDays = wagesForDays;
            _closeShiftModule = closeShiftModule;
            _dayResult = dayResult;
            _orderCarWashWorkers = orderCarWashWorkers;
            _salaryBalance = salaryBalance;
            _carWashWorkers = carWashWorkers;
            _bonusToSalary = bonusToSalary;
        }

        public async Task<ActionResult> ShiftInformation()
        {
            return View(Mapper.Map<IEnumerable<DayResultModelView>>(await _dayResult.TotalForEachEmployee()));
        }

        #region Выплаты за дни 

        public async Task<ActionResult> PaymentForSpecificDays(int? idCarWash, bool message = true, bool messageClose = true)
        {
            if (idCarWash != null)
            {
                var viewResult = Mapper.Map<IEnumerable<WagesForDaysWorkedView>>(await _wagesForDays.DayOrderResult(idCarWash));
                var payouts = Mapper.Map<IEnumerable<SalaryBalanceView>>(await _salaryBalance.SelectIdToDate(idCarWash, DateTime.Now));
                var salaryBalance = Mapper.Map<SalaryBalanceView>(await _salaryBalance.LastMonthBalance(idCarWash));
                var bonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.WhereMontsBonusToSalary());

                double monthlySalary = viewResult.Sum(s => s.payroll).Value;
                double paidPerMonth = payouts.Sum(s => s.payoutAmount).Value;
                double bonusToSalarySum = bonusToSalary.Sum(s => s.amount).Value;
                double lastMonthBalance = 0;

                if (salaryBalance != null)
                {
                    lastMonthBalance = salaryBalance.accountBalance.Value - salaryBalance.payoutAmount.Value;
                }

                ViewBag.IdCarWash = idCarWash;
                ViewBag.MonthlySalary = monthlySalary;
                ViewBag.PaidPerMonth = paidPerMonth;
                ViewBag.LastMonth = lastMonthBalance;
                ViewBag.BonusToSalary = bonusToSalarySum; 
                ViewBag.TotalPayable = Math.Round(monthlySalary - paidPerMonth + lastMonthBalance + bonusToSalarySum, 3);
                ViewBag.Employee = Mapper.Map<CarWashWorkersView>(await _carWashWorkers.CarWashWorkersId(idCarWash));
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
        public async Task<ActionResult> PaymentForSpecificDays(string payout, int? id, double totalPayable)
        {
            try
            {
                double? payoutAmount = Convert.ToDouble(payout);

                if (id != null)
                {
                    await _wagesForDays.PaymentOfPartOfTheSalary(id, payoutAmount.Value, totalPayable, true);
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

        public async Task<ActionResult> PaymentForTheCompletedOrder(int? idCarWash, DateTime? date)
        {
            if (idCarWash != null && date != null)
            {
                return View(Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWashWorkers.GetClosedDay(idCarWash, date)));
            }

            return RedirectToAction("ShiftInformation");
        }

        #endregion


        [HttpPost]
        public async Task<ActionResult> PartialPayout(string payout, int? id, double totalPayable, bool NegativeBalance = false)
        {
            try
            {
                string stringReplace = payout.Replace(".", ",");
                double payoutAmount = Convert.ToDouble(stringReplace);

                if (id != null)
                {
                    await _wagesForDays.PaymentOfPartOfTheSalary(id, payoutAmount, totalPayable, false, NegativeBalance);
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

