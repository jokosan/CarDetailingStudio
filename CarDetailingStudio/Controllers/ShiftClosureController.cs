using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
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
        private ITotalMonthlySalaryModules _totalMonthlySalary;
        private ISalaryArchive _salaryArchive;

        public ShiftClosureController(
            IDayResult dayResult,
            IWagesForDaysWorkedGroup wagesForDays,
            ICloseShiftModule closeShiftModule,
            IOrderCarWashWorkersServices orderCarWashWorkers,
            ISalaryBalanceService salaryBalance,
            ICarWashWorkersServices carWashWorkers,
            IBonusToSalary bonusToSalary,
            ITotalMonthlySalaryModules totalMonthlySalary,
            ISalaryArchive salaryArchive)
        {
            _wagesForDays = wagesForDays;
            _closeShiftModule = closeShiftModule;
            _dayResult = dayResult;
            _orderCarWashWorkers = orderCarWashWorkers;
            _salaryBalance = salaryBalance;
            _carWashWorkers = carWashWorkers;
            _bonusToSalary = bonusToSalary;
            _totalMonthlySalary = totalMonthlySalary;
            _salaryArchive = salaryArchive;
        }

        public async Task<ActionResult> ShiftInformation()
        {
            return View(Mapper.Map<IEnumerable<DayResultModelView>>(await _dayResult.TotalForEachEmployee()));
        }

        #region Проверить надобность 
        #region Выплаты за дни 

        public async Task<ActionResult> PaymentForSpecificDays(int? idCarWash, bool message = true, bool messageClose = true)
        {
            if (idCarWash != null)
            {
                await _totalMonthlySalary.CheckMonthlyPaymentsEmployee(idCarWash);

                var viewResult = Mapper.Map<IEnumerable<WagesForDaysWorkedView>>(await _wagesForDays.MonthOrderResult(idCarWash, DateTime.Now.Month, DateTime.Now.Year)).OrderByDescending(x => x.carWashWorkersId);
                var remainingUnpaidWages = Mapper.Map<SalaryArchiveView>(await _salaryArchive.SelectId(idCarWash));
                // var payouts = Mapper.Map<IEnumerable<SalaryBalanceView>>(await _salaryBalance.SelectIdToDate(idCarWash, DateTime.Now));
                var payouts = Mapper.Map<IEnumerable<SalaryBalanceView>>(await _salaryBalance.SelectIdToDate(idCarWash, DateTime.Now.Month, DateTime.Now.Year));
                var salaryBalance = Mapper.Map<SalaryBalanceView>(await _salaryBalance.LastMonthBalance(idCarWash));
                var bonusToSalary = Mapper.Map<IEnumerable<BonusToSalaryView>>(await _bonusToSalary.WhereMontsBonusToSalary(idCarWash.Value));                

                var salaryArxiv = Mapper.Map<SalaryArchiveView>(await _salaryArchive.LastMonth(idCarWash.Value));               

                double monthlySalary = viewResult.Sum(s => s.payroll).Value;
                double paidPerMonth = payouts.Sum(s => s.payoutAmount).Value;
                double bonusToSalarySum = bonusToSalary.Sum(s => s.amount).Value;
                double lastMonth = 0;

                if (salaryArxiv != null)
                {
                    lastMonth = salaryArxiv.balanceAtTheEndOfTheMonth.Value;
                }
               
                ViewBag.IdCarWash = idCarWash;
                ViewBag.MonthlySalary = monthlySalary;
                ViewBag.PaidPerMonth = paidPerMonth;
                ViewBag.LastMonth = lastMonth;
                ViewBag.BonusToSalary = bonusToSalarySum; 
                ViewBag.TotalPayable = Math.Round(monthlySalary - paidPerMonth + lastMonth + bonusToSalarySum, 3);
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

        #endregion

        #region Выплаты за конкретный заказ
        // Проверить надобность и удалить 
        public async Task<ActionResult> PaymentForTheCompletedOrder(int? idCarWash, DateTime? date)
        {
            if (idCarWash != null && date != null)
            {
                return View(Mapper.Map<IEnumerable<OrderCarWashWorkersView>>(await _orderCarWashWorkers.GetClosedDay(idCarWash, date)));
            }

            return RedirectToAction("ShiftInformation");
        }

        #endregion
        #endregion


        [HttpPost]
        public async Task<ActionResult> PartialPayout(string payout, int? idEmployee, double? totalPayable, double? SalaryCurrentMonth, double? Prize, double? BalancLastMonth, double? PaidMonth )
        {
            try
            {
                string stringReplace = payout.Replace(".", ",");
                double payoutAmount = Convert.ToDouble(stringReplace);

                if (idEmployee != null)
                {
                    await _wagesForDays.PaymentOfPartOfTheSalary(idEmployee, payoutAmount, totalPayable.Value, SalaryCurrentMonth.Value, Prize.Value, BalancLastMonth.Value, PaidMonth.Value);
                }

                return RedirectToAction("PaymentForSpecificDays", "ShiftClosure", new RouteValueDictionary(new
                {
                    idCarWash = idEmployee
                }));
            }
            catch
            {
                return RedirectToAction("PaymentForSpecificDays", "ShiftClosure", new RouteValueDictionary(new
                {
                    idCarWash = idEmployee,
                    message = false
                }));

            }
        }
    }
}

