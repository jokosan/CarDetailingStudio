using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift
{
    public class WagesForDaysWorkedGroup : IWagesForDaysWorkedGroup
    {
        private IOrderCarWashWorkersServices _orderCarWash;
        private ISalaryExpenses _salaryExpenses;

        public WagesForDaysWorkedGroup(IOrderCarWashWorkersServices orderCarWash, ISalaryExpenses salaryExpenses)
        {
            _orderCarWash = orderCarWash;
            _salaryExpenses = salaryExpenses;
        }

        public IEnumerable<WagesForDaysWorkedBll> DayOrderResult(int? Id)
        {
            var getResult = _orderCarWash.SampleForPayroll(Id);

            return getResult.GroupBy(x => x.IdCarWashWorkers)
                              .Select(y => new WagesForDaysWorkedBll
                              {
                                  carWashWorkersId = y.First().IdCarWashWorkers,
                                  ClosingData = y.First().OrderServicesCarWash.ClosingData?.ToString("dd.MM.yyy"),
                                  DiscountPrice = y.Sum(s => s.OrderServicesCarWash.DiscountPrice),
                                  orderCount = y.Count(),
                                  calculationStatus = y.First().CalculationStatus,
                                  payroll = y.Sum(s => s.Payroll)
                              });
        }

        public void PartPayroll(List<string> idCureentShift)
        {
            foreach (var idShift in idCureentShift)
            {
                var pay = _orderCarWash.СontractorAllId(Convert.ToInt32(idShift));
                PayWages(pay);
            }
        }

        public void PayrollForDaysWorked(List<string> day, List<string> carWashWorkers)
        {
            Dictionary<int, DateTime> keyValues = new Dictionary<int, DateTime>();

            for (int i = 0; i < day.Count; i++)
            {
                if (day.Count == carWashWorkers.Count)
                {
                    keyValues.Add(Convert.ToInt32(carWashWorkers[i]), Convert.ToDateTime(day[i]));
                }
            }

            foreach (var item in keyValues)
            {
                var result = _orderCarWash.SampleForPayroll(item.Key, item.Value);
                PayWages(result);
            }
        }

        public void PayWagesForAllDays(int? idCarWashWorkers)
        {
            var dayAll = _orderCarWash.SampleForPayroll(idCarWashWorkers);
            PayWages(dayAll);
        }

        private void PayWages(IEnumerable<OrderCarWashWorkersBll> orderCarWash)
        {
            foreach (var itemResultOrderDay in orderCarWash)
            {

                itemResultOrderDay.salaryDate = DateTime.Now;
                itemResultOrderDay.CalculationStatus = true;


                _orderCarWash.UpdateOrderCarWashWorkers(itemResultOrderDay);
                AddSalaryExpenses(orderCarWash);
            }
        }

        private void AddSalaryExpenses(IEnumerable<OrderCarWashWorkersBll> orderCarWash)
        {
            var SalaryExpenses = orderCarWash.GroupBy(x => x.IdCarWashWorkers)
                               .Select(y => new SalaryExpensesBll
                               {
                                   idCarWashWorkers = y.First().IdCarWashWorkers,
                                   amount = y.Sum(s => s.Payroll),
                                   dateExpenses = DateTime.Now,
                                   expenseCategoryId = 21
                               });

            _salaryExpenses.Insert(SalaryExpenses);
        }
    }
}
