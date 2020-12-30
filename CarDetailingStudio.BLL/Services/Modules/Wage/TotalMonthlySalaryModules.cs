using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.ExpensesServices;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;

namespace CarDetailingStudio.BLL.Services.Modules.Wage
{
    public class TotalMonthlySalaryModules : ITotalMonthlySalaryModules
    {
        private readonly IOrderCarWashWorkersServices _orderCarWashWorkers;
        private readonly ICarWashWorkersServices _carWashWorkers;
        private readonly ISalaryArchive _salaryArchive;
        private readonly ISalaryExpenses _salaryExpenses;

        public TotalMonthlySalaryModules(
            IOrderCarWashWorkersServices orderCarWashWorkers,
            ICarWashWorkersServices carWashWorkers,
            ISalaryArchive salaryArchive,
            ISalaryExpenses salaryExpenses)
        {
            _orderCarWashWorkers = orderCarWashWorkers;
            _carWashWorkers = carWashWorkers;
            _salaryArchive = salaryArchive;
            _salaryExpenses = salaryExpenses;
        }

        public async Task CloseWagesForMonth()
        { 
            
        }

        public async Task CheckMonthlyPaymentsEmployee(int? id)
        {
            var lastMonthsSalary = await _salaryArchive.MonthlySalary(id.Value, DateTime.Now.AddMonths(-1).Month, DateTime.Now.Year);
            
            if (lastMonthsSalary.Count() == 0)
            {
                await AddSalaryArchive(id);
            }
        }

        public async Task CheckForPastMonths()
        {
            var carWashWorkers = await _carWashWorkers.GetChooseEmployees();
            
            foreach (var item in carWashWorkers)
            {
                await AddSalaryArchive(item.id);    
            }            
        }

        private async Task AddSalaryArchive(int? id)
        {
            var orderCarWorkers = await _orderCarWashWorkers.SelectMonth(id, DateTime.Now.AddMonths(-1).Month, DateTime.Now.Year);
            var salaryExpenses = await _salaryExpenses.PayrollExpensesPerMonth(id, DateTime.Now.AddMonths(-1).Month, DateTime.Now.Year);

            SalaryArchiveBll salaryArchive = new SalaryArchiveBll();
            salaryArchive.carWashWorkersId = id.Value;
            salaryArchive.date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(0).AddDays(-1);
            salaryArchive.amountOfCompletedOrders = orderCarWorkers.Count();
            salaryArchive.monthlySalaryAmountEarned = orderCarWorkers.Sum(x => x.Payroll);
            salaryArchive.monthlySalaryAmountReceived = salaryExpenses.Sum(x => x.Expenses.Amount);
            salaryArchive.balanceAtTheEndOfTheMonth = (salaryArchive.monthlySalaryAmountReceived - salaryArchive.monthlySalaryAmountEarned);

            if (salaryArchive.balanceAtTheEndOfTheMonth == 0)
            {
                salaryArchive.salaryStatus = true;
            }
            else
            {
                salaryArchive.salaryStatus = false;
            }

            await _salaryArchive.Insert(salaryArchive);
        }

      

    }
}
